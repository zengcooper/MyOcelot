using System;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace SocketClient
{
    class Program
    {

        /// <summary>
        /// <![CDATA[Socket-Client]]>
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {

            //本地端口1862连接远程渡口1861
            var localEndPoint = new IPEndPoint(IPAddress.Parse("10.10.10.12"), 1862);
            var remoteEndPoint = new IPEndPoint(IPAddress.Parse("10.10.10.12"), 1861);
            try
            {
                //TcpListenerCall();//测试TcpListener 状态：通过
                Console.Title = "客户端";
                Console.WriteLine("按{回车}键连接远程主机");
                Console.ReadKey();
                //开启一个线程
                System.Threading.Thread thread = new System.Threading.Thread((state) =>
                {
                    using (Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
                    {
                        try
                        {
                            socket.Bind(localEndPoint);
                            socket.Connect(remoteEndPoint);//连接远程
                            Console.Title = $"已经连接到远程主机{remoteEndPoint}";
                            while (socket.Connected)
                            {
                                {
                                    //接收消息
                                    byte[] recieveData = new byte[socket.Available];
                                    socket.Receive(recieveData);
                                    string strRecieveText = System.Text.Encoding.UTF8.GetString(recieveData);
                                    if (!string.IsNullOrEmpty(strRecieveText))
                                    {
                                        Console.WriteLine($"发送：{strRecieveText}");
                                        //发送
                                        byte[] sender = System.Text.Encoding.UTF8.GetBytes($"{localEndPoint}   [{DateTime.Now}]接收到：{strRecieveText}");
                                        socket.Send(sender);
                                    }
                                }
                            }
                        }
                        catch (System.Net.Sockets.SocketException ex)
                        {
                            if (ex.SocketErrorCode == SocketError.ConnectionReset ||
                                ex.SocketErrorCode == SocketError.ConnectionAborted) Println("连接被关闭！", true);
                            socket.Dispose();
                        }
                        finally
                        {
                            if (socket.Connected)
                            {
                                socket.Shutdown(SocketShutdown.Both);
                                socket.Close();
                            }
                        }
                    }
                });
                thread.Start();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally { Console.ReadLine(); }
        }


        /// <summary>
        /// 测试tcpClient
        /// </summary>
        static void TcpClientCall()
        {
            var localEndPoint = new IPEndPoint(IPAddress.Parse("10.10.10.12"), 1862);
            var remoteEndPoint = new IPEndPoint(IPAddress.Parse("10.10.10.12"), 1861);
            TcpClient tcpClient = new TcpClient(localEndPoint);
            try
            {
                Console.Title = "客户端-Client";
                Console.WriteLine("{回车}连接远程服务器！");
                Console.Read();
                //开启线程，连接远程主机
                System.Threading.Thread thread = new System.Threading.Thread((state) =>
                {
                    tcpClient.Connect(remoteEndPoint);
                    while (true)
                    {
                        if (tcpClient.Connected)
                        {
                            Console.Title = "已连接！";
                            var socket = tcpClient.Client;
                            {
                                byte[] buffer = new byte[socket.Available];

                                socket.Receive(buffer);
                                string strRecieveText = System.Text.Encoding.UTF8.GetString(buffer);
                                Console.WriteLine(strRecieveText);
                            }
                        }
                        System.Threading.Thread.Sleep(300);
                    }

                });
                thread.Start();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                tcpClient.Close();
            }
            finally
            {
                Console.ReadLine();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="output"></param>
        /// <param name="isTitle"></param>
        static void Println(string output, bool isTitle = false)
        {
            if (isTitle) Console.Title = output;
            Console.WriteLine(output);
        }
    }
}
