using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RestSharp;
using DotNetOpenAuth.OAuth2;

/*=================================================================================================
*
* Title:OAuth2.0授权示例程序
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
namespace OAuthCode.Controllers
{
    /// <summary>
    /// 请求示例：http://localhost:3335/OAuth/Authorize?redirect_uri=http://localhost:3335/home/getcode&response_type=code&client_id=asp
    /// </summary>
    public class HomeController : Controller
    {

        /// <summary>
        ///默认主页
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            ViewBag.Title = "OAuth2.0授权示例程序";

            //RestSharp.RestClient client = new RestClient("http://localhost:3335/");
            //RestSharp.RestRequest request = new RestRequest("/OAuth/Authorize");
            //request.AddParameter("redirect_uri", "http://localhost:3335/");
            //request.AddParameter("client_id", "892f98d2af8d2afd5f2ds5fe2");
            //request.AddParameter("response_type", "code");


            //ISSUE HTTPS
            //var response = client.Execute(request);
            //string strResponseText = response.Content;

            //var authServer = new AuthorizationServerDescription
            //{
            //    AuthorizationEndpoint = new Uri("http://localhost:3335/OAuth/Authorize"),
            //    TokenEndpoint = new Uri("http://localhost:3335/OAuth/Token"),

            //};

            //var autoServerClient = new DotNetOpenAuth.OAuth2.WebServerClient(authServer, clientIdentifier: "fNm0EDIXbfuuDowUpAoq5GTEiywV8eg0TpiIVnV8", clientSecret: "clientSecret");
            //autoServerClient.RequestUserAuthorization(new string[] { "scope1" });
            //var authorizationState = autoServerClient.ProcessUserAuthorization();
            //if (authorizationState != null)
            //{
            //    if (!string.IsNullOrEmpty(authorizationState.AccessToken))
            //    {
            //        var token = authorizationState.AccessToken;
            //    }
            //}
            return View();
        }
    }
}
