using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ImplicitGrant.Sample.Controllers
{

    /// <summary>
    /// 
    /// </summary>
    public class HomeController : Controller
    {

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult> Index()
        {
            try
            {
                ViewBag.Title = "简化模式";
                string strAuthrizeUri = $"{Request.Url.AbsoluteUri}/oauth2/Authorize?client_id=_clientId&redirect_uri=" + Uri.EscapeUriString($"{Request.Url.AbsoluteUri}OAuth2/GetAccessToken/") + "&response_type=token&state=state&scope=scope1";

                //这里用httpClient对象发起授权请求，获取访问令牌
                HttpClient httpClient = new HttpClient();
                HttpRequestMessage httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, strAuthrizeUri);
                HttpResponseMessage httpResponseMessage = await httpClient.SendAsync(httpRequestMessage);
                NameValueCollection nameValueCollection = ParseRedirectFragment(httpResponseMessage);
                string strAccessToken = nameValueCollection.Get("access_token");
                int expire_in = Convert.ToInt32(nameValueCollection.Get("expires_in"));
                ViewBag.access_token = strAccessToken;

            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult GetToken()
        {
            string strQueryString = Request.QueryString.ToString();
            return new EmptyResult();
        }

        /// <summary>
        ///解析Uri片段，获取访问令牌
        /// </summary>
        /// <param name="response"></param>
        /// <returns></returns>
        public NameValueCollection ParseRedirectFragment(HttpResponseMessage response)
        {
            string fragment = response.RequestMessage.RequestUri.Fragment.Substring(1);
            var nvc = new NameValueCollection();
            foreach (var pair in fragment
                .Split(new[] { '&' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(x => x.Split(new[] { '=' }, 2).Select(Uri.UnescapeDataString)))
            {
                if (pair.Count() == 2)
                {
                    nvc.Add(pair.First(), pair.Last());
                }
            }
            return nvc;
        }
    }
}
