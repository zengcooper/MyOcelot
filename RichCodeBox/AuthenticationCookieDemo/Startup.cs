using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using Microsoft.Owin.Security.Cookies;
[assembly: OwinStartup(typeof(AuthenticationCookieDemo.Startup))]

namespace AuthenticationCookieDemo
{

    /// <summary>
    /// 
    /// </summary>
    public partial class Startup
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="app"></param>
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuthentication(app);
            // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=316888
        }
    }
}
