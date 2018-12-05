using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ImplicitGrant.Sample.Controllers
{

    /// <summary>
    /// 
    /// </summary>
    public class OAuth2Controller : Controller
    {

        /// <summary>
        ///<![CDATA[获取访问令牌]]>
        /// </summary>
        /// <returns></returns>
        public HttpResponseMessage GetAccessToken()
        {
            var url = Request.Url;
            return new HttpResponseMessage()
            {
                Content = new StringContent("", Encoding.UTF8, "text/plain")
            };
        }
    }
}