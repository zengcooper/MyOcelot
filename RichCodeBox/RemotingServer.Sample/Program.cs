using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.IO.Pipes;



namespace RemotingServer.Sample
{
    class Program
    {
        /// <summary>
        /// 
        /// </summary>
        const string NAMED_PIPE_SERVER = "_testNamedPipeServer";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            Console.Title = "命名管道-服务端";
            using (NamedPipeServerStream namedPipeServerStream = new NamedPipeServerStream(NAMED_PIPE_SERVER))
            {
                try
                {
                    Console.WriteLine("等待客户端连接...");
                    namedPipeServerStream.WaitForConnection();
                    Console.WriteLine("客户端已连接...");
                    while (true)
                    {
                        byte[] buffer = System.Text.Encoding.UTF8.GetBytes("My name is lichaoqiang!");
                        namedPipeServerStream.Write(buffer, 0, buffer.Length);
                        namedPipeServerStream.Flush();
                        System.Threading.Thread.Sleep(2000);
                    }
                }
                catch (System.IO.IOException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            Console.ReadLine();
        }
    }
}
