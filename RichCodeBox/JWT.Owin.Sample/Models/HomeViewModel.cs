using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JWT.Owin.Sample.Models
{

    /// <summary>
    /// 
    /// </summary>
    public class HomeViewModel
    {

        /// <summary>
        /// 
        /// </summary>
        public string client_id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string username { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string password { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string grant_type { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string scope { get; set; }
    }



}