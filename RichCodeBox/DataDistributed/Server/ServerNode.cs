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
 * 日　期: 2017-03-28 12:55:24
 * 修　改：2017-03-28 12:55:24
 * 参　考：http://my.oschina.net/lichaoqiang/ http://www.lichaoqiang.com
 * 备　注：暂无...
 * 
 * 
 * ***************************************************************************************************************/
namespace DataDistributed.Server
{
    public class ServerNode
    {

        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 虚拟节点数
        /// </summary>
        public int Copies { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int Host { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int UseRate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("{0}  {1}", this.Name, this.Copies);
        }
    }
}
