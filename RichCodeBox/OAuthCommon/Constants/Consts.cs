using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OAuthCommon.Constants
{
    /// <summary>
    /// 
    /// </summary>
    public static class Consts
    {

        /// <summary>
        /// 
        /// </summary>
        public static class Client
        {
            /// <summary>
            /// clientid
            /// </summary>
            public const string ClientId = "_auth2.0Client";


            /// <summary>
            /// 听众
            /// </summary>
            public const string Audience = "_audience_test";



        }

        /// <summary>
        /// 
        /// </summary>
        public static class JWT
        {
            /// <summary>
            /// 
            /// </summary>
            public static class Security
            {
                public const string SecretKey = "IxrAjDoa2FqElO7IhrSrUJELhUckePEPVpaePlS_Xaw";
            }
        }
    }
}
