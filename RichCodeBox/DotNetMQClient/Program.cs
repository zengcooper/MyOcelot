using MDS.Client;
using System;

namespace DotNetMQClient
{

    /// <summary>
    /// 
    /// </summary>
    class Program
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            while (true)
            {
                //实例化客户端
                var mdsClient = new MDSClient("SyncService", "127.0.0.1", 10905);
             
                //链接SERVER
                mdsClient.Connect();
                var message = mdsClient.CreateMessage();
                message.MessageData = System.Text.Encoding.UTF8.GetBytes(Guid.NewGuid().ToString());
                message.Send();
                Console.WriteLine("00");
                mdsClient.Disconnect();
                System.Threading.Thread.Sleep(1000);
            }

        }
    }
}