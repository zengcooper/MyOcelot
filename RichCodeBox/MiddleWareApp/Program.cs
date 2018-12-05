using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using MiddleWareApp.MiddleWare;
using MiddleWareApp.Extension;

namespace MiddleWareApp
{
    class Program
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {

            AppBuilder appBuilder = new AppBuilder();
            Configure(appBuilder);
            appBuilder.Build();

            Console.ReadLine();
        }


        public void Test() { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="app"></param>
        static void Configure(IAppBuilder app)
        {

            //1>
            app.Use(new MiddleWare.CustomMiddleWare());

            //2>
            app.Use<MiddleWare.StaticMiddleWare>();

            //3>
            app.Use(typeof(MiddleWare.OAuthMiddleWare));
        }
    }
}
