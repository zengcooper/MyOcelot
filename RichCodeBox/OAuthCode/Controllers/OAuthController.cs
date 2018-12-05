using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using System.Threading.Tasks;
using DotNetOpenAuth.OAuth2;
/******************************************************************************************************************
* 
* 
* 说　明： 授权(版本：Version1.0.0)
* 作　者：李朝强
* 日　期：2015/05/19
* 修　改：
* 参　考：http://my.oschina.net/lichaoqiang/
* 备　注：暂无...
* 
* 
* ***************************************************************************************************************/
namespace OAuthCode.Controllers
{
    /// <summary>
    /// <![CDATA[验证授权]]>
    /// </summary>
    public class OAuthController : Controller
    {
        /// <summary>
        /// <!--授权-->
        /// </summary>
        /// <returns></returns>
        public ActionResult Authorize()
        {
            //验证是否登录，如果没有，
            IAuthenticationManager authentication = HttpContext.GetOwinContext().Authentication;
            AuthenticateResult ticket = authentication.AuthenticateAsync(DefaultAuthenticationTypes.ApplicationCookie).Result;
            ClaimsIdentity identity = ticket == null ? null : ticket.Identity;

            if (identity == null)
            {
                //如果没有验证通过，则必须先通过身份验证，跳转到验证方法
                authentication.Challenge();
                return new HttpUnauthorizedResult();
            }

            identity = new ClaimsIdentity(identity.Claims, "Bearer");
            //hardcode添加一些Claim，正常是从数据库中根据用户ID来查找添加
            identity.AddClaim(new Claim(ClaimTypes.Role, "Admin"));
            identity.AddClaim(new Claim(ClaimTypes.Role, "Normal"));
            identity.AddClaim(new Claim("MyType", "MyValue"));

            authentication.SignIn(new AuthenticationProperties() { IsPersistent = true }, identity);

            return new EmptyResult();
        }

        /// <summary>
        /// 这是之前测试用的回调地址，不建议使用
        /// </summary>
        /// <returns></returns>
        [Obsolete("这是之前测试用的回调地址，不建议使用")]
        public ActionResult Callback(string code)
        {
            //var authServer = new AuthorizationServerDescription
            //{
            //    AuthorizationEndpoint = new Uri("http://localhost:3335/OAuth/Authorize"),
            //    TokenEndpoint = new Uri("http://localhost:3335/OAuth/Token"),

            //};

            //var autoServerClient = new DotNetOpenAuth.OAuth2.WebServerClient(authServer, clientIdentifier: "fNm0EDIXbfuuDowUpAoq5GTEiywV8eg0TpiIVnV8", clientSecret: "clientSecret");
            //var authorizationState = autoServerClient.ProcessUserAuthorization(Request);
            //if (authorizationState != null)
            //{
            //    if (!string.IsNullOrEmpty(authorizationState.AccessToken))
            //    {
            //        var token = authorizationState.AccessToken;
            //    }
            //}
            return new EmptyResult();
        }

        /// <summary>
        /// 使用DotNetOpenOAuth组件来模拟授权
        /// </summary>
        /// <returns></returns>
        public ActionResult TestAuthorize()
        {

            var authServer = new AuthorizationServerDescription
            {
                AuthorizationEndpoint = new Uri("http://localhost:3335/OAuth/Authorize"),
                TokenEndpoint = new Uri("http://localhost:3335/OAuth/Token")
            };

            var autoServerClient = new DotNetOpenAuth.OAuth2.WebServerClient(authServer, clientIdentifier: "fNm0EDIXbfuuDowUpAoq5GTEiywV8eg0TpiIVnV8", clientSecret: "clientSecret");
            autoServerClient.RequestUserAuthorization(new string[] { "scope1" }, new Uri("http://localhost:3335/OAuth/GetAccessToken"));
            return new EmptyResult();
        }

        /// <summary>
        ///<![CDATA[获取访问令牌]]>
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult> GetAccessToken()
        {

            #region 使用DotNetOpenOAuth获取访问令牌
            //var authServer = new AuthorizationServerDescription
            //{
            //    AuthorizationEndpoint = new Uri("http://localhost:3335/OAuth/Authorize"),
            //    TokenEndpoint = new Uri("http://localhost:3335/OAuth/Token"),

            //};

            //var autoServerClient = new DotNetOpenAuth.OAuth2.WebServerClient(authServer, clientIdentifier: "fNm0EDIXbfuuDowUpAoq5GTEiywV8eg0TpiIVnV8", clientSecret: "clientSecret");
            //var authorizationState = autoServerClient.ProcessUserAuthorization();
            //if (authorizationState != null)
            //{
            //    if (!string.IsNullOrEmpty(authorizationState.AccessToken))
            //    {
            //        var token = authorizationState.AccessToken;
            //    }
            //} 
            #endregion 使用DotNetOpenOAuth获取访问令牌

            #region 根据授权码，获取访问令牌 模拟第三方回调地址 redirect_uri
            string strCode = Request.QueryString["code"];//访问令牌

            if (string.IsNullOrEmpty(strCode) == false)
            {
                HttpClient client = new HttpClient();
                HttpRequestMessage message = new HttpRequestMessage(HttpMethod.Post, "http://localhost:3335/OAuth/Token");

                Dictionary<string, string> dict = new Dictionary<string, string>();
                dict["grant_type"] = "authorization_code";
                dict["client_id"] = "fNm0EDIXbfuuDowUpAoq5GTEiywV8eg0TpiIVnV8";
                dict["client_secret"] = "111111";
                dict["code"] = strCode;
                dict["redirect_uri"] = "http://localhost:3335/OAuth/GetAccessToken";
                dict["scope"] = "scope1";
                message.Content = new FormUrlEncodedContent(dict);

                var response = await client.SendAsync(message);
                string strResponseText = await response.Content.ReadAsStringAsync();
                return Content(strResponseText, "text/javascript");
            }
            #endregion 根据授权码，获取访问令牌

            return Content("invalid code!");
        }
    }
}
