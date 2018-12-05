using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/******************************************************************************************************************
 * 
 * 
 * 说　明：(版本：Version1.0.0)
 * 作　者：李朝强
 * 日　期：2017-03-31 12:52:19
 * 修　改：2017-03-31 12:52:19
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
    public interface IAppBuilder
    {
        /// <summary>
        /// 
        /// </summary>
        void Use(object middleware, params object[] args);
    }
}
