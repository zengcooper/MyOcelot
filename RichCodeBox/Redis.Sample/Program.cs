using ServiceStack.Redis;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Linq;

namespace Redis.Sample
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


            //SaveStringToRedisSet();

            //using (RedisClient redisClient = (RedisClient)GetRedisClient())
            //{
            //    Domain.User user = new Domain.User()
            //    {
            //        Id = 1024,
            //        Name = "lichaoqiang",
            //        Mobile = "15890676666",
            //        Email = "xxxxx@163.com",
            //    };
            //    var redisTypedClient = redisClient.As<Domain.User>();
            //    redisTypedClient.Store(user);
            //}
            //TestLimited();

            //bitemap
            //SetBit();

            //存储hash
            //StoreHash();

            //发布/订阅
            PubSubServer();

            Console.ReadLine();
        }


        /// <summary>
        /// <![CDATA[保存字符串到redis set]]>
        /// </summary>
        static void SaveStringToRedisSet()
        {
            //1>redis客户端
            using (RedisClient redisClient = (RedisClient)GetRedisClient())
            {
                //set集
                redisClient.Store<string>("_stringKey");
                redisClient.AddItemToSet("_setId", "lichaoqiang");

                redisClient.Save();
            }
        }

        /// <summary>
        /// <![CDATA[bitmap测试]]>
        /// </summary>
        static void SetBit()
        {
            //1>redis客户端
            using (RedisClient redisClient = (RedisClient)GetRedisClient())
            {
                redisClient.SetBit("_bitmap-test-key", 18, 1);
                redisClient.Save();
            }
        }

        /// <summary>
        /// 存储Hash
        /// </summary>
        static void StoreHash()
        {
            return;
            using (RedisClient redisClient = (RedisClient)GetRedisClient())
            {
                Domain.User user = new Domain.User()
                {
                    Id = 1024,
                    Name = "lichaoqiang",
                    Mobile = "15890676666",
                    Email = "xxxxx@163.com",
                };
                redisClient.StoreAsHash<Domain.User>(user);
                redisClient.Save();


                //从redis中获取对象
                var u = redisClient.GetFromHash<Domain.User>(id: 1024);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        static void TestLimited()
        {
            Loop(10000, (i) =>
            {
                StoreHash();
                Console.WriteLine("写入{0}次", i);
            });
        }

        #region 发布/订阅
        /// <summary>
        /// 订阅
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

            }
        }

        /// <summary>
        /// 
        /// </summary>
        static void PubSubServer()
        {
            Console.Title = "发布服务器";
            string strChannel = "channel-lcq";
            using (PooledRedisClientManager pooledRedisClientManager = new PooledRedisClientManager("localhost"))
            using (RedisPubSubServer redisPubSubServer = (RedisPubSubServer)pooledRedisClientManager.CreatePubSubServer(channel: strChannel))
            {
                redisPubSubServer.OnStart += () => { Console.WriteLine("PubSubServer start channel:{0} ", strChannel); };
                redisPubSubServer.OnStop += () => { Console.WriteLine("PubSubServer stop channel:{0} ", strChannel); };
                redisPubSubServer.OnMessage += (e, m) => { Console.WriteLine("RedisPubSubServer OnMessage:{0} {1}", e, m); };
                redisPubSubServer.OnHeartbeatSent += () => { Console.WriteLine("OnHeartbeatSent"); };
                redisPubSubServer.Start();//启动发布服务

                Console.WriteLine("请输入消息内容...");
                while (true)
                {
                    string message = Console.ReadLine();
                    PublishMessage(message);//发布消息
                }
                Console.ReadLine();
            }
        }

        /// <summary>
        /// 发送消息
        /// </summary>
        private static void PublishMessage(string message)
        {
            try
            {
                //Console.WriteLine("PublishMessage(): " + message);
                using (var redis = GetRedisClient())
                {
                    redis.PublishMessage("channel-lcq", message);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR PublishMessage: " + ex);
            }
        }
        #endregion


        /// <summary>
        ///分片
        /// </summary>
        /// <param name="key"><![CDATA[映射的key,主要是通过一致性哈希，把当前请求映射到某个虚拟节点上]]></param>
        /// <returns></returns>
        static T GetShardedRedisClient<T>(string key)
            where T : IRedisClient
        {
            ShardedConnectionPool shardedConnectionPool = new ShardedConnectionPool("node-1", 60, "localhost", "127.0.0.1");
            ShardedRedisClientManager shardedRedisClientManager = new ShardedRedisClientManager(shardedConnectionPool);
            var redisConnectionPool = shardedRedisClientManager.GetConnectionPool(key: key);
            return (T)redisConnectionPool.GetClient();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        static IRedisClient GetRedisClient()
        {
            return new RedisClient("localhost", 6379);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="times"></param>
        /// <param name="action"></param>
        static void Loop(int times, Action<int> action)
        {
            for (int i = 1; i <= times; i++)
            {
                action(i);
            }
        }


    }
}
