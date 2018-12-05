using Autofac;
using System;
using au = Autofac;

namespace IOC.Autofac.Sample
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
         
            Console.Title = "IOC,Autofac示例程序 www.lichaoqiang.com ";

            //容器生成器
            ContainerBuilder containerBuilder = new ContainerBuilder();

            //注册类型
            containerBuilder.RegisterType<Support.Log>().As<Service.ILog>();
            containerBuilder.RegisterType<Support.Car>().As<Service.ICar>();
            containerBuilder.RegisterType<Support.User>().As<Service.IUser>().PropertiesAutowired();

            IContainer container = containerBuilder.Build();//生成容器

            //对象
            var car = container.Resolve<Service.ICar>();
            car.Count = 10;

            Service.IUser userService = container.Resolve<Service.IUser>(new au.NamedParameter("car", car));

            Domain.User user = userService.GetUserById(id: 1);

            Console.WriteLine("ok!");
            Console.ReadLine();
        }
    }
}
