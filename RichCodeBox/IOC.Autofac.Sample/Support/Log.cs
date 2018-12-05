using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOC.Autofac.Sample.Support
{
    public class Log : Service.ILog
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="exception"></param>
        public void Error(Exception exception)
        {
            throw new NotImplementedException();
        }
    }
}
