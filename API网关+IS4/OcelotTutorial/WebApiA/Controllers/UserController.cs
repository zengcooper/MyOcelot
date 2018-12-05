using Microsoft.AspNetCore.Mvc;

namespace WebApiA.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]/[action]")]
    public class UserController : Controller
    {
        [HttpGet]
        public string GetSex(string name)
        {
            if (name == "Jonathan")
                return "Man";
            return "women";
        }
        [HttpGet]
        public string GetID(string name)
        {
            if (name == "Jonathan")
                return "8";
            return "0";
        }
        [HttpGet]
        public int? GetAge(string name)
        {
            if (name == "Jonathan")
                return 24;
            return 0;
        }
    }
}