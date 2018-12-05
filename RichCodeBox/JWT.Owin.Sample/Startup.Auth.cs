using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using System.Web.Configuration;
using Microsoft.Owin.Security.DataHandler.Encoder;
using System.IdentityModel.Tokens;
using Microsoft.Owin.Security.Jwt;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using Microsoft.Owin.Security.DataProtection;
using System.Security.Claims;
using Microsoft.Owin.Security.DataHandler;
using Microsoft.Owin.Security.Infrastructure;
using Microsoft.AspNet.Identity;
using JWT.Owin.Sample.OAuth;
using System.Collections.Generic;

[assembly: OwinStartup(typeof(JWT.Owin.Sample.Startup))]

namespace JWT.Owin.Sample
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
            // 有关如何配置应用程序的详细信息，请访问 https://go.microsoft.com/fwlink/?LinkID=316888
            var issuer = "http://localhost:51912/";//发行者
            var audience = "fNm0EDIXbfuuDowUpAoq5GTEiywV8eg0TpiIVnV8";//观众
            var secret = TextEncodings.Base64Url.Decode(OAuthCommon.Constants.Consts.JWT.Security.SecretKey);//秘钥
            var signingKey = new System.IdentityModel.Tokens.InMemorySymmetricSecurityKey(secret);

            //令牌验证参数
            TokenValidationParameters tokenValidationParameters = new TokenValidationParameters()
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = signingKey,

                // Validate the JWT Issuer (iss) claim
                ValidateIssuer = true,
                ValidIssuer = "http://localhost:51912/",

                // Validate the JWT Audience (aud) claim
                ValidateAudience = false,
                ValidAudience = audience,

                // Validate the token expiry
                ValidateLifetime = true,

                ClockSkew = TimeSpan.Zero
            };
            //配置JwtBearer授权中间件   这个中间件的作用主要是解决由JWT方式提供的身份认证。算是Resource资源,认证服务器需要另外开发
            app.UseJwtBearerAuthentication(new JwtBearerAuthenticationOptions()
            {
                TokenValidationParameters = tokenValidationParameters,
                AuthenticationMode = Microsoft.Owin.Security.AuthenticationMode.Active,
                AuthenticationType = "Bearer",
                AllowedAudiences = new[] { audience },
                Description = new AuthenticationDescription(),
                Realm = "",//领域，范围;
                Provider = new OAuthBearerAuthenticationProvider()
                {
                    //验证当前访客身份
                    OnValidateIdentity = context =>
                    {
                        AuthenticationTicket ticket = context.Ticket;

                        //校验身份是否过期
                        if (ticket.Properties.ExpiresUtc < DateTime.Now)
                        {
                            context.SetError("the token has expired!");
                            context.Rejected();
                        }
                        else
                        {
                            context.Validated(ticket);
                        }
                        return Task.FromResult<object>(context);
                    },

                    //处理授权令牌 OAuthBearerAuthenticationHandler
                    //headers Authorization=Bear:token
                    OnRequestToken = (context) =>
                    {
                        context.Token = context.Token ?? context.Request.Query["access_token"];
                        if (context.Token != null)
                        {
                         
                            context.Response.Headers["WWW-Authorization"] = "Bearer";
                            //protector
                            IDataProtector protector = app.CreateDataProtector(typeof(OAuthAuthorizationServerMiddleware).Namespace, "Access_Token", "v1");
                            JwtFormat ticketDataFormat = new JwtFormat(tokenValidationParameters);
                            
                            var ticket = ticketDataFormat.Unprotect(context.Token);//从令牌字符串中，获取授权票据
                            context.Request.User = new ClaimsPrincipal(ticket.Identity);
                        }
                        return Task.FromResult<object>(context);
                    },
                    OnApplyChallenge = (context) => { return Task.FromResult<object>(context); }
                },
                IssuerSecurityTokenProviders = new IIssuerSecurityTokenProvider[]
                {
                     new SymmetricKeyIssuerSecurityTokenProvider(issuer, secret)//对称加密
                }

            });
        }
    }
}
