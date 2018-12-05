using System.Web.Mvc;

namespace H5.Controllers
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
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Contact()
        {
            var files = Request.Files;
            var file = files["media"];
            file.SaveAs(@"D:\XXXXXXXXX.jpg");
            ViewBag.Message = "Your contact page.";

            return View();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="callback"></param>
        /// <returns></returns>
        public JavaScriptResult SignIn(string callback)
        {
            return JavaScript($"{callback}({{id:1,name:'dddddd'}})");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult ImageEditor()
        {
            var request = Request;
            return View();
        }
    }
}