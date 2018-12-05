using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/******************************************************************************************************************
 * 
 * 
 * 说　明： 授权(版本：Version1.0.0)
 * 作　者：李朝强
 * 日　期: 2017-03-31 13:01:17
 * 修　改：2017-03-31 13:01:17
 * 参　考：http://my.oschina.net/lichaoqiang/ http://www.lichaoqiang.com
 * 备　注：暂无...
 * 
 * 
 * ***************************************************************************************************************/
namespace MiddleWareApp.MiddleWare
{

    /// <summary>
    /// 
    /// </summary>
    public abstract class MiddleWareBase
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="next"></param>
        protected MiddleWareBase(MiddleWareBase next)
        {
            this.Next = next;
        }

        /// <summary>
        /// 
        /// </summary>
        public MiddleWareBase Next { get; protected set; }


        /// <summary>
        /// 
        /// </summary>
        public abstract void Invoke();
    }
}
