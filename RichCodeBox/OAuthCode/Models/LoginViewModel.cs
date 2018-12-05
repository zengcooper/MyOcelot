using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
/******************************************************************************************************************
 * 
 * 
 * 说　明：授权(版本：Version1.0.0)
 * 作　者：李朝强
 * 日　期：2017-03-29 13:36:54 
 * 修　改：
 * 参　考：http://my.oschina.net/lichaoqiang/
 * 备　注：暂无...
 * 
 * 
 * ***************************************************************************************************************/
namespace OAuthCode.Models
{
    /// <summary>
    /// LoginViewModel
    /// </summary>
    public class LoginViewModel
    {
        public string account { get; set; }

        public string password { get; set; }
    }
}