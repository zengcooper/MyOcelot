using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Thrift.Protocol;
using Thrift.Transport;

namespace ThriftClient
{
    class Program
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            Console.Title = "Thrift客户端-Client";
            TTransport transport = new TSocket("10.10.10.19", 10501);
            TProtocol protocol = new TBinaryProtocol(transport);
            UserService.Client client = new UserService.Client(protocol);
            transport.Open();
            //var users = client.GetAllUser();

            //users.ForEach(u => Console.WriteLine(string.Format("User ID : {0}, User Name {1}", u.ID, u.Name)));
            var user = client.GetUserByID(1);
            Console.WriteLine("------------------");
            Console.WriteLine(string.Format("User ID : {0}, User Name {1}", user.ID, user.Name));
            Console.ReadLine();
        }
    }
}
