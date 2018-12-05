using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Common.Security
{

    /// <summary>
    /// MD5加密
    /// </summary>
    public class Md5
    {
        /// <summary>
        ///     MD5函数
        /// </summary>
        /// <param name="strInput">原始字符串</param>
        /// <returns>MD5结果</returns>
        public static string Encrypt(string strInput)
        {
            var buffer = Encoding.UTF8.GetBytes(strInput);
            buffer = new MD5CryptoServiceProvider().ComputeHash(buffer);
            var ret = string.Empty;
            for (var i = 0; i < buffer.Length; i++)
            {
                ret += buffer[i].ToString("x").PadLeft(2, '0');
            }
            return ret;
        }
    }
}
