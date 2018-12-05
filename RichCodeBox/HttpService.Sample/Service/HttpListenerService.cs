using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace HttpService.Sample.Service
{
    /// <summary>
    ///<![CDATA[http监听服务]]>
    /// </summary>
    internal class HttpListenerService
    {
        /// <summary>
        /// <![CDATA[构造函数]]>
        /// </summary>
        public HttpListenerService()
        {

        }

        /// <summary>
        /// <![CDATA[启动服务]]>
        /// </summary>
        /// <param name="args"></param>
        public void OnStart(string[] args)
        {
            Console.Title = "HTTP 题库同步服务！";
            //http listener
            int iPort = 3332;
            System.Net.HttpListener httpListener = new HttpListener();
            string strPrefixes = string.Format("http://*:{0}/", iPort);
            httpListener.Prefixes.Add(strPrefixes);
            httpListener.ExtendedProtectionSelectorDelegate += (o) =>
            {
                o.Headers["Server"] = "DTK-Service";
                o.Headers["Access-Control-Allow-Origin"] = "*";
                return null;
            };
            try
            {
                httpListener.Start();
                System.Threading.ThreadPool.SetMinThreads(10, 3);
                System.Threading.ThreadPool.SetMaxThreads(1000, 512);

                //创建10个队列
                for (int i = 0; i < 10; i++)
                {
                    System.Threading.ThreadPool.QueueUserWorkItem((state) => { Handle(httpListener); });
                }
                Console.WriteLine("HTTP监听服务启动成功！");
                Console.WriteLine("访问地址：{0}", strPrefixes);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                if (httpListener.IsListening)
                {
                    httpListener.Stop();
                }
            }
        }

        /// <summary>
        /// <![CDATA[服务停止]]>
        /// </summary>
        protected void OnStop()
        {

        }

        #region 私有方法  
        /// <summary>
        /// <![CDATA[异步方法]]>
        /// </summary>
        /// <param name="httpListener"></param>
        /// <returns></returns>
        private void Handle(System.Net.HttpListener httpListener)
        {
            while (true)
            {
                HttpListenerContext context = httpListener.GetContext();
                HttpListenerRequest request = context.Request;
                context.Response.Headers["Server"] = "DTK-Server";
                if (httpListener.IsListening && request != null)
                {
                    HttpListenerResponse response = context.Response;
                    Console.WriteLine("#处理线程{0} 请求ULR：{1}", System.Threading.Thread.CurrentThread.ManagedThreadId, request.Url);

                    string responseString = "This is HttpListener Service!";//默认输出文本
                    response.KeepAlive = true;
                    response.ContentEncoding = System.Text.Encoding.UTF8;
                    try
                    {
                        WriteJson(response, 200, responseString);//默认输出
                    }
                    catch (ArgumentNullException ex)
                    {
                        WriteJson(response, 0, ex.Message);
                        Console.WriteLine("{0}", ex.Message);
                        continue;
                    }
                    catch (InvalidOperationException ex)
                    {
                        WriteJson(response, 0, ex.Message);
                        Console.WriteLine("{0}", ex.Message);
                        continue;
                    }
                    catch (Exception ex)
                    {
                        WriteJson(response, 0, ex.Message);
                        Console.WriteLine("{0}", ex.Message);
                        continue;
                    }
                    finally
                    {

                    }
                }
            }
        }




        /// <summary>
        /// <![CDATA[写入响应]]>
        /// </summary>
        /// <param name="response"></param>
        /// <param name="html"></param>
        private void WriteResponse(HttpListenerResponse response, string html)
        {
            response.ContentType = "text/html;charset=utf8";

            using (System.IO.Stream output = response.OutputStream)
            {
                string strHtml = html;
                WriteHtmlDocument(ref strHtml);//写入HTML格式数据
                byte[] error = System.Text.Encoding.UTF8.GetBytes(strHtml);
                response.ContentLength64 = error.Length;
                output.Write(error, 0, error.Length);
                output.Flush();
            }
            response.Close();
        }

        /// <summary>
        /// <![CDATA[写入HTML]]>
        /// </summary>
        /// <param name="html"></param>
        private void WriteHtmlDocument(ref string html)
        {
            StringBuilder stbHtml = new StringBuilder("<html>");
            stbHtml.AppendLine("<head>");
            stbHtml.AppendLine("<meta http-equiv=Content-Type content=\"text/html;charset=utf-8\">");
            stbHtml.AppendLine("<title>HTTP服务</title>");
            stbHtml.AppendLine("</head>");
            stbHtml.AppendLine("<body>");
            stbHtml.AppendLine(html);
            stbHtml.AppendLine("</body>");
            stbHtml.AppendLine("</html>");
            html = stbHtml.ToString();
        }

        /// <summary>
        /// 写入JSON数据
        /// </summary>
        /// <param name="response"></param>
        /// <param name="status"></param>
        /// <param name="message"></param>
        private void WriteJson(HttpListenerResponse response, int status, string message, object sbj = null)
        {
            response.Headers["Access-Control-Allow-Origin"] = "*";
            response.ContentType = "application/json;charset=utf8";
            var data = new { status = status, message = message, data = sbj };
            string strJson = Newtonsoft.Json.JsonConvert.SerializeObject(data);
            using (System.IO.Stream output = response.OutputStream)
            {
                byte[] buffer = System.Text.Encoding.UTF8.GetBytes(strJson);
                response.ContentLength64 = buffer.Length;
                output.Write(buffer, 0, buffer.Length);
                output.Flush();
            }
            response.Close();
        }
        #endregion 私有方法
    }
}
