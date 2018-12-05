using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OAuthAuthorizationServer.Controllers
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
        public ActionResult Index()
        {
            ViewBag.Title = "OAuth2.0认证服务器";

            return View();
        }
    }
}
