using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace QuartzApp.Listener
{

    /// <summary>
    /// 
    /// </summary>
    public class JobListener : IJobListener
    {
        /// <summary>
        /// 
        /// </summary>
        public string Name => "JobListener-0001";


        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task JobExecutionVetoed(IJobExecutionContext context, CancellationToken cancellationToken = default(CancellationToken))
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("JobExecutionVetoed");
            return Task.Delay(0);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task JobToBeExecuted(IJobExecutionContext context, CancellationToken cancellationToken = default(CancellationToken))
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("JobToBeExecuted");
            return Task.Delay(0);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="jobException"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task JobWasExecuted(IJobExecutionContext context, JobExecutionException jobException, CancellationToken cancellationToken = default(CancellationToken))
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("JobWasExecuted");
            return Task.Delay(0);
        }
    }
}
