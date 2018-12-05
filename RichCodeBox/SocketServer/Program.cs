using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace SocketServer
{
    class Program
    {

        /// <summary>
        /// Socket-Server
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            try
            {
                //TcpListenerCall();//测试TcpListener

                Console.Title = "服务端";
                System.Threading.Thread thread = new System.Threading.Thread((state) =>
                {
                    var localEndPoint = new IPEndPoint(IPAddress.Parse("10.10.10.12"), 1861);

                    using (Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
                    {
                        try
                        {
                            socket.Bind(localEndPoint);
                            socket.Listen(1);//连接远程
                            Println("正在等待远程客户端接入...");
                            while (true)
                            {
                                var acceptSocket = socket.Accept();
                                System.Threading.ThreadPool.QueueUserWorkItem((o) =>
                                {
                                    Socket recieveSocket = (Socket)o;
                                    //循环发送消息
                                    while (true)
                                    {
                                        try
                                        {
                                            Println($"远程主机{recieveSocket.RemoteEndPoint}已连接", true);
                                            byte[] buffer = System.Text.Encoding.UTF8.GetBytes("www.lichaoqiang.com");

                                            //发送消息
                                            recieveSocket.Send(buffer);

                                            //接收客户端发送的消息
                                            byte[] recieveData = new byte[recieveSocket.Available];
                                            recieveSocket.Receive(recieveData);
                                            Console.WriteLine(System.Text.Encoding.UTF8.GetString(recieveData));

                                            System.Threading.Thread.Sleep(300);
                                        }
                                        catch (System.Net.Sockets.SocketException ex)
                                        {
                                            if (ex.SocketErrorCode == SocketError.ConnectionReset ||
                                                ex.SocketErrorCode == SocketError.ConnectionAborted) Println("连接被关闭！");
                                            recieveSocket.Dispose();
                                            Console.WriteLine(ex.Message);
                                            break;
                                        }
                                        finally
                                        {

                                        }
                                    }
                                }, acceptSocket);
                            }
                        }
                        catch (System.Net.Sockets.SocketException ex)
                        {
                            if (ex.SocketErrorCode == SocketError.ConnectionReset ||
                                ex.SocketErrorCode == SocketError.ConnectionAborted) Println("连接被关闭！");
                            socket.Dispose();
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
        /// /
        /// </summary>
        static void TcpListenerCall()
        {
            TcpListener tcpListener = new TcpListener(IPAddress.Parse("10.10.10.12"), 1861);
            try
            {
                Console.Title = "服务端-Server";
                System.Threading.Thread thread = new System.Threading.Thread((state) =>
                {
                    tcpListener.Start();
                    Console.Title = "正在等待连接...";
                    var socket = tcpListener.AcceptSocket();
                    while (true)
                    {
                        Console.Title = "客户端已连接！";
                        Console.WriteLine($"{DateTime.Now}");
                        byte[] buffer = System.Text.Encoding.UTF8.GetBytes("www.lichaoqiang.com");
                        socket.Send(buffer);

                        System.Threading.Thread.Sleep(300);
                    }
                });
                thread.Start();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                tcpListener.Stop();
            }
            finally
            {
                Console.Title = "服务已关闭！";
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
