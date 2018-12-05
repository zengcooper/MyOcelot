using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using Microsoft.Owin.Security.OAuth;
using Microsoft.Owin.Security.Infrastructure;
using ImplicitGrant.Sample.OAuth;
using Microsoft.Owin.Security.DataHandler;
using Microsoft.Owin.Security.DataProtection;
using System.Security.Claims;

[assembly: OwinStartup(typeof(ImplicitGrant.Sample.Startup))]

namespace ImplicitGrant.Sample
{

    /// <summary>
    /// 
    /// </summary>
    public class Startup
    {

        /// <summary>
        /// <![CDATA[配置简化授权模式]]>
        /// </summary>
        /// <param name="app"></param>
        public void Configuration(IAppBuilder app)
        {
            // 有关如何配置应用程序的详细信息，请访问 https://go.microsoft.com/fwlink/?LinkID=316888
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions()
            {
                AuthenticationMode = Microsoft.Owin.Security.AuthenticationMode.Active,
                AuthenticationType = "Brearer",
                Description = new Microsoft.Owin.Security.AuthenticationDescription() { },
                Provider = new OAuthBearerAuthenticationProvider()
                {
                    //http头中携带access_token时，
                    OnRequestToken = (e) =>
                    {
                        //解密令牌，生成身份票据
                        if (string.IsNullOrEmpty(e.Token) == false)
                        {
                            IDataProtector protector = app.CreateDataProtector(typeof(OAuthAuthorizationServerMiddleware).Namespace, "Access_Token", "v1");
                            TicketDataFormat ticketDataFormat = new TicketDataFormat(protector);
                            var ticket = ticketDataFormat.Unprotect(e.Token);
                            e.Request.User = new ClaimsPrincipal(ticket.Identity);
                        }
                        return Task.FromResult(0);
                    },
                    OnValidateIdentity = (e) =>
                    {
                        if (e.Ticket != null) e.Validated();
                        return Task.FromResult(0);
                    }
                }

            });

            //认证服务中间件
            app.UseOAuthAuthorizationServer(new Microsoft.Owin.Security.OAuth.OAuthAuthorizationServerOptions()
            {
                AllowInsecureHttp = true,
                AuthenticationMode = Microsoft.Owin.Security.AuthenticationMode.Active,
                AuthenticationType = "Bearer",
                AuthorizeEndpointPath = new PathString("/oauth2/Authorize"),
                TokenEndpointPath = new PathString("/oauth2/Token"),
                Provider = new ImpicitAuthorizationServerProvider(),
                AccessTokenExpireTimeSpan = new TimeSpan(2, 0, 0),

            });

        }
    }
}
