using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using WebSocketSharp;

namespace WebSocket.Sample
{
    class Program
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            WebSocketSharp.Server.WebSocketServer webSocket = null;
            try
            {
                webSocket = new WebSocketSharp.Server.WebSocketServer(IPAddress.Parse("127.0.0.1"), 9999);
                {
                    webSocket.AddWebSocketService<ChatWebSocketService>("/chat");
                    webSocket.Start();
                    Console.Title = "服务已启动";
                    while (true)
                    {
                        System.Threading.Thread.Sleep(1000);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                webSocket.Stop();
            }
            finally
            {
                Console.ReadLine();
            }
        }
    }

    public class ChatWebSocketService : WebSocketSharp.Server.WebSocketBehavior
    {


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected override Task OnOpen()
        {
            Console.Title = "已连接";
            Console.WriteLine("连接已打开！");
            return base.OnOpen();
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        protected override Task OnMessage(MessageEventArgs e)
        {
            var dictQuery = TakeQueryString();
            string id = string.Empty;
            var data = new Domain.Result() { id = this.Id };
            if (dictQuery.TryGetValue("id", out id))
            {
                data.id = id;
                data.data = "this is a message by client id!";
                this.Sessions.SendTo(id, Newtonsoft.Json.JsonConvert.SerializeObject(data));
                return Task.Delay(0);
            }

            string strRecieve = e.Text.ReadToEnd();
            data.data = strRecieve;
            Console.WriteLine(strRecieve);
            Send(Newtonsoft.Json.JsonConvert.SerializeObject(data));
            return base.OnMessage(e);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private Dictionary<string, string> TakeQueryString()
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            var query = this.Context.GetRequestUri().Result.Query;
            if (string.IsNullOrEmpty(query)) return dict;

            var data = query.Split('?')[1].Split('&');
            foreach (var item in data)
            {
                var arrays = item.Split('=');
                dict[arrays[0]] = arrays[1];
            }
            return dict;
        }
    }
}
