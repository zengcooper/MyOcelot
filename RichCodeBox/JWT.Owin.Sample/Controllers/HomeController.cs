using JWT.Owin.Sample.Models;
using System.Web.Mvc;

namespace JWT.Owin.Sample.Controllers
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
            ViewBag.Title = "基于Owin的JWT授权示例程序";
            HomeViewModel model = new HomeViewModel()
            {
                client_id = OAuthCommon.Constants.Consts.Client.ClientId,
                grant_type = "password",
                scope = "scop1",
                password = "admin",
                username = "admin"
            };
            return View(model);
        }
    }
}
