using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AuthenticationCookieDemo.Controllers
{

    public class user
    {
        public DateTime examDate { get; set; }
    }

    [Authorize]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var date = DateTime.FromFileTimeUtc(1510934400000);

            var unixDate = new DateTime(1971, 1, 1);
            date = unixDate.AddMilliseconds(1510934400000);
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}