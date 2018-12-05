using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HashLib;

namespace HashApp.Sample
{

    /// <summary>
    /// 
    /// </summary>
    class Program
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            string strSource = "lichaoqiang";
            string strMd5 = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(strSource, "md5");

            var md5 = System.Security.Cryptography.MD5.Create();
            byte[] encrypt = md5.ComputeHash(System.Text.Encoding.UTF8.GetBytes(strSource));
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data 
            // and format each one as a hexadecimal string.
            for (int i = 0; i < encrypt.Length; i++)
            {
                sBuilder.Append(encrypt[i].ToString("x2"));
            }
            string strMd52 = sBuilder.ToString();

            //斯内夫鲁
            IHash hash = HashLib.HashFactory.Crypto.CreateSnefru_8_128();
            HashResult hashResult = hash.ComputeString("www.lichaoqiang.com");
            Console.WriteLine(hashResult.ToString());

            //hash32
            string strHash32 = HashLib.HashFactory.Hash32.CreateRS().ComputeString("www.lichaoqianag.com").ToString();
            Console.WriteLine("CreateRS:{0}", strHash32);

            strHash32 = HashLib.HashFactory.Hash32.CreatePJW().ComputeString("www.lichaoqianag.com").ToString();
            Console.WriteLine("CreatePJW:{0}", strHash32);

            //
            string strHmac = HashLib.HashFactory.HMAC.CreateHMAC(hash).ComputeString("www.lichaoqiang.com").ToString();
            Console.WriteLine("CreateHMAC:{0}", strHmac);

            Console.ReadLine();
        }
    }
}
