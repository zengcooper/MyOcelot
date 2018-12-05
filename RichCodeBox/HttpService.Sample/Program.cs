using HttpService.Sample.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpService.Sample
{

    /// <summary>
    /// http服务
    /// </summary>
    class Program
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            HttpListenerService httpListenerService = new HttpListenerService();
            httpListenerService.OnStart(args);
            Console.ReadLine();
        }
    }
}
