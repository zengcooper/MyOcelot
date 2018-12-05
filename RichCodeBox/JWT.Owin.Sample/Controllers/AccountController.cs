using JWT.Owin.Sample.Models;
using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JWT.Owin.Sample.Controllers
{

    /// <summary>
    /// 
    /// </summary>
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        
        public ActionResult Login()
        {
            LoginViewModel model = new LoginViewModel()
            {
                client_id = OAuthCommon.Constants.Consts.Client.ClientId,
                grant_type = "password"
            };
            return View(model);
        }


    }
}