using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MiddleWareApp.MiddleWare;

/******************************************************************************************************************
 * 
 * 
 * 说　明： 授权(版本：Version1.0.0)
 * 作　者：李朝强
 * 日　期: 2017-03-31 12:55:22
 * 修　改：2017-03-31 12:55:22
 * 参　考：http://my.oschina.net/lichaoqiang/ http://www.lichaoqiang.com
 * 备　注：暂无...
 * 
 * 
 * ***************************************************************************************************************/
namespace MiddleWareApp.Extension
{

    /// <summary>
    /// 
    /// </summary>
    public static class AppBuilderExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="app"></param>
        public static void UseCustomMiddleWare(this IAppBuilder app)
        {
            Console.WriteLine("UseCustomMiddleWare");
            CustomMiddleWare cm = new CustomMiddleWare(null);
            cm.Invoke();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="app"></param>
        /// <param name="args"></param>
        public static void Use<T>(this IAppBuilder app, params object[] args)
            where T : new()
        {
            app.Use(new T(), args);
        }
    }
}
