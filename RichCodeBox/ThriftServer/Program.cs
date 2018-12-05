using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Thrift.Server;
using Thrift.Transport;

namespace ThriftServer
{
    class Program
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            Console.Title = "Thrift服务端-Server";
            TServerSocket serverTransport = new TServerSocket(8899, 0, false);
            UserService.Processor processor = new UserService.Processor(new Services.TheUserService());
            TServer server = new TSimpleServer(processor, serverTransport);
            Console.WriteLine("启动服务器，监听端口8899 ...");
            server.Serve();
        }
    }


}
