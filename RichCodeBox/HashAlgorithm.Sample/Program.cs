using System;
using System.Collections.Generic;

namespace HashAlgorithm.Sample
{
    class Program
    {


        /// <summary>
        /// 测试一致性哈希
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            ConsistentHash<ShardedConnection> consistentHash = new ConsistentHash<ShardedConnection>();
            for (int i = 0; i < 10; i++)
            {
                consistentHash.AddTarget(new ShardedConnection() { Name = "Server-" + i }, 4 * (i + 1));
            }
            string strKey = "lichaoqiang";
            var shardedConnection = consistentHash.GetTarget(strKey);
            Console.WriteLine("{0}  {1}", strKey, shardedConnection.Name);

            var keys = new string[] { "lcq", "jifeaf", "ddfett", "fte5q", "fdfetrqa", "ctdte", "mbtgadf", "dettrqqa", "dddefetr", "ccdqffa" };

            Dictionary<string, string> mapper = new Dictionary<string, string>();
            for (int i = 1; i < 10; i++)
            {
                string key = keys[i - 1];
                var sharded = consistentHash.GetTarget(key);
                Console.WriteLine("{0} {1}", key, sharded.Name);
                mapper[key] = sharded.Name;
            }

            //新增虚拟节点
            consistentHash.AddTarget(new ShardedConnection() { Name = "Server-new-01" }, 50);
            consistentHash.AddTarget(new ShardedConnection() { Name = "Server-new-02" }, 50);
            consistentHash.AddTarget(new ShardedConnection() { Name = "Server-new-03" }, 50);

            for (int i = 1; i < 10; i++)
            {
                string key = keys[i - 1];
                var sharded = consistentHash.GetTarget(key);
                Console.WriteLine("{0} {1}", key, sharded.Name);
                // if (mapper[key] != sharded.Name) throw new NotSupportedException();
            }
            Console.Read();
            while (true)
            {
                string key = Guid.NewGuid().ToString("N");
                var sharded = consistentHash.GetTarget(key);
                Console.WriteLine("{0} {1}", key, sharded.Name);
                System.Threading.Thread.Sleep(800);
            }
        }
    }
}
