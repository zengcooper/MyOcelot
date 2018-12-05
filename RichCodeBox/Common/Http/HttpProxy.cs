using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace Common.Http
{
    /// <summary>
    /// <![CDATA[HTTP服务器代理]]>
    /// </summary>
    public class HttpProxy
    {
        #region Member
        /// <summary>
        /// <![CDATA[配置应用序列化常用方法]]>
        /// </summary>
        public Func<string, object> SerializeFunc { get; set; }


        /// <summary>
        /// <![CDATA[获取请求URL信息]]>
        /// </summary>
        public string Url { get; private set; }

        /// <summary>
        /// <![CDATA[参数]]>
        /// </summary>
        protected SortedDictionary<string, object> Parameters = null;

        /// <summary>
        /// <![CDATA[索引器]]>
        /// </summary>
        /// <param name="name">参数名</param>
        /// <returns></returns>
        public object this[string name] { get { return Parameters[name]; } set { Parameters[name] = value; } }


        /// <summary>
        /// <![CDATA[获取HttpWebRequest对象]]>
        /// </summary>
        private HttpWebRequest Request { get; set; }

        /// <summary>
        /// <![CDATA[Accept]]>
        /// </summary>
        public virtual string Accept { get; set; }

        /// <summary>
        /// <![CDATA[HTTP相应状态]]>
        /// </summary>
        public virtual HttpStatusCode HttpStatusCode { get; protected set; }

        /// <summary>
        /// <![CDATA[超时时间]]>
        /// </summary>
        public int Timeout { get; set; }

        /// <summary>
        /// <![CDATA[请求内容类型]]>
        /// </summary>
        public string ContentType { get; set; }

        /// <summary>
        /// <![CDATA[代理]]>
        /// </summary>
        public string UserAgent { get; set; }




        /// <summary>
        /// <![CDATA[Cookie容器]]>
        /// </summary>
        public CookieContainer CookieContainer { get; set; }


        /// <summary>
        ///<![CDATA[构造函数]]>
        /// </summary>
        protected HttpProxy()
        {
            Parameters = new SortedDictionary<string, object>();
            ContentType = "application/json;charset=utf-8";
            Accept = "application/json,text/html,*/*";
            UserAgent = "Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/61.0.3163.100 Safari/537.36";
            Timeout = 60000;
        }

        /// <summary>
        /// <![CDATA[构造函数]]>
        /// </summary>
        /// <param name="url">请求的URL</param>
        public HttpProxy(string url)
            : this()
        {
            Url = url;
        }
        #endregion


        /// <summary>
        ///<![CDATA[以GET方式： 获取接口响应流]]>
        /// </summary>
        /// <returns><![CDATA[返回响应流]]></returns>
        public virtual Stream GetResponseStream()
        {
            System.GC.Collect();
            MakeUrl(ToQueryString(Parameters));
            //2>发起请求
            Request = (HttpWebRequest)WebRequest.Create(Url);
            HttpWebResponse response = null;
            Request.Timeout = Timeout;
            Request.ContentType = this.ContentType;
            Request.Method = "GET";
            Request.Accept = Accept;
            Request.KeepAlive = true;
            Request.UserAgent = UserAgent;
            try
            {
                if (null != CookieContainer) Request.CookieContainer = CookieContainer;//COOKIE
                response = (HttpWebResponse)Request.GetResponse();
                HttpStatusCode = response.StatusCode;
                return response.GetResponseStream();
            }
            catch (System.Net.WebException exception)
            {
                HttpStatusCode = (exception.Response as HttpWebResponse).StatusCode;
                throw new System.Exception("以GET方式： 获取接口响应流时，发生网络错误！", exception);
            }
            finally
            {
                ////////////////////////////
                ///http://www.lichaoqiang.com/
                ///////////////////////////
            }
        }

        private string ToQueryString(SortedDictionary<string, object> parameters)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// <![CDATA[以GET方式： 获取接口响应流]]>
        /// </summary>
        /// <returns><![CDATA[返回响字符串]]></returns>
        public virtual string Get()
        {
            string strResponseText = string.Empty;
            using (StreamReader stream = new StreamReader(GetResponseStream()))
            {
                strResponseText = stream.ReadToEnd();
            }
            return strResponseText;
        }

        /// <summary>
        /// <![CDATA[HTTP POST]]>
        /// </summary>
        /// <param name="postData"><![CDATA[Post数据,格式如：a=1&b=2]]></param>
        /// <returns><![CDATA[返回响应流]]></returns>
        public virtual Stream PostResponseStream()
        {
            System.GC.Collect();
            //1>处理URL
            Request = (HttpWebRequest)WebRequest.Create(this.Url);
            HttpWebResponse response = null;
            Request.Method = "POST";
            Request.Timeout = this.Timeout;
            Request.ContentType = "application/x-www-form-urlencoded";
            Request.Accept = this.Accept;
            Request.UserAgent = this.UserAgent;
            try
            {
                if (null != CookieContainer) Request.CookieContainer = CookieContainer;//COOKIE
                string strFormEncodeData = ToQueryString(parameters: Parameters);
                //将POST数据写入请求流
                if (!string.IsNullOrEmpty(strFormEncodeData))
                {
                    using (var stream = Request.GetRequestStream())
                    {
                        var buffer = Encoding.UTF8.GetBytes(strFormEncodeData);
                        stream.Write(buffer, 0, buffer.Length);
                    }
                }
                response = (HttpWebResponse)Request.GetResponse();
                HttpStatusCode = response.StatusCode;
                return response.GetResponseStream();
            }
            catch (System.Net.WebException exception)
            {
                HttpStatusCode = (exception.Response as HttpWebResponse).StatusCode;
                throw new System.Exception("Post请求接口时： 获取接口响应流时，发生网络错误！", exception);
            }
            finally
            {

            }
        }

        /// <summary>
        /// <![CDATA[POST]]>
        /// </summary>
        /// <param name="data"><![CDATA[参数字典]]></param>
        /// <returns></returns>
        public virtual string Post()
        {
            string strResponseBody = string.Empty;//响应内容
            using (StreamReader sr = new StreamReader(PostResponseStream()))
            {
                strResponseBody = sr.ReadToEnd();
            }
            return strResponseBody;
        }


        /// <summary>
        /// <![CDATA[把一个key，value放入参数字典]]>
        /// </summary>
        /// <param name="key"><![CDATA[key]]></param>
        /// <param name="value"><![CDATA[value]]></param>
        public virtual void PutData(string key, object value)
        {
            Parameters[key] = value;
        }

        #region 派生方法
        /// <summary>
        /// <![CDATA[转换查询字符串]]>
        /// </summary>
        /// <param name="parameters"><![CDATA[参数字典]]></param>
        /// <returns></returns>
        protected virtual string ToQueryString(IDictionary parameters)
        {
            if (null == parameters || parameters.Count == 0) return null;
            var enumerator = parameters.GetEnumerator();
            StringBuilder stbUrl = new StringBuilder();
            while (enumerator.MoveNext())
            {
                stbUrl.AppendFormat("{0}={1}", enumerator.Key, enumerator.Value);
                stbUrl.Append("&");
            }
            string strQueryString = string.Empty;
            strQueryString = stbUrl.ToString().TrimEnd('&');
            return strQueryString;
        }

        /// <summary>
        /// <![CDATA[组合生成URL]]>
        /// </summary>
        /// <param name="query"><![CDATA[查询字符串（a=1&b=2）]]></param>
        /// <returns></returns>
        protected virtual void MakeUrl(String query)
        {
            if (String.IsNullOrEmpty(query)) return;
            UriBuilder ubl = new UriBuilder(this.Url);
            if (!string.IsNullOrEmpty(ubl.Query.Trim('?')))
            {
                ubl.Query = ubl.Query.TrimStart('?') + "&" + query.Trim('&');
            }
            else
            {
                ubl.Query = query;
            }
            Url = string.Format("{0}://{1}{2}{3}{4}", ubl.Scheme, ubl.Host, (ubl.Port != 80) ? ":" + ubl.Port : String.Empty, ubl.Path, ubl.Query).TrimEnd('?');
        }
        #endregion
    }
}
