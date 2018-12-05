using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static UserService;

namespace ThriftServer.Services
{
    /// <summary>
    /// 用户服务
    /// </summary>
    public class TheUserService:Iface
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public User GetUserByID(int userID)
        {
            return new User() { ID = 1, Name = "lichaoqiang" };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<User> GetAllUser()
        {
            List<User> users = new List<User>(){
                new User() { ID = 1, Name = "lichaoqiang" },
                new User() { ID = 2, Name = "yuyuangfang" }
            };
            return users;
        }
    }
}
