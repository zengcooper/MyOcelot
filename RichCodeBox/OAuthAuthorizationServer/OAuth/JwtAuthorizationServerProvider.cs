using Microsoft.Owin.Security;
using Microsoft.Owin.Security.DataHandler.Encoder;
using Microsoft.Owin.Security.OAuth;
using System;
using System.IdentityModel.Tokens;
using System.Security.Claims;
using System.Threading.Tasks;

/*=================================================================================================
*
* Title:JwtAuthorizationServerProvider
* Author:李朝强
* Description:模块描述
* CreatedBy:lichaoqiang.com
* CreatedOn:
* ModifyBy:暂无...
* ModifyOn:
* Company:河南天一文化传播股份有限公司
* Blog:http://www.lichaoqiang.com
* Mark:
*
*** ================================================================================================*/
namespace OAuthAuthorizationServer.OAuth
{
    /// <summary>
    /// <![CDATA[自定义认证服务器]]>
    /// </summary>
    public class JwtAuthorizationServerProvider : OAuthAuthorizationServerProvider
    {

        /// <summary>
        /// 构造函数
        /// </summary>
        public JwtAuthorizationServerProvider()
        {

        }

        /// <summary>
        /// 令牌校验参数
        /// </summary>
        private TokenValidationParameters _validationParameters;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="validationParameters"></param>
        public JwtAuthorizationServerProvider(TokenValidationParameters validationParameters)
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
            //身份票据
            var ticket = new AuthenticationTicket(SetClaimsIdentity(context, userid, username), new AuthenticationProperties()
            {
                AllowRefresh = true,
                ExpiresUtc = DateTime.UtcNow + context.Options.AccessTokenExpireTimeSpan,
                IssuedUtc = DateTime.UtcNow,
                IsPersistent = false,
                RedirectUri = "http://www.lichaoqiang.com"
            });
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
            context.Validated();//授权刷新令牌
            return Task.FromResult<object>(context);
        }


        /// <summary>
        /// <![CDATA[验证请求]]>
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override Task ValidateAuthorizeRequest(OAuthValidateAuthorizeRequestContext context)
        {
            context.Validated();
            return Task.FromResult<object>(0);
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
                context.Validated();
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