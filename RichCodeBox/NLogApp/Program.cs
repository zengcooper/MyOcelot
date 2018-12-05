using System;
/**************************************************************************************
 * 
 * 
 * 
 * NLog
 * 配置文件参考：Config/NLog.config
 * github url:https://github.com/NLog
 * 
 * 
 * 
 * 
 * 
 * 
 * ***************************************************************************************/
namespace NLogApp
{

    /// <summary>
    /// NLog的配置及用法示例
    /// </summary>
    class Program
    {
        /// <summary>
        /// 
        /// </summary>
        private static readonly NLog.ILogger logger = NLog.LogManager.GetCurrentClassLogger();


        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            Console.Title = "测试NLog日志";
            logger.Error(new Exception("异常！"), "测试NLog异常日志");
            logger.Info("测试NLog Info");
            logger.Warn("测试NLog Warn");
            logger.Debug("测试NLog Debug");
            Console.WriteLine("执行完毕！");
            Console.ReadLine();
        }
    }
}
