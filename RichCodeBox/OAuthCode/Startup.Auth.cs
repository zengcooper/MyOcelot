using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.Infrastructure;
using Microsoft.Owin.Security.OAuth;
using Owin;
using System.Security.Claims;
using System.Collections.Concurrent;

/*=================================================================================================
*
* Title:XXXXXXXX
* Author:李朝强
* Description:模块描述
* CreatedBy:lichaoqiang.com
* CreatedOn:2017-8-4 11:16:42
* ModifyBy:暂无...
* ModifyOn:2017-8-4 11:16:42
* Blog:http://www.lichaoqiang.com
* Mark:
*
*================================================================================================*/
[assembly: OwinStartup(typeof(OAuthCode.Startup))]
namespace OAuthCode
{
    /// <summary>
    /// 应用程序启动类
    /// </summary>
    public class Startup
    {

        /// <summary>
        /// 用来存放临时授权码 线程安全
        /// </summary>
        private readonly ConcurrentDictionary<string, string> _authenticationCodes = new ConcurrentDictionary<string, string>(StringComparer.Ordinal);

        /// <summary>
        /// 配置授权
        /// </summary>
        /// <param name="app"></param>
        public void Configuration(IAppBuilder app)
        {
            //创建OAuth授权服务器
            app.UseOAuthAuthorizationServer(new Microsoft.Owin.Security.OAuth.OAuthAuthorizationServerOptions()
            {
                AllowInsecureHttp = true,//开启
                AuthenticationType = "Bearer",
                AuthorizeEndpointPath = new PathString("/OAuth/Authorize"),
                TokenEndpointPath = new PathString("/OAuth/Token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromMinutes(30),
                Provider = new OAuthAuthorizationServerProvider()
                {
                    //授权码 authorization_code
                    OnGrantAuthorizationCode = ctx =>
                    {
                        if (ctx.Ticket != null &&
                            ctx.Ticket.Identity != null &&
                            ctx.Ticket.Identity.IsAuthenticated)
                        {
                            ctx.Validated(ctx.Ticket.Identity);//
                        }
                        return Task.FromResult(0);
                    },
                    OnGrantRefreshToken = ctx =>
                    {
                        if (ctx.Ticket != null &&
                            ctx.Ticket.Identity != null &&
                            ctx.Ticket.Identity.IsAuthenticated)
                        {
                            ctx.Validated();
                        }
                        return Task.FromResult(0);
                    },
                    //OnGrantResourceOwnerCredentials = (context) =>
                    //{
                    //    context.Validated(context.Ticket.Identity);
                    //    return Task.FromResult(0);
                    //},
                    OnValidateAuthorizeRequest = ctx =>
                    {
                        ctx.Validated();
                        return Task.FromResult(ctx);
                    },
                    //验证redirect_uri是否合法
                    OnValidateClientRedirectUri = context =>
                    {
                        context.Validated(redirectUri: context.RedirectUri);
                        return Task.FromResult(context);
                    },
                    //用来验证请求中的client_id和client_secret
                    OnValidateClientAuthentication = context =>
                    {
                        string clientId;
                        string clientSecret;
                        //这是通过Basic或form的方式，获取client_id和client_secret
                        if (context.TryGetBasicCredentials(out clientId, out clientSecret) ||
                            context.TryGetFormCredentials(out clientId, out clientSecret))
                        {
                            context.Validated(clientId);
                        }
                        return Task.FromResult(context);
                    },
                    OnAuthorizeEndpoint = context =>
                    {
                        return Task.FromResult(context);
                    },
                    OnTokenEndpoint = (context) =>
                    {
                        return Task.FromResult(context);
                    },
                    //OnGrantClientCredentials = (context) =>
                    //{
                    //    context.Validated();
                    //    return Task.FromResult(context);
                    //}
                },
                //Code授权
                AuthorizationCodeProvider = new AuthenticationTokenProvider()
                {

                    OnCreate = context =>
                    {
                        context.SetToken(DateTime.Now.Ticks.ToString());
                        string token = context.Token;
                        string ticket = context.SerializeTicket();
                        var redirect_uri = context.Request.Query["redirect_uri"];
                        context.Response.Redirect(string.Format("{0}?code={1}&state=1", redirect_uri, token));
                        _authenticationCodes[token] = ticket;//这里存放授权码
                    },
                    //当接收到code时
                    OnReceive = context =>
                    {
                        string token = context.Token;
                        string ticket;
                        if (_authenticationCodes.TryRemove(token, out ticket))
                        {
                            context.DeserializeTicket(ticket);
                        }

                    },
                },
                //（可选）访问令牌
                AccessTokenProvider = new AuthenticationTokenProvider()
                {
                    //创建访问令牌
                    OnCreate = (context) =>
                    {
                        string token = context.SerializeTicket();
                        context.SetToken(token);
                    },
                    //接收
                    OnReceive = (context) =>
                    {
                        context.DeserializeTicket(context.Token);
                    },
                },
                //刷新令牌
                RefreshTokenProvider = new AuthenticationTokenProvider()
                {
                    OnCreate = context =>
                    {
                        context.SetToken(context.SerializeTicket());
                    },
                    OnReceive = context =>
                    {
                        context.DeserializeTicket(context.Token);
                    },
                }
            });

            //本地Cookie身份认证
            app.UseCookieAuthentication(new CookieAuthenticationOptions()
            {
                LoginPath = new PathString("/Account/Login"),
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie
            });

        }
    }
}
