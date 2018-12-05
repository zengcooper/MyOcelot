using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using OAuthCode.Models;


namespace OAuthCode.Controllers
{
    public class AccountController : Controller
    {

        /// <summary>
        /// 
        /// </summary>
        public IAuthenticationManager AuthenticationManager
        {
            get { return HttpContext.GetOwinContext().Authentication; }
        }

        //
        // GET: /Account/
        public ActionResult Index()
        {
            return View();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Login(string returnUrl)
        {
            ViewBag.returnUrl = Uri.EscapeDataString(returnUrl);
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Login(LoginViewModel model, string returnUrl)
        {
            string userId = "1";
            //可以在这里将用户所属的role或者Claim添加到此
            ClaimsIdentity claims = new ClaimsIdentity(new[] {
                new Claim(ClaimTypes.Name, model.account)
                ,new Claim(ClaimTypes.NameIdentifier,userId)
                ,new Claim("http://schemas.microsoft.com/accesscontrolservice/2010/07/claims/identityprovider",userId)},
                DefaultAuthenticationTypes.ApplicationCookie);

            AuthenticationProperties properties = new AuthenticationProperties
            {
                IsPersistent = true
            };

            ClaimsPrincipal principal = new ClaimsPrincipal(claims);
            //System.Threading.Thread.CurrentPrincipal = principal;
            this.AuthenticationManager.SignIn(properties, new[] { claims });

            return Redirect(returnUrl);
        }
    }
}