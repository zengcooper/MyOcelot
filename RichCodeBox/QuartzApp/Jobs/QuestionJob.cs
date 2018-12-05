using Quartz;
using System.Threading.Tasks;
/*
 sql脚本：https://github.com/quartznet/quartznet/tree/master/database/tables
     */
namespace QuartzApp.Jobs
{
    public class QuestionJob : IJob
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly static log4net.ILog logger = log4net.LogManager.GetLogger(typeof(QuestionJob));

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public Task Execute(IJobExecutionContext context)
        {
            logger.Info("执行了QuestionJob");
            return Task.CompletedTask;
        }
    }
}