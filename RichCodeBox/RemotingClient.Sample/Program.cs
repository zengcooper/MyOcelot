using System;
using System.IO.Pipes;

namespace RemotingClient.Sample
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
            Console.Title = "命名管道-客户端";
            using (NamedPipeClientStream namedClientStream = new NamedPipeClientStream("localhost", NAMED_PIPE_SERVER))
            {
                Console.WriteLine("正在连接...");
                try
                {
                    namedClientStream.Connect(30000);
                    Console.WriteLine("成功连接至{0}", NAMED_PIPE_SERVER);
                    while (namedClientStream.CanRead)
                    {
                        byte[] buffer = new byte[78];
                        namedClientStream.Read(buffer, 0, buffer.Length);
                        string strReciveData = System.Text.Encoding.UTF8.GetString(buffer);
                        Console.WriteLine("接收到消息！{0}", strReciveData);
                        System.Threading.Thread.Sleep(1000);
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
