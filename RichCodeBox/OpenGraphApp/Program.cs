using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenGraphApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var openGraph = OpenGraphNet.OpenGraph.ParseUrl("http://www.baidu.com");
            var a = openGraph.Image;
     

        }
    }
}
