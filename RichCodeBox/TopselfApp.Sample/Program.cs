using System;
using Topshelf;
using TopshelfApp.Sample.Services;
/*

下载地址：https://github.com/Topshelf/Topshelf/downloads

*/
namespace Topshelf.Sample
{

    /// <summary>
    /// 
    /// </summary>
    class Program
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {

            /*
             * 
             * 
             *         用法：cmd定位到当前exe程序，直接在控制台输入 当前exe程序 install
             *         这里运行命令： TopshelfApp.Sample install
             * 
             */
            var x = Topshelf.HostFactory.Run((e) =>
             {
                 e.Service<TopshelfApp.Sample.Services.TestService>(s =>
                 {
                     var service = new TopshelfApp.Sample.Services.TestService();
                     s.ConstructUsing(name => new TopshelfApp.Sample.Services.TestService());
                     s.WhenStarted<TestService>((t) => { t.Start(null); });
                     s.WhenStopped((h) => service.Stop(null));
                 });
                 e.RunAsLocalService();
                 e.SetServiceName("TopshelfTestService");
                 e.SetDisplayName("TopshelfTestService");
                 e.SetDescription("测试TopshelfTestService");
                 e.SetHelpTextPrefix("http://www.lichaoqiang.com");
                 e.StartAutomatically();//设置自动启动

             });
            var exitCode = (int)Convert.ChangeType(x, x.GetTypeCode());  //11
            Environment.ExitCode = exitCode;

        }


    }
}