using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOC.Autofac.Sample.Service
{
    /// <summary>
    /// 
    /// </summary>
    public interface IUser
    {

        /// <summary>
        /// 
        /// </summary>
        Service.ILog Logger { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Domain.User GetUserById(int id);
    }
}
