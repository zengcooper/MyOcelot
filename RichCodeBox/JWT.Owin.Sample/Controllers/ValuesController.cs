using System.Collections.Generic;
using System.Web.Http;

namespace JWT.Owin.Sample.Controllers
{

    /// <summary>
    /// <![CDATA[资源]]>
    /// </summary>
    [Authorize]
    public class ValuesController : ApiController
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<string> Get()
        {
            return new string[] { "受保护的资源-1", "受保护的资源-2" };
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
