using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;
using Unity.Interception.PolicyInjection.Pipeline;
using Unity.Interception.PolicyInjection.Policies;

namespace UnityApp.Sample.Attributes
{

    /// <summary>
    /// 
    /// </summary>
    public class TestHandlerAttribute : HandlerAttribute
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="container"></param>
        /// <returns></returns>
        public override ICallHandler CreateHandler(IUnityContainer container)
        {
            return new Interception.TestCallHanlder()
            {
                Order = 1
            };
        }
    }
}
