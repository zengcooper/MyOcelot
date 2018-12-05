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
 * 日　期: 2017-03-31 13:00:54
 * 修　改：2017-03-31 13:00:54
 * 参　考：http://my.oschina.net/lichaoqiang/ http://www.lichaoqiang.com
 * 备　注：暂无...
 * 
 * 
 * ***************************************************************************************************************/
namespace MiddleWareApp.MiddleWare
{

    /// <summary>
    /// 自定义中间件
    /// </summary>
    public class CustomMiddleWare : MiddleWareBase
    {

        /// <summary>
        /// 
        /// </summary>
        public CustomMiddleWare() : base(null) { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="next"></param>
        public CustomMiddleWare(MiddleWareBase next)
            : base(next)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        public override void Invoke()
        {
            Console.WriteLine("1、CustomMiddleWare.Invoke");
            if (null != Next) Next.Invoke();
        }
    }
}
