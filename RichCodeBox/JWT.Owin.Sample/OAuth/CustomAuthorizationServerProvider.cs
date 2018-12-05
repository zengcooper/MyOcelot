using Microsoft.Owin.Security;
using Microsoft.Owin.Security.DataHandler.Encoder;
using Microsoft.Owin.Security.OAuth;
using System;
using System.IdentityModel.Tokens;
using System.Security.Claims;
using System.Threading.Tasks;

namespace JWT.Owin.Sample.OAuth
{
    /// <summary>
    /// <![CDATA[自定义认证服务器]]>
    /// </summary>
    [Obsolete("这里的服务是担任资源拥有者的身份，对携带token的请求进行身份认证。因此不需要认证服务提供程序")]
    public class CustomAuthorizationServerProvider : OAuthAuthorizationServerProvider
    {

        /// <summary>
        /// 
        /// </summary>
        public CustomAuthorizationServerProvider()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        private TokenValidationParameters _validationParameters;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="validationParameters"></param>
        public CustomAuthorizationServerProvider(TokenValidationParameters validationParameters)
        {
            _validationParameters = validationParameters;
        }

        /// <summary>
        /// <![CDATA[授权资源拥有者证书]]>
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            var secret = TextEncodings.Base64Url.Decode(OAuthCommon.Constants.Consts.JWT.Security.SecretKey);//秘钥
            var username = context.UserName;
            var password = context.Password;
            string userid;
            if (!CheckCredential(username, password, out userid))
            {
                context.SetError("invalid_grant", "The user name or password is incorrect");
                context.Rejected();//拒绝访问
                return Task.FromResult<object>(context);
            }

            var ticket = new AuthenticationTicket(SetClaimsIdentity(context, userid, username), new AuthenticationProperties());
            context.Validated(ticket);

            return Task.FromResult<object>(context);
        }

        /// <summary>
        /// <![CDATA[验证客户端授权]]>
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            string client_id;
            string client_secret;
            context.TryGetFormCredentials(out client_id, out client_secret);
            //验证clientid
            if (string.IsNullOrEmpty(context.ClientId) ||
                context.ClientId != OAuthCommon.Constants.Consts.Client.ClientId)
            {
                context.SetError("ClientId is incorrect!");
                context.Rejected();
            }
            //正常
            else
            {
                context.Validated();
            }
            return Task.FromResult<object>(context);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override Task GrantRefreshToken(OAuthGrantRefreshTokenContext context)
        {
            return Task.FromResult<object>(context);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override Task ValidateAuthorizeRequest(OAuthValidateAuthorizeRequestContext context)
        {

            return base.ValidateAuthorizeRequest(context);
        }

        /// <summary>
        /// 验证客户端重定向URL
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override Task ValidateClientRedirectUri(OAuthValidateClientRedirectUriContext context)
        {
            if (string.IsNullOrEmpty(context.RedirectUri) ||
                context.ClientId != OAuthCommon.Constants.Consts.Client.ClientId)
            {
                context.SetError("client_id is incorrect!");
                context.Rejected();
            }
            if (string.IsNullOrEmpty(context.RedirectUri))
            {
                context.SetError("redirect_uri is null!");
                context.Rejected();
            }
            else
            {
                context.Validated(context.RedirectUri);
            }
            return Task.FromResult<object>(0);
        }

        /// <summary>
        /// 验证令牌
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override Task ValidateTokenRequest(OAuthValidateTokenRequestContext context)
        {
            context.Validated();
            return Task.FromResult<object>(context);
        }

        #region 私有方法
        /// <summary>
        /// 设置声明
        /// </summary>
        /// <param name="context"></param>
        /// <param name="userid"></param>
        /// <param name="usercode"></param>
        /// <returns></returns>
        private static ClaimsIdentity SetClaimsIdentity(OAuthGrantResourceOwnerCredentialsContext context, string userid, string usercode)
        {
            var identity = new ClaimsIdentity("JWT");
            identity.AddClaim(new Claim(JwtRegisteredClaimNames.Sub, userid));
            identity.AddClaim(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            identity.AddClaim(new Claim(ClaimTypes.Name, usercode));
            identity.AddClaim(new Claim(ClaimTypes.Role, "1"));
            identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, usercode));
            return identity;
        }

        /// <summary>
        /// 检测用户凭证
        /// </summary>
        /// <param name="usernme"></param>
        /// <param name="password"></param>
        /// <param name="userid"></param>
        /// <returns></returns>
        private static bool CheckCredential(string usernme, string password, out string userid)
        {
            var success = false;
            // 用户名和密码验证
            if (usernme == "admin" &&
                password == "admin")
            {
                userid = "1";
                success = true;
            }
            else
            {
                userid = string.Empty;
            }
            return success;
        }
        #endregion 私有方法
    }
}