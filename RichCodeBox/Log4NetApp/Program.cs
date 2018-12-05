using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*=================================================================================================
*
* Title:log4net示例
* Author:李朝强
* Description:模块描述
* CreatedBy:lichaoqiang.com
* CreatedOn:
* ModifyBy:暂无...
* ModifyOn:
* Company:河南天一文化传播股份有限公司
* Blog:http://www.lichaoqiang.com
* Mark:
*
*** ================================================================================================*/
namespace Log4NetApp
{
    class Program
    {

        /// <summary>
        /// 
        /// </summary>
        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(typeof(Program));

        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            log4net.Config.XmlConfigurator.Configure();//可以参考 http://logging.apache.org/log4net/release/config-examples.html
            logger.Error(new { Id = 1, name = "lcq" });
            Console.ReadLine();

        }
    }
}
