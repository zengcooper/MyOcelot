using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnityApp.Sample.Strategy
{

    /// <summary>
    /// 
    /// </summary>
    public interface IUser
    {
        /// <summary>
        /// 
        /// </summary>
        int Id { get; }

        /// <summary>
        /// 
        /// </summary>
        string Name { get; }


        /// <summary>
        /// 
        /// </summary>
        string Mobile { get; }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IUser GetUserById();
    }
}
