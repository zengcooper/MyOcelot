using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOC.Autofac.Sample.Support
{
    public class Car : Service.ICar
    {
        /// <summary>
        /// 
        /// </summary>
        public int Count { get; set; }
    }
}
