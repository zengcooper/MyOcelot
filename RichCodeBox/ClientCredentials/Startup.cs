using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using Microsoft.Owin.Security.Infrastructure;
using Microsoft.Owin.Security;
using System.Security.Claims;
using System.Collections.Generic;
using Microsoft.Owin.Security.OAuth;
using Microsoft.Owin.Security.DataHandler;
using Microsoft.Owin.Security.DataProtection;

[assembly: OwinStartup(typeof(ClientCredentials.Startup))]

namespace ClientCredentials
{

    /// <summary>
    /// 
    /// </summary>
    public class Startup
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="app"></param>
        public void Configuration(IAppBuilder app)
        {
            //这个中间件主要是根据token，给客户端身份
            app.UseOAuthBearerAuthentication(new Microsoft.Owin.Security.OAuth.OAuthBearerAuthenticationOptions()
            {
                AuthenticationType = "Bearer",
                Provider = new OAuthBearerAuthenticationProvider()
                {
                    //处理请求令牌
                    OnRequestToken = (context) =>
                    {
                        context.Token = context.Token ?? context.Request.Query["access_token"];
                        if (context.Token != null)
                        {
                            context.Response.Headers["WWW-Authorization"] = "Bearer";
                            IDataProtector protector = app.CreateDataProtector(typeof(OAuthAuthorizationServerMiddleware).Namespace, "Access_Token", "v1");
                            var dataFormat = new TicketDataFormat(protector);
                            var ticket = dataFormat.Unprotect(context.Token);//从令牌字符串中，获取授权票据
                            if (ticket == null) return null;
                            context.Request.User = new ClaimsPrincipal(ticket.Identity);
                        }
                        return Task.FromResult<object>(context);
                    },
                    OnValidateIdentity = context =>
                    {
                        return Task.FromResult<object>(context);
                    },
                    OnApplyChallenge = (context) =>
                    {
                        return Task.FromResult<object>(context);
                    }
                },

            });
            //这个中间件主要是供客户端请求获取授权获取access_token
            app.UseOAuthAuthorizationServer(new Microsoft.Owin.Security.OAuth.OAuthAuthorizationServerOptions()
            {
                AuthenticationMode = AuthenticationMode.Active,
                AuthenticationType = "Bearer",
                AllowInsecureHttp = true,
                AuthorizeEndpointPath = new PathString("/Authorize/"),
                AccessTokenExpireTimeSpan = TimeSpan.FromHours(3),
                TokenEndpointPath = new PathString("/Token/"),
                Provider = new Microsoft.Owin.Security.OAuth.OAuthAuthorizationServerProvider()
                {
                    //验证客户端
                    OnValidateClientAuthentication = (context) =>
                    {
                        string clientId = null;
                        string clientSecret = null;
                        context.TryGetFormCredentials(out clientId, out clientSecret);
                        if (string.IsNullOrEmpty(clientId) ||
                            string.IsNullOrEmpty(clientSecret))
                        {
                            context.SetError("400", "client invalid!");
                        }
                        else
                        {
                            context.Validated();
                        }
                        return Task.FromResult(context);
                    },
                    //验证令牌
                    OnValidateTokenRequest = (context) =>
                    {
                        context.Validated();
                        return Task.FromResult(context);
                    },
                    //授权给客户端
                    OnGrantClientCredentials = (context) =>
                    {
                        var claims = new List<Claim>
                    {
                        new Claim(ClaimsIdentity.DefaultNameClaimType, context.ClientId),
                    };
                        string scope = string.Join(" ", context.Scope);
                        if (!string.IsNullOrEmpty(scope))
                        {
                            claims.Add(new Claim("scope", scope));
                        }
                        context.Validated(new ClaimsIdentity(claims, "Bearer"));

                        return Task.FromResult<object>(context);
                    },
                    //
                    OnGrantResourceOwnerCredentials = (context) =>
                    {
                        context.Validated();
                        return Task.FromResult<object>(context);
                    },
                    //在OnTokenEndpoint之前调用
                    OnTokenEndpointResponse = (context) =>
                    {
                        return Task.FromResult<object>(context);
                    },
                    OnTokenEndpoint = (context) =>
                    {
                        return Task.FromResult<object>(context);
                    },
                    OnValidateClientRedirectUri = (context) =>
                    {
                        //context.Validated(context.RedirectUri);
                        return Task.FromResult(context);
                    },
                    //验证授权
                    OnValidateAuthorizeRequest = (context) =>
                     {
                         context.Validated();
                         return Task.FromResult(context);
                     },


                },
                //AccessTokenProvider = new AuthenticationTokenProvider()
                //{
                //    OnCreate = (context) =>
                //    {
                //        context.SetToken("0000");

                //    },
                //    OnReceive = (context) => { }
                //},

            });

            // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=316888
        }
    }
}
