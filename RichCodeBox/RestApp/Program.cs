using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace RestApp
{
    class Program
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {

            try
            {
                Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

        /// <summary>
        /// 
        /// </summary>
        static void Rest()
        {
            var client = new RestSharp.RestClient("http://www.kaola100.com/");
     
        }

        /// <summary>
        /// 
        /// </summary>
        static void Post()
        {
            string strApiUri = "http://www.oschina.net/";

            HttpClientHandler handler = new HttpClientHandler();

            //Web代理
            WebProxy proxy = new WebProxy("117.90.5.64", 9000);
            handler.UseProxy = true;
            handler.Proxy = proxy;

            HttpClient client = new HttpClient(handler);

            Dictionary<string, string> formData = new Dictionary<string, string>();
            formData["mobile"] = "15969696969";

            HttpContent content = new FormUrlEncodedContent(formData);
            HttpRequestMessage requetMessage = new HttpRequestMessage(HttpMethod.Post, strApiUri);
            requetMessage.Content = content;

            HttpResponseMessage responseMessage = client.SendAsync(requetMessage).Result;
            string strResponseText = responseMessage.Content.ReadAsStringAsync().Result;

            Console.WriteLine(strResponseText);
        }
    }
}
