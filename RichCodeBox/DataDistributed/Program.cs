using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using DataDistributed.Server;

namespace DataDistributed
{
    class Program
    {

        /*
         SERVER-6  b536424c20584149b4301e6e4a012d9a

SERVER-10  90c0e30f983745cb9672d73570e10466

SERVER-0  b726be8ba9084842bbb90c6c49ac463f

SERVER-1  44280ae6386b4837a47c06922f08ec9c

SERVER-4  a4a6a7a3d6a74e43aed8778bda9ed1c1

SERVER-3  0cb4d776625c48c68bee157df2a64c1d

SERVER-8  dd88fe89a2e64001b345d1ddc23e3c8d

SERVER-9  e324ef298ace40cda46cbc843d7e85d9

SERVER-7  01edc06de4124616b513f126aff5aa52

SERVER-5  a2337c208ab3499ca0685d5a6437f68e

SERVER-2  69e9d7dd82364c23a26e7f70065ec316
             */
        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            try
            {
                //虚拟节点
                List<Server.ServerNode> list = new List<Server.ServerNode>();

                for (int i = 0; i < 11; i++)
                {
                    Server.ServerNode node = new ServerNode();
                    node.Name = "SERVER-" + i;
                    node.Copies = 20;
                    list.Add(node);
                }

                //Server.ServerNode node0 = new ServerNode();
                //node0.Name = "SERVER-zero";
                //node0.Copies = 20;
                //list.Add(node0);
                KetamaNodeLocator knl = new KetamaNodeLocator(list);

                #region 生成节点
                SortedDictionary<string, ServerNode> nodes = new SortedDictionary<string, ServerNode>();
                while (true)
                {
                    string key = Guid.NewGuid().ToString("N");

                    var n = knl.GetWorkerNode(key);

                    if (nodes.ContainsValue(n) == false)
                    {
                        nodes.Add(key, n);
                    }
                    if (nodes.Count == 11)
                    {
                        break;
                    }
                }
                #endregion
                //测试节点
                foreach (var item in nodes)
                {
                    ServerNode node = knl.GetWorkerNode(item.Key);
                    if (node.Name != item.Value.Name) throw new InvalidOperationException("节点无效！");
                }
                Console.WriteLine("节点匹配成功！");

                //新增一个节点
                Console.WriteLine("新增一个节点！");
                Server.ServerNode node0 = new ServerNode();
                node0.Name = "SERVER-zero";
                node0.Copies = 90;
                list.Add(node0);

                //list.RemoveAt(2);//移除一个节点
                knl = new KetamaNodeLocator(list);

                //再次进行测试
                List<string> error = new List<string>();
                foreach (var item in nodes)
                {
                    ServerNode node = knl.GetWorkerNode(item.Key);
                    if (node.Name != item.Value.Name) error.Add(item.Key);
                }

                Console.WriteLine("测试完成！");

                #region 测试一致性哈希
                for (int i = 0; i < 1000; i++)
                {
                    string uid = Guid.NewGuid().ToString();
                    var node = knl.GetWorkerNode(uid);
                    node.UseRate++;

                    Console.Clear();

                    if (node.Name == "SERVER-zero")
                    {
                        Console.WriteLine(uid);
                    }
                    foreach (var item in list)
                    {
                        var total = list.Sum((m) => m.UseRate);
                        var rate = (item.UseRate * 100) / total;
                        Console.WriteLine("{0}  使用率：{1}%", item, rate);
                    }
                    //System.Threading.Thread.Sleep(200);
                    // Console.WriteLine(node);
                }
                #endregion

                Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadLine();
            }


        }
    }
}
