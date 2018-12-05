using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Common.Security
{
    #region 基于哈希值的消息加密验证
    /// <summary>
    ///     <![CDATA[基于哈希值的消息加密验证]]>
    /// </summary>
    public class HMACSha1
    {
        /// <summary>
        /// <![CDATA[HMACSHA1算法加密并返回ToBase64String]]>
        /// </summary>
        /// <param name="strText">签名参数字符串</param>
        /// <param name="strKey">密钥参数</param>
        /// <returns>返回一个签名值(即哈希值)</returns>
        public static string ToBase64hmac(string strText, string strKey)
        {
            var myHMACSHA1 = new HMACSHA1(Encoding.UTF8.GetBytes(strKey));
            var byteText = myHMACSHA1.ComputeHash(Encoding.UTF8.GetBytes(strText));
            return Convert.ToBase64String(byteText);
        }

        /// <summary>
        ///     方  法： (版本：Version1.0.0)
        ///     作　者：李朝强
        ///     日　期：2015/05/19
        ///     修　改：2015/08/03
        ///     参　考：http://my.oschina.net/lichaoqiang/
        ///     备　注：暂无...
        /// </summary>
        public static string HMACSha1Text(string EncryptText, string EncryptKey)
        {
            //HMACSHA1加密
            string message, key;
            message = EncryptText;
            key = EncryptKey;

            var encoding = new ASCIIEncoding();
            var keyByte = encoding.GetBytes(key);
            var hmacsha1 = new HMACSHA1(keyByte);
            var messageBytes = encoding.GetBytes(message);
            var hashmessage = hmacsha1.ComputeHash(messageBytes);

            return Convert.ToBase64String(hashmessage);
        }

        /// <summary>
        ///     方  法：HMACSHA1Text (版本：Version1.0.0)
        ///     作　者：李朝强
        ///     日　期：2015/05/19
        ///     修　改：2015/08/03
        ///     参　考：http://my.oschina.net/lichaoqiang/
        ///     备　注：暂无...
        /// </summary>
        public static string HMACSHA1Text(string EncryptText, string EncryptKey)
        {
            //HMACSHA1加密
            var hmacsha1 = new HMACSHA1();
            hmacsha1.Key = Encoding.UTF8.GetBytes(EncryptKey);

            var dataBuffer = Encoding.UTF8.GetBytes(EncryptText);
            var hashBytes = hmacsha1.ComputeHash(dataBuffer);
            return Convert.ToBase64String(hashBytes);
        }

        /// <summary>
        ///     Sha1加密
        /// </summary>
        /// <param name="EncryptText">要加密的文本</param>
        /// <returns></returns>
        public static string Sha1(string EncryptText)
        {
            //sha1
            var _sha1Provider = new SHA1CryptoServiceProvider();

            var buffer = Encoding.ASCII.GetBytes(EncryptText);

            //加密后的字节
            var encryptBuffer = _sha1Provider.ComputeHash(buffer);

            EncryptText = BitConverter.ToString(encryptBuffer).Replace("-", string.Empty);

            return EncryptText;
        }
    }
    #endregion 基于哈希值的消息加密验证
}
