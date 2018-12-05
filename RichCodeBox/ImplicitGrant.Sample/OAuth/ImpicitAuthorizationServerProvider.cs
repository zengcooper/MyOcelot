using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Infrastructure;
using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using System.Web;

namespace ImplicitGrant.Sample.OAuth
{

    /// <summary>
    /// 
    /// </summary>
    public class ImpicitAuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        /// <summary>
        /// 验证 client 信息
        /// </summary>
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            string clientId;
            string clientSecret;
            if (!context.TryGetBasicCredentials(out clientId, out clientSecret))
            {
                context.TryGetFormCredentials(out clientId, out clientSecret);
            }

            if (clientId != "_clientId")
            {
                context.SetError("invalid_client", "client is not valid");
                return;
            }
            context.Validated();
        }

        /// <summary>
        /// 生成 authorization_code（authorization code 授权方式）、生成 access_token （implicit 授权模式）
        /// </summary>
        public override async Task AuthorizeEndpoint(OAuthAuthorizeEndpointContext context)
        {
            if (context.AuthorizeRequest.IsImplicitGrantType)
            {
                //implicit 授权方式 声明认证
                var identity = new ClaimsIdentity("Bearer");

                identity.AddClaim(new Claim(ClaimTypes.Name, "lichaoqiang"));
                identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, "1006"));
                identity.AddClaim(new Claim(ClaimTypes.Role, "user"));

                context.OwinContext.Authentication.SignIn(identity);
                context.RequestCompleted();
            }
            else if (context.AuthorizeRequest.IsAuthorizationCodeGrantType)
            {
                //authorization code 授权方式
                var redirectUri = context.Request.Query["redirect_uri"];
                var clientId = context.Request.Query["client_id"];
                var identity = new ClaimsIdentity(new GenericIdentity(
                    clientId, OAuthDefaults.AuthenticationType));

                var authorizeCodeContext = new AuthenticationTokenCreateContext(
                    context.OwinContext,
                    context.Options.AuthorizationCodeFormat,
                    new AuthenticationTicket(
                        identity,
                        new AuthenticationProperties(new Dictionary<string, string>
                        {
                        {"client_id", clientId},
                        {"redirect_uri", redirectUri}
                        })
                        {
                            IssuedUtc = DateTimeOffset.UtcNow,
                            ExpiresUtc = DateTimeOffset.UtcNow.Add(context.Options.AuthorizationCodeExpireTimeSpan)
                        }));

                await context.Options.AuthorizationCodeProvider.CreateAsync(authorizeCodeContext);
                context.Response.Redirect(redirectUri + "?code=" + Uri.EscapeDataString(authorizeCodeContext.Token));
                context.RequestCompleted();
            }
        }

        /// <summary>
        /// 验证 authorization_code 的请求
        /// </summary>
        public override async Task ValidateAuthorizeRequest(OAuthValidateAuthorizeRequestContext context)
        {
            if (context.AuthorizeRequest.ClientId == "_clientId" &&
                (context.AuthorizeRequest.IsAuthorizationCodeGrantType || context.AuthorizeRequest.IsImplicitGrantType))
            {
                context.Validated();
            }
            else
            {
                context.Rejected();
            }
        }

        /// <summary>
        /// 验证 redirect_uri
        /// </summary>
        public override async Task ValidateClientRedirectUri(OAuthValidateClientRedirectUriContext context)
        {
            context.Validated(context.RedirectUri);
        }

        /// <summary>
        /// 验证 access_token 的请求
        /// </summary>
        public override async Task ValidateTokenRequest(OAuthValidateTokenRequestContext context)
        {
            if (context.TokenRequest.IsAuthorizationCodeGrantType || context.TokenRequest.IsRefreshTokenGrantType)
            {
                context.Validated();
            }
            else
            {
                context.Rejected();
            }
        }
    }
}