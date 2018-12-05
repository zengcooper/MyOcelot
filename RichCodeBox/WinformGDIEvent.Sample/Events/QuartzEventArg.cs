using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinformGDIEvent.Sample.Events
{
    /// <summary>
    /// 事件参数
    /// </summary>
    public class QuartzEventArg : EventArgs
    {
        /// <summary>
        /// 
        /// </summary>
        public int Hour { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public int Minutes { get; set; }


        /// <summary>
        /// 秒
        /// </summary>
        public int Seconds { get; set; }
    }
}
