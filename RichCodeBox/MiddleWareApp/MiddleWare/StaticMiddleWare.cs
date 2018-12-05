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
 * 日　期: 2017-03-31 17:05:08
 * 修　改：2017-03-31 17:05:08
 * 参　考：http://my.oschina.net/lichaoqiang/ http://www.lichaoqiang.com
 * 备　注：暂无...
 * 
 * 
 * ***************************************************************************************************************/
namespace MiddleWareApp.MiddleWare
{
    public class StaticMiddleWare : MiddleWareBase
    {

        /// <summary>
        /// 
        /// </summary>
        public StaticMiddleWare() : base(null) { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="next"></param>
        public StaticMiddleWare(MiddleWareBase next)
            : base(next)
        {

        }
        public override void Invoke()
        {
            Console.WriteLine("2、StaticMiddleWare.Invoke");
            if (null != Next) Next.Invoke();
        }
    }
}
