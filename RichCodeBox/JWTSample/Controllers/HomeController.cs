using System;
using System.Collections.Generic;
using System.Web.Mvc;

/*=================================================================================================
*
* Title:JWT示例程序
* Author:李朝强
* Description:模块描述
* CreatedBy:lichaoqiang.com
* CreatedOn:
* ModifyBy:暂无...
* ModifyOn:
* Company:河南天一文化传播股份有限公司
* Blog:http://www.lichaoqiang.com
* Mark:
*
*** ================================================================================================*/
namespace JWTSample.Controllers
{

    /// <summary>
    /// <![CDATA[JWT]]>
    /// </summary>
    public class HomeController : Controller
    {

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            ViewBag.Title = "JWT示例程序";

            const string key = "GQDstcKsx0NHjPOuXOYg5MbeJ1XT0uFiwDVvVBrk";//私密key

            //1、生成JWT Token
            JWT.Algorithms.HMACSHA256Algorithm hMACSHA256Algorithm = new JWT.Algorithms.HMACSHA256Algorithm();
            JWT.Serializers.JsonNetSerializer jsonNetSerializer = new JWT.Serializers.JsonNetSerializer();
            JWT.JwtBase64UrlEncoder jwtBase64UrlEncoder = new JWT.JwtBase64UrlEncoder();
            JWT.JwtEncoder jwtEncoder = new JWT.JwtEncoder(hMACSHA256Algorithm, jsonNetSerializer, jwtBase64UrlEncoder);

            //JWT三部分组成 header.payload.signature
            Dictionary<string, object> dictHeader = new Dictionary<string, object>()//header
            {

            };

            DateTime dtExpire = DateTime.UtcNow.AddSeconds(15);//为了测试过期可以把过期时间设置为1秒钟
            double exp = (dtExpire - JWT.JwtValidator.UnixEpoch).TotalSeconds;//注意，这里用的是UTC时间

            object payload = new Models.Payload<Models.User>()
            {
                exp = exp,
                data = new Models.User()
                {
                    Id = Guid.Parse("e05aa9d2-6a97-4df1-ab81-b1b585e9bc44"),
                    Name = "lichaoqiang",
                    Mobile = "13503879XXX",
                    Email = "1058736170@qq.com",
                    Role = "admin",
                }

            };//负载
            try
            {

                string strJwtToken = jwtEncoder.Encode(payload, key);

                //2、验证JWT Token
                JWT.JwtValidator jwtValidator = new JWT.JwtValidator(jsonNetSerializer, new JWT.UtcDateTimeProvider());//jwtValidator
                JWT.JwtDecoder jwtDecoder = new JWT.JwtDecoder(jsonNetSerializer, jwtValidator, jwtBase64UrlEncoder);

                var payloadData = jwtDecoder.DecodeToObject<Models.Payload<Models.User>>(token: strJwtToken);//从JWTToken中反序列化负荷

                byte[] keys = System.Text.Encoding.UTF8.GetBytes(key);
                JWT.JwtParts jwtParts = new JWT.JwtParts(token: strJwtToken);
                jwtDecoder.Validate(jwtParts, keys);
            }
            catch (JWT.TokenExpiredException ex)
            {
                //令牌过期
            }
            catch (JWT.SignatureVerificationException ex)
            {
                //签名失败
            }
            catch (Exception ex)
            {

            }
            return View();
        }
    }
}
