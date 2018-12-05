using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Common.Security
{

    /// <summary>
    /// 
    /// </summary>
    public class Sha1
    {
        /// <summary>
        ///  <![CDATA[进行sha1加密]]>
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string Encrypt(string input)
        {
            var pwBytes = Encoding.UTF8.GetBytes(input);
            var hash = SHA1.Create().ComputeHash(pwBytes);
            var hex = new StringBuilder();
            for (var i = 0; i < hash.Length; i++) hex.Append(hash[i].ToString("X2"));
            return hex.ToString();
        }
    }
}
