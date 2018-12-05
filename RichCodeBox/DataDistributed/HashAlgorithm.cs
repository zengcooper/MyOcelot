using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

/******************************************************************************************************************
 * 
 * 
 * 说　明： 授权(版本：Version1.0.0)
 * 作　者：李朝强
 * 日　期: 2017-03-28 9:31:13
 * 修　改：2017-03-28 9:31:13
 * 参　考：http://my.oschina.net/lichaoqiang/ http://www.lichaoqiang.com
 * 备　注：暂无...
 * 
 * 
 * ***************************************************************************************************************/
namespace DataDistributed
{
    /// <summary>
    ///     <![CDATA[哈希算法]]>
    /// </summary>
    public class HashAlgorithm
    {
        /// <summary>
        ///     <![CDATA[获取Hash]]>
        /// </summary>
        /// <param name="digest"></param>
        /// <param name="nTime"></param>
        /// <returns></returns>
        public static long Hash(byte[] digest, int nTime)
        {
            var rv = ((long)(digest[3 + nTime * 4] & 0xFF) << 24)
                     | ((long)(digest[2 + nTime * 4] & 0xFF) << 16)
                     | ((long)(digest[1 + nTime * 4] & 0xFF) << 8)
                     | ((long)digest[0 + nTime * 4] & 0xFF);

            return rv & 0xffffffffL; /* Truncate to 32-bits */
        }

        /// <summary>
        ///     <![CDATA[MD5加密]]>
        /// </summary>
        /// <param name="k"></param>
        /// <returns></returns>
        public static byte[] ComputeMd5(string k)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            var keyBytes = md5.ComputeHash(Encoding.UTF8.GetBytes(k));
            md5.Clear();
            return keyBytes;
        }
    }
}
