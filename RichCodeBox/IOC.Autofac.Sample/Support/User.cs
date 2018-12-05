using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IOC.Autofac.Sample.Domain;
using au = Autofac;

namespace IOC.Autofac.Sample.Support
{

    /// <summary>
    /// 
    /// </summary>
    public class User : Service.IUser
    {

        /// <summary>
        /// 
        /// </summary>
        public Service.ILog Logger { get; set; }

        /// <summary>
        /// 构造函数注入
        /// </summary>
        /// <param name="name"></param>
        public User(Service.ICar car)
        {
            Console.WriteLine($"car.Count:{car.Count}");
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Domain.User GetUserById(int id)
        {

            Domain.User user = new Domain.User()
            {
                Id = id,
                Name = "lichaoqiang",
            };
            return user;
        }
    }
}
