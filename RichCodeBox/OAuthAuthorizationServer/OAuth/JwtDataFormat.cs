using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens;
using System.Linq;
using System.Security.Claims;
using System.Web;

/*=================================================================================================
*
* Title:JwtDataFormat
* Author:李朝强
* Description:JwtDataFormat模块描述
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
    /// 自定义 jwt token 的格式 
    /// </summary>
    public class JwtDataFormat : ISecureDataFormat<Microsoft.Owin.Security.AuthenticationTicket>
    {


        /// <summary>
        /// 
        /// </summary>
        private JwtSecurityTokenHandler securityTokenHandler { get; set; }

        /// <summary>
        /// TokenValidationParameters
        /// </summary>
        public TokenValidationParameters _tokenValidationParameters { get; protected set; }


        /// <summary>
        /// 
        /// </summary>
        private JwtDataFormat()
        {
            securityTokenHandler = new JwtSecurityTokenHandler();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="issuer"></param>
        /// <param name="secret"></param>
        public JwtDataFormat(string issuer, byte[] secret)
            : this()
        {
            _tokenValidationParameters = new TokenValidationParameters();
            _tokenValidationParameters.ValidIssuer = issuer;
            _tokenValidationParameters.IssuerSigningKey = new System.IdentityModel.Tokens.InMemorySymmetricSecurityKey(secret);
            _tokenValidationParameters.ValidateIssuerSigningKey = true;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="tokenValidationParameters"></param>
        public JwtDataFormat(TokenValidationParameters tokenValidationParameters)
              : this()
        {
            _tokenValidationParameters = tokenValidationParameters;
        }

        /// <summary>
        /// <![CDATA[生成令牌]]>
        /// </summary>
        /// <param name="data"><![CDATA[票据]]></param>
        /// <returns></returns>
        public string Protect(AuthenticationTicket data)
        {
            var issued = data.Properties.IssuedUtc;
            var expires = data.Properties.ExpiresUtc;

            //签名证书
            var signingCredentials = new SigningCredentials(_tokenValidationParameters.IssuerSigningKey, SecurityAlgorithms.HmacSha256Signature, SecurityAlgorithms.Sha256Digest);
            var jst = new JwtSecurityToken(_tokenValidationParameters.ValidIssuer,
                                           _tokenValidationParameters.ValidAudience,
                                           data.Identity.Claims,
                                           issued.Value.UtcDateTime,
                                           expires.Value.UtcDateTime,
                                           signingCredentials);
#if DEBUG
            System.Diagnostics.Debug.WriteLine(jst.ToString());
#endif 
            return securityTokenHandler.WriteToken(jst);
        }

        /// <summary>
        /// <![CDATA[从token中解码，返回一个票据信息
        /// 这个类主要是用来加密数据生成Token,所以，不需要实现Uprotect方法，如果要解密，可以使用JwtFormat类
        /// ]]>
        /// </summary>
        /// <param name="protectedText"><![CDATA[受保护的文本，也就是token]]></param>
        /// <returns></returns>
        public AuthenticationTicket Unprotect(string protectedText)
        {
            throw new NotSupportedException();
        }
    }
}