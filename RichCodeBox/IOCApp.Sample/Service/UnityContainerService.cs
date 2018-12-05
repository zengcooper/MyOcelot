using Microsoft.Practices.Unity.Configuration;
using System;
using System.Configuration;
using Unity;
using UnityApp.Sample.Strategy;

namespace UnityApp.Sample.Service
{

    /// <summary>
    /// <![CDATA[这里是为了方便测试，定义容器服务]]>
    /// </summary>
    public class UnityContainerService
    {

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Strategy.IUser GetUser()
        {
            //IOC容器
            Unity.UnityContainer unityContainer = new Unity.UnityContainer();
            unityContainer.RegisterType(typeof(Strategy.IUser), typeof(Domain.User));

            Strategy.IUser user = unityContainer.Resolve<Strategy.IUser>();
            return user;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IUser GetUserByUnityConfiguration()
        {
            Unity.IUnityContainer unityContainer = new UnityContainer();
            ////根据文件名获取指定config文件
            string filePath = AppDomain.CurrentDomain.BaseDirectory + @"Config\Unity.config";
            var fileMap = new ExeConfigurationFileMap { ExeConfigFilename = filePath };

            //从config文件中读取配置信息
            Configuration configuration = ConfigurationManager.OpenMappedExeConfiguration(fileMap, ConfigurationUserLevel.None);
            var unitySection = (UnityConfigurationSection)configuration.GetSection("unity");
            foreach (var item in unitySection.Containers)
            {
                unityContainer.LoadConfiguration(unitySection, item.Name);
            }
            Strategy.IUser user = unityContainer.Resolve<Strategy.IUser>();
            user = user.GetUserById();
            return user;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IUser GetUserByAppConfiguration()
        {
            Unity.IUnityContainer unityContainer = new Unity.UnityContainer();
            unityContainer.LoadConfiguration("unityContainer");
            Strategy.IUser user = unityContainer.Resolve<Strategy.IUser>();
            user = user.GetUserById();
            return user;
        }
    }
}
