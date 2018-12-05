using System;
using ZooKeeperNet;

namespace ZookeeperApp.Sample
{

    /// <summary>
    /// 
    /// </summary>
    class Program
    {

        /// <summary>
        /// <![CDATA[Zookeeper使用示例]]>
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            //创建一个Zookeeper实例，第一个参数为目标服务器地址和端口，第二个参数为Session超时时间，第三个为节点变化时的回调方法 
            using (ZooKeeper zk = new ZooKeeper("10.10.10.19:2181", new TimeSpan(0, 0, 0, 50000), new Watcher()))
            {

                var stat = zk.Exists("/root", true);
                ////创建一个节点root，数据是mydata,不进行ACL权限控制，节点为永久性的(即客户端shutdown了也不会消失) 
                //zk.Create("/root", "mydata".GetBytes(), Ids.OPEN_ACL_UNSAFE, CreateMode.Persistent);

                ////在root下面创建一个childone znode,数据为childone,不进行ACL权限控制，节点为永久性的 
                //zk.Create("/root/childone", "lichaoqiang.com".GetBytes(), Ids.OPEN_ACL_UNSAFE, CreateMode.Persistent);
                //取得/root节点下的子节点名称,返回List<String> 
                var listRootChildren = zk.GetChildren("/dubbo", true);
                listRootChildren = zk.GetChildren("/dubbo/com.ty.dao.mapper.ebook.EBookNoteMapper/providers", true);
                //取得/root/childone节点下的数据,返回byte[] 
                //byte[] buffer = zk.GetData("/root/childone", true, null);

                //string strData = System.Text.Encoding.UTF8.GetString(buffer);
                //Console.WriteLine(strData);

                //修改节点/root/childone下的数据，第三个参数为版本，如果是-1，那会无视被修改的数据版本，直接改掉
                //zk.SetData("/root/childone", "childonemodify".GetBytes(), -1);
                //删除/root/childone这个节点，第二个参数为版本，－1的话直接删除，无视版本 
                //zk.Delete("/root/childone", -1);

                ZooKeeperNet.ClientConnection clientConnection = new ClientConnection("10.10.10.19:2181", new TimeSpan(0, 0, 0, 50000), zk, null);
                clientConnection.Start();
                ZooKeeperNet.ClientConnectionEventConsumer clientConnectionEventConsumer = new ClientConnectionEventConsumer(clientConnection);
                clientConnectionEventConsumer.Start();
            }
            Console.ReadLine();
        }
    }


    /// <summary>
    /// 
    /// </summary>
    class Watcher : IWatcher
    {
        public void Process(WatchedEvent @event)
        {
            if (@event.Type == EventType.NodeDataChanged)
            {
                Console.WriteLine(@event.Path);
            }
        }
    }
}
