using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataDistributed.Server;

/******************************************************************************************************************
 * 
 * 
 * 说　明： 授权(版本：Version1.0.0)
 * 作　者：李朝强
 * 日　期: 2017-03-28 9:33:01
 * 修　改：2017-03-28 9:33:01
 * 参　考：http://my.oschina.net/lichaoqiang/ http://www.lichaoqiang.com
 * 备　注：暂无...
 * 
 * 
 * ***************************************************************************************************************/
namespace DataDistributed
{
    /// <summary>
    /// <![CDATA[集群路由]]>
    /// </summary>
    public sealed class KetamaNodeLocator
    {

        #region 事件
        /// <summary>
        /// 自定义事件：当激活集群节点的时触发
        /// </summary>
        private EventHandler _OnActived = null;
        public event EventHandler OnActived
        {
            add { _OnActived += value; }
            remove { _OnActived -= value; }
        }
        #endregion

        #region Member
        /// <summary>
        /// 
        /// </summary>
        private Dictionary<long, ServerNode> ketamaNodes;

        /// <summary>
        /// 
        /// </summary>
        private long[] keys;
        #endregion

        #region Method
        /// <summary>
        /// <![CDATA[构造函数]]>
        /// </summary>
        /// <param name="nodes"></param>
        public KetamaNodeLocator(List<ServerNode> nodes)
        {
            ketamaNodes = new Dictionary<long, ServerNode>();
            //对所有节点，生成nCopies个虚拟结点
            for (int j = 0; j < nodes.Count; j++)
            {
                int numReps = nodes[j].Copies;
                //每四个虚拟结点为一组
                for (int i = 0; i < numReps / 4; i++)
                {
                    byte[] digest = HashAlgorithm.ComputeMd5(string.Format("{0}", nodes[j].Name)+i);
                    /** Md5是一个16字节长度的数组，将16字节的数组每四个字节一组，*  分别对应一个虚拟结点，这就是为什么上面把虚拟结点四个划分一组的原因*/
                    for (int h = 0; h < 4; h++)
                    {
                        long rv = ((long)(digest[3 + h * 4] & 0xFF) << 24)
                                 | ((long)(digest[2 + h * 4] & 0xFF) << 16)
                                 | ((long)(digest[1 + h * 4] & 0xFF) << 8)
                                 | ((long)digest[0 + h * 4] & 0xFF);

                        rv = rv & 0xffffffffL; /* Truncate to 32-bits */
                        ketamaNodes[rv] = nodes[j];
                    }
                }

                if (numReps == 0)
                {
                    byte[] digest = HashAlgorithm.ComputeMd5(string.Format("{0}", nodes[j].Name));
                    /** Md5是一个16字节长度的数组，将16字节的数组每四个字节一组，*  分别对应一个虚拟结点，这就是为什么上面把虚拟结点四个划分一组的原因*/
                    for (int h = 0; h < 4; h++)
                    {
                        long rv = ((long)(digest[3 + h * 4] & 0xFF) << 24)
                                 | ((long)(digest[2 + h * 4] & 0xFF) << 16)
                                 | ((long)(digest[1 + h * 4] & 0xFF) << 8)
                                 | ((long)digest[0 + h * 4] & 0xFF);

                        rv = rv & 0xffffffffL; /* Truncate to 32-bits */
                        ketamaNodes[rv] = nodes[j];
                    }
                }

            }
            keys = ketamaNodes.Keys.OrderBy(p => p).ToArray();
        }


        /// <summary>
        /// <![CDATA[根据key获取虚拟节点]]>
        /// </summary>
        /// <param name="k"></param>
        /// <returns></returns>
        public ServerNode GetWorkerNode(string k)
        {
            byte[] digest = HashAlgorithm.ComputeMd5(k);
            return GetNodeInner(HashAlgorithm.Hash(digest, 0));
        }

        /// <summary>
        /// <![CDATA[获取节点]]>
        /// </summary>
        /// <param name="hash"></param>
        /// <returns></returns>
        ServerNode GetNodeInner(long hash)
        {
            if (ketamaNodes.Count == 0) return null;
            long key = hash;
            int near = 0, index = Array.BinarySearch(keys, hash);
            if (index < 0)
            {
                near = (~index);
                if (near == keys.Length) near = 0;
            }
            else { near = index; }
            //事件
            if (null != _OnActived) { _OnActived(ketamaNodes[keys[near]], null); }
            return ketamaNodes[keys[near]];

        }
        #endregion

    }
}
