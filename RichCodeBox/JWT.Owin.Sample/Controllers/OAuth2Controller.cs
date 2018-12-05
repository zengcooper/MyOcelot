using Microsoft.Owin.Security;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Security.Claims;
using System.Web.Mvc;
using System.Web;

/*=================================================================================================
*
* Title:OAuth2Controller
* Author:李朝强
* Description:模块描述
* CreatedBy:lichaoqiang.com
* CreatedOn:
* ModifyBy:暂无...
* ModifyOn:
* Company:河南天一文化传播股份有限公司
* Blog:http://www.lichaoqiang.com
* Mark:
*
*** ================================================================================================*/
namespace JWT.Owin.Sample.Controllers
{

    /// <summary>
    /// 
    /// </summary>
    public class OAuth2Controller : Controller
    {

        // GET: OAuth2
        public ActionResult Index()
        {
            return View();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Authorize()
        {
            //验证是否登录，如果没有，
            IAuthenticationManager authentication = HttpContext.GetOwinContext().Authentication;
            AuthenticateResult ticket = authentication.AuthenticateAsync(DefaultAuthenticationTypes.ApplicationCookie).Result;
            ClaimsIdentity identity = ticket == null ? null : ticket.Identity;

            if (identity == null)
            {
                //如果没有验证通过，则必须先通过身份验证，跳转到验证方法
                authentication.Challenge();
                return new HttpUnauthorizedResult();
            }
            identity = new ClaimsIdentity();
            List<Claim> claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Name, "admin"));
            claims.Add(new Claim(ClaimTypes.NameIdentifier, "admin"));
            identity = new ClaimsIdentity(identity.Claims, "Bearer");
            //hardcode添加一些Claim，正常是从数据库中根据用户ID来查找添加
            identity.AddClaim(new Claim(ClaimTypes.Role, "Admin"));
            identity.AddClaim(new Claim("MyType", "MyValue"));

            authentication.SignIn(new AuthenticationProperties() { IsPersistent = true, RedirectUri = Request.QueryString["redirect_uri"] }, identity);


            return new EmptyResult();

        }
    }
}