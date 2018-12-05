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
 * 日　期: 2017-03-31 17:08:10
 * 修　改：2017-03-31 17:08:10
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
    public class OAuthMiddleWare : MiddleWareBase
    {

        /// <summary>
        /// 
        /// </summary>
        public OAuthMiddleWare() : base(null) { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="next"></param>
        public OAuthMiddleWare(MiddleWareBase next)
            : base(next)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        public override void Invoke()
        {
            Console.WriteLine("3、OAuthMiddleWare.Invoke");
            if (null != Next) Next.Invoke();
        }
    }
}
