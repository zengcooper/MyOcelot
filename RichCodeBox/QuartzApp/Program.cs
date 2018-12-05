using Quartz;
using Quartz.Impl;
using QuartzApp.Jobs;
using System;
using System.Collections.Specialized;
using System.Threading.Tasks;

/*=================================================================================================
*
* Title:任务调度计划
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
namespace QuartzApp
{

    /// <summary>
    /// 
    /// </summary>
    class Program
    {

        /// <summary>
        ///<![CDATA[任务调度计划]]>
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            try
            {
                Console.Title = "任务调度计划示例程序-www.lichaoqiang.com";
                log4net.Config.XmlConfigurator.Configure();
                log4net.ILog log = log4net.LogManager.GetLogger(typeof(Program));

                Configure().Wait();
                //ConfigureAdo().Wait();
                Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally { Console.Read(); }
        }


        /// <summary>
        /// <![CDATA[程序调用启动Job]]>
        /// </summary>
        /// <returns></returns>
        static async Task StartQuestionJob()
        {
            ISchedulerFactory schedulerFactory = new StdSchedulerFactory();
            IScheduler scheduler = await schedulerFactory.GetScheduler();

            var jobDetail = JobBuilder.Create<QuestionJob>().Build();
            ITrigger trigger = TriggerBuilder.Create().WithIdentity("myTrigger")
                .StartNow()
                .WithSimpleSchedule((e) =>
                {
                    e.WithIntervalInSeconds(3).RepeatForever();

                }).Build();
            await scheduler.ScheduleJob(jobDetail, trigger);
        }


        /// <summary>
        /// <![CDATA[通过配置文件的方式]]>
        /// </summary>
        /// <returns></returns>
        static async Task Configure()
        {
            Jobs.QuestionJob questionJob = new Jobs.QuestionJob();

            Quartz.Simpl.SimpleTypeLoadHelper simpleTypeLoadHelper = new Quartz.Simpl.SimpleTypeLoadHelper();
            Quartz.Xml.XMLSchedulingDataProcessor xmlSchedulingDataProcessor = new Quartz.Xml.XMLSchedulingDataProcessor(simpleTypeLoadHelper);

            ISchedulerFactory schedulerFactory = new StdSchedulerFactory();
            IScheduler scheduler = schedulerFactory.GetScheduler().Result;
            await xmlSchedulingDataProcessor.ProcessFileAndScheduleJobs(scheduler);

            await scheduler.Start();
        }

        /// <summary>
        /// ADO.Net
        /// </summary>
        /// <returns></returns>
        static async Task ConfigureAdo()
        {
            Jobs.QuestionJob questionJob = new Jobs.QuestionJob();
            NameValueCollection properties = new NameValueCollection();

            properties["quartz.scheduler.instanceName"] = "TestScheduler";
            properties["quartz.scheduler.instanceId"] = "instance_one";
            properties["quartz.threadPool.type"] = "Quartz.Simpl.SimpleThreadPool, Quartz";
            properties["quartz.threadPool.threadCount"] = "5";
            //properties["quartz.threadPool.threadPriority"] = "Normal";
            properties["quartz.jobStore.misfireThreshold"] = "60000";
            properties["quartz.jobStore.type"] = "Quartz.Impl.AdoJobStore.JobStoreTX, Quartz";
            properties["quartz.jobStore.useProperties"] = "false";
            properties["quartz.jobStore.dataSource"] = "default";
            properties["quartz.jobStore.tablePrefix"] = "QRTZ_";
            properties["quartz.serializer.type"] = "binary";
            properties["quartz.jobStore.clustered"] = "true";

            properties["quartz.jobStore.driverDelegateType"] = "Quartz.Impl.AdoJobStore.SqlServerDelegate, Quartz";
            // 数据库连接字符串
            properties["quartz.dataSource.default.connectionString"] = "Data Source=LICHAOQIANG-PC;Initial Catalog=Quartz;Integrated Security=True";
            properties["quartz.dataSource.default.provider"] = "SqlServer";//3.0以上不再带版本

            ISchedulerFactory schedulerFactory = new StdSchedulerFactory(properties);
            IScheduler scheduler = await schedulerFactory.GetScheduler();
            await scheduler.Start();
            var jobKey = JobKey.Create("myjob8", "group");
            if (await scheduler.CheckExists(jobKey))
            {
                Console.WriteLine("当前job已经存在，无需调度:{0}", jobKey.ToString());
            }
            else
            {
                IJobDetail job = JobBuilder.Create<QuestionJob>()
                            .WithDescription("使用quartz进行持久化存储")
                            .StoreDurably()
                            .RequestRecovery()
                            .WithIdentity(jobKey)
                            .UsingJobData("count", 1)
                            .Build();
                //定义触发器
                ITrigger trigger = null;
                trigger = TriggerBuilder.Create().WithSimpleSchedule(x => x.WithIntervalInSeconds(2)
                                                 .RepeatForever())
                                                 .Build();
                //trigger = TriggerBuilder.Create().WithCalendarIntervalSchedule((c) =>
                // {

                //     c.WithInterval(6, IntervalUnit.Month);

                // }).Build();
                scheduler.ListenerManager.AddJobListener(new Listener.JobListener());
                await scheduler.ScheduleJob(job, trigger);

            }

        }
    }
}
