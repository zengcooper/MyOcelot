using System;
using Unity.Interception.PolicyInjection.Pipeline;
using UnityApp.Sample.Strategy;

/*=================================================================================================
*
* Title:IOC，Unity示例程序
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
namespace UnityApp.Sample
{
    class ProgramSample
    {

        /// <summary>
        /// <![CDATA[主程序入口]]>
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            Console.Title = "IOC容器Unity使用示例 www.lichaoqiang.com";
            //unity：https://github.com/unitycontainer/unity

            Service.UnityContainerService unityContainerService = new Service.UnityContainerService();
            //IOC容器
            Console.WriteLine("GetUser");
            IUser user = unityContainerService.GetUser();

            //测试配置 unity.config
            Console.WriteLine("GetUserByUnityConfiguration");
            unityContainerService.GetUserByUnityConfiguration();

            //测试app.config配置
            Console.WriteLine("GetUserByAppConfiguration");
            unityContainerService.GetUserByAppConfiguration();

            Console.ReadLine();
        }

    }
}
