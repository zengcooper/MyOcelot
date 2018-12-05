using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.Interception.PolicyInjection.Pipeline;

namespace UnityApp.Sample.Interception
{

    /// <summary>
    /// <![CDATA[调用处理程序]]>
    /// </summary>
    public class TestCallHanlder : ICallHandler
    {
        /// <summary>
        /// 
        /// </summary>
        public int Order { get; set; }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <param name="getNext"></param>
        /// <returns></returns>
        public IMethodReturn Invoke(IMethodInvocation input, GetNextHandlerDelegate getNext)
        {
            return getNext().Invoke(input, null);
        }
    }
}
