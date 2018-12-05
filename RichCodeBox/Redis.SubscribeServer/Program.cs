using ServiceStack.Redis;
using System;

namespace Redis.SubscribeServer
{
    class Program
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            Console.Title = "redis订阅服务";
            Subscribe();//订阅频道
            Console.ReadLine();
        }


        /// <summary>
        ///<![CDATA[订阅频道]]>
        /// </summary>
        static void Subscribe()
        {
            using (RedisNativeClient redisNativeClient = new RedisNativeClient("localhost", 6379))
            using (RedisSubscription redisSubscription = new RedisSubscription(redisNativeClient))
            {
                redisSubscription.OnMessage += (e, m) =>
                {
                    Console.WriteLine("Subscribe OnMessage:{0} {1}", e, m);
                };
                redisSubscription.OnSubscribe += (m) =>
                {
                    Console.WriteLine("Subscribe OnSubscribe:{0}", m);
                };
                redisSubscription.OnUnSubscribe += (m) =>
                {
                    Console.WriteLine(" Subscribe OnUnSubscribe:{0}", m);
                };
                redisSubscription.SubscribeToChannels("channel-lcq");
                Console.WriteLine("订阅的数：{0}", redisSubscription.SubscriptionCount);
            }
        }
    }
}
