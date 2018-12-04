using System;
using IdentityModel;
using IdentityModel.Client;
using System.Net.Http;

namespace PwdClient
{
    class Program
    {
        static void Main(string[] args)
        {
            var diso = DiscoveryClient.GetAsync("http://localhost:50000").Result;
            if(diso.IsError)
            {
                Console.WriteLine(diso.Error);
            }
            var tokenClient = new TokenClient(diso.TokenEndpoint, "pwdClient", "secrt");
            var tokenResponse = tokenClient.RequestResourceOwnerPasswordAsync("MQ", "123456").Result;
            if(tokenResponse.IsError)
            {
                Console.WriteLine(tokenResponse.Error);
            }
            else
            {
                Console.WriteLine(tokenResponse.Json);
            }
            var httpClient = new HttpClient();
            httpClient.SetBearerToken(tokenResponse.AccessToken);
            var response = httpClient.GetAsync("http://localhost:50001/api/values").Result;
            if(response.IsSuccessStatusCode)
            {
                Console.WriteLine(response.Content.ReadAsStringAsync().Result);
            }
            Console.WriteLine("Hello World!");
            Console.ReadLine();
        }
    }
}
