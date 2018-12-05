using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace SerializerApp
{
    class Program
    {
        static void Main(string[] args)
        {

            var task = CallAsync();
            Console.WriteLine($"[{System.Threading.Thread.CurrentThread.ManagedThreadId}] {DateTime.Now}");
            //CallAsync().Wait();
            task.Wait();
            //MyXmlSerializer xmlSerializer = new MyXmlSerializer(typeof(User));
            //User user = new User { Id = 1001, Name = "lichaoqiang" };
            //StringWriter sw = new StringWriter();
            //xmlSerializer.Serialize(sw, user);

            //Console.WriteLine(sw);
            Console.ReadLine();
        }

        static async Task<string> CallAsync()
        {
            var str = await Task.Run<string>(() =>
            {
                string strOutput = $"[{System.Threading.Thread.CurrentThread.ManagedThreadId}] lichaoqiang";
                Console.WriteLine(strOutput);
                return strOutput;
            });
            return str;
        }
    }



    [XmlRoot("xml")]
    public class User
    {
        /// <summary>
        /// 
        /// </summary>
        public long Id { get; set; }


        /// <summary>
        /// 
        /// </summary>

        public string Name { get; set; }
    }

    public class MyXmlSerializer : XmlSerializer
    {
        public MyXmlSerializer(Type type) : base(type)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="o"></param>
        new public void Serialize(TextWriter writer, object o)
        {
            var myXmlWriter = CreateWriter(writer);
            Serialize(o, myXmlWriter);

        }



        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected XmlSerializationWriter CreateWriter(TextWriter writer)
        {
            var myXmlWriter = new MyXmlWriter(writer);
            return myXmlWriter;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="o"></param>
        /// <param name="writer"></param>
        protected override void Serialize(object o, XmlSerializationWriter writer)
        {
            ((MyXmlWriter)writer).Write(o);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class MyXmlWriter : XmlSerializationWriter
    {

        /// <summary>
        /// 
        /// </summary>
        protected StringBuilder stbXml = null;

        TextWriter textWriter = null;

        /// <summary>
        /// 
        /// </summary>
        public MyXmlWriter(TextWriter writer)
        {
            stbXml = new StringBuilder();
            var xmlSetting = new XmlWriterSettings();

            Writer = XmlWriter.Create(stbXml, xmlSetting);
            textWriter = writer;
        }

        /// <summary>
        /// 
        /// </summary>
        protected override void InitCallbacks()
        {

        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="o"></param>
        public void Write(object o)
        {

            Writer.WriteStartElement("ToUserName");
            Writer.WriteCData("lichaoqiang");
            Writer.WriteEndElement();
            Writer.Flush();
            textWriter.Write(stbXml);
        }


    }
}
