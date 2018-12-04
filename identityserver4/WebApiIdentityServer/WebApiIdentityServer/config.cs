using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4.Models;
using IdentityServer4.Test;

namespace WebApiIdentityServer
{
    public class config
    {
        //IdentityServer配置——用户
        //IdentityServer用户-这里通过提供一个简单的C#类完成，
        //当然你可以从任何数据存储加载用户。
        //我们提供了ASP.NET Identity 和MembershipReboot支持检索用户信息。
        public static IEnumerable<ApiResource> GetResources()
        {
            return new List<ApiResource> { new ApiResource("api","MQapi")};
        }

        //IdentityServer需要一些关于客户端信息，这可以简单地提供使用客户端对象
        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>
            {
                new Client()
                {
                    ClientId="ClientId",
                    AllowedGrantTypes=GrantTypes.ClientCredentials,//客户端模式
                    ClientSecrets={ new Secret("secrt".Sha256())},
                    AllowedScopes={ "api"}
                },
                new Client()
                {
                    ClientId="pwdClient",
                    AllowedGrantTypes=GrantTypes.ResourceOwnerPassword,//密码模式
                     ClientSecrets={ new Secret("secrt".Sha256())},
                     RequireClientSecret=false,
                    AllowedScopes={ "api"}
                }
            };
        }

        //模拟用户
        public static List<TestUser> GetTsetUsers()
        {
            return new List<TestUser>{
                new TestUser{
                    SubjectId="1",
                    Username="MQ",
                    Password="123456"
                }
            };
        }
    }
}
