using Microsoft.Owin.Security;
using System.Collections.Generic;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace ClientCredentials.Controllers
{


    /// <summary>
    /// 
    /// </summary>
    public class AuthorizeController : Controller
    {
        public object DefaultAuthenticationTypes { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            //验证是否登录，如果没有，
            IAuthenticationManager authentication = HttpContext.GetOwinContext().Authentication;
            AuthenticateResult ticket = authentication.AuthenticateAsync("client_credentials").Result;
            ClaimsIdentity identity = ticket == null ? null : ticket.Identity;

            //if (identity == null)
            //{
            //    //如果没有验证通过，则必须先通过身份验证，跳转到验证方法
            //    authentication.Challenge();
            //    return new HttpUnauthorizedResult();
            //}
            identity = new ClaimsIdentity();
            List<Claim> claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Name, "admin"));
            claims.Add(new Claim(ClaimTypes.NameIdentifier, "admin"));
            identity = new ClaimsIdentity(identity.Claims, "Bearer");
            //hardcode添加一些Claim，正常是从数据库中根据用户ID来查找添加
            identity.AddClaim(new Claim(ClaimTypes.Role, "Admin"));
            identity.AddClaim(new Claim("MyType", "MyValue"));

            authentication.SignIn(new AuthenticationProperties() { IsPersistent = true }, identity);


            return new EmptyResult();
        }
    }
}