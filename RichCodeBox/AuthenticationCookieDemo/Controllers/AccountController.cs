using AuthenticationCookieDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using System.Security;
using System.Security.Claims;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;

namespace AuthenticationCookieDemo.Controllers
{
    public class AccountController : Controller
    {
        /// <summary>
        /// 
        /// </summary>
        private Microsoft.Owin.Security.IAuthenticationManager authenticationManager = null;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="requestContext"></param>
        protected override void Initialize(RequestContext requestContext)
        {
            IOwinContext context = requestContext.HttpContext.GetOwinContext();
            authenticationManager = requestContext.HttpContext.GetOwinContext().Authentication;
            base.Initialize(requestContext);
        }

        // GET: Account
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
            if (Request.IsAuthenticated)
            {
                returnUrl = string.IsNullOrEmpty(returnUrl) ? "/" : returnUrl;
                return Redirect(returnUrl);
            }
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        [HttpPost]
        public ActionResult Login(LoginViewModel model)
        {
            try
            {
                if (string.IsNullOrEmpty(model.account)) { throw new ArgumentNullException("account"); }
                if (string.IsNullOrEmpty(model.password)) { throw new ArgumentNullException("password"); }
                //声明
                Claim[] claims =
                {
                new Claim(ClaimTypes.Name,model.account),
                new Claim(ClaimTypes.NameIdentifier,model.account),
                new Claim(ClaimTypes.Role,"normal"),
                };

                //注意： DefaultAuthenticationTypes.ApplicationCookie否则会导致Cookie无法写入
                System.Security.Claims.ClaimsIdentity claimsIdentity = new System.Security.Claims.ClaimsIdentity(claims, DefaultAuthenticationTypes.ApplicationCookie);
                authenticationManager.SignIn(new Microsoft.Owin.Security.AuthenticationProperties()
                {
                    RedirectUri = "http://www.baidu.com/",
                    IsPersistent = true

                }, claimsIdentity);

                return Redirect("/home/");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex);
            }
            return View(model);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Logout()
        {
            return View();
        }
    }
}