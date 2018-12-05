using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using Microsoft.Owin.Security.Jwt;
using Microsoft.Owin.Security.DataHandler.Encoder;
using System.IdentityModel.Tokens;
using System.Web.Http;
using Microsoft.Owin.Security.Infrastructure;

[assembly: OwinStartup(typeof(OAuthAuthorizationServer.Startup))]

namespace OAuthAuthorizationServer
{

    /// <summary>
    /// <![CDATA[启动类]]>
    /// </summary>
    public class Startup
    {

        /// <summary>
        /// <![CDATA[配置JWT认证]]>
        /// </summary>
        /// <param name="app"></param>
        public void Configuration(IAppBuilder app)
        {

            HttpConfiguration config = new HttpConfiguration();
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

                // Validate the JWT Issuer (iss) claim 颁布者
                ValidateIssuer = true,
                ValidIssuer = issuer,

                // Validate the JWT Audience (aud) claim 观众
                ValidateAudience = true,
                ValidAudience = audience,

                // Validate the token expiry
                ValidateLifetime = true,

                ClockSkew = TimeSpan.Zero,

            };


            //Jwt数据格式
            OAuth.JwtDataFormat jwtDataFormat = new OAuth.JwtDataFormat(issuer: issuer, secret: secret);

            // 有关如何配置应用程序的详细信息，请访问 https://go.microsoft.com/fwlink/?LinkID=316888
            app.UseOAuthAuthorizationServer(new Microsoft.Owin.Security.OAuth.OAuthAuthorizationServerOptions()
            {
                AllowInsecureHttp = true,
                AuthenticationMode = Microsoft.Owin.Security.AuthenticationMode.Active,
                AuthenticationType = "Bearer",
                AuthorizeEndpointPath = new PathString("/oauth2/authorize"),//http://localhost:50573/oauth2/authorize?response_type=token&client_id=_auth2.0Client&redirect_uri=http://localhost:50573/api/values/ 主要是授权码和客户端模式
                TokenEndpointPath = new PathString("/oauth2/token"),//
                AccessTokenExpireTimeSpan = new TimeSpan(3, 0, 0),
                AccessTokenFormat = jwtDataFormat,//自定义
                RefreshTokenFormat = jwtDataFormat,
                //AccessTokenProvider = new AuthenticationTokenProvider()
                //{
                //},
                Provider = new OAuth.JwtAuthorizationServerProvider(tokenValidationParameters),
                ApplicationCanDisplayErrors = true,
            });


        }
    }
}
