using System;
using System.Collections.Generic;
using System.IdentityModel.Claims;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace OAuthAuthorizationServer.Controllers
{

    /// <summary>
    /// 
    /// </summary>
    public class OAuth2Controller : Controller
    {
        /// <summary>
        /// 认证成功后，返回令牌token
        /// </summary>
        /// <returns></returns>
        public ActionResult Authorize()
        {
            if (Response.StatusCode != 200)
            {
                return View("AuthorizeError");
            }
            //客户端授权模式 grant_type=client_credentials https://tools.ietf.org/html/rfc6749#section-4.4

            /*
               +---------+                                  +---------------+
     |         |                                  |               |
     |         |>--(A)- Client Authentication --->| Authorization |
     | Client  |                                  |     Server    |
     |         |<--(B)---- Access Token ---------<|               |
     |         |                                  |               |
     +---------+                                  +---------------+

             */
            //密码模式 grant_type=password https://tools.ietf.org/html/rfc6749#section-4.3
            var authentication = HttpContext.GetOwinContext().Authentication;
            var ticket = authentication.AuthenticateAsync("Application").Result;
            var identity = ticket != null ? ticket.Identity : null;
            if (identity == null)
            {
                authentication.Challenge("Application");
                return new HttpUnauthorizedResult();
            }

            var scopes = (Request.QueryString.Get("scope") ?? "").Split(' ');

            if (Request.HttpMethod == "POST")
            {
                if (!string.IsNullOrEmpty(Request.Form.Get("submit.Grant")))
                {
                    identity = new ClaimsIdentity(identity.Claims, "Bearer", identity.NameClaimType, identity.RoleClaimType);
                    foreach (var scope in scopes)
                    {
                        identity.AddClaim(new System.Security.Claims.Claim("urn:oauth:scope", scope));
                    }
                    authentication.SignIn(identity);
                }
                if (!string.IsNullOrEmpty(Request.Form.Get("submit.Login")))
                {
                    authentication.SignOut("Application");
                    authentication.Challenge("Application");
                    return new HttpUnauthorizedResult();
                }
            }

            return View();
        }
    }
}