using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

/******************************************************************************************************************
 * 
 * 
 * 说　明： 授权(版本：Version1.0.0)
 * 作　者：李朝强
 * 日　期: 2017-03-31 12:53:44
 * 修　改：2017-03-31 12:53:44
 * 参　考：http://my.oschina.net/lichaoqiang/ http://www.lichaoqiang.com
 * 备　注：暂无...
 * 
 * 
 * ***************************************************************************************************************/
namespace MiddleWareApp.MiddleWare
{

    /// <summary>
    /// 
    /// </summary>
    public class AppBuilder : IAppBuilder
    {

        /// <summary>
        /// 
        /// </summary>
        private List<Tuple<object, Delegate, object[]>> listMiddleWare = new List<Tuple<object, Delegate, object[]>>();

        #region IAppBuilder 成员

        /// <summary>
        /// 
        /// </summary>
        public void Use(object middleware, params object[] args)
        {
            if (null == middleware) return;

            if (middleware is Type)
            {
                Type middlewareType = middleware as Type;
                object middleWareObj = Activator.CreateInstance(middlewareType);
                Tuple<object, Delegate, object[]> tuple2 = new Tuple<object, Delegate, object[]>(middleWareObj, Delegate.CreateDelegate(typeof(Action), middleWareObj, Constants.INVOKE), args);
                listMiddleWare.Add(tuple2);
                return;
            }

            //如果是中间件对象类型，则
            Tuple<object, Delegate, object[]> tuple = new Tuple<object, Delegate, object[]>(middleware, Delegate.CreateDelegate(typeof(Action), middleware, Constants.INVOKE), args);
            listMiddleWare.Add(tuple);
        }
        #endregion


        /// <summary>
        /// 
        /// </summary>
        public void Build()
        {

            object next = null;
            foreach (Tuple<object, Delegate, object[]> item in listMiddleWare)
            {
                Type middleWareType = item.Item1 as Type;//中间件类型
                if (middleWareType == null) middleWareType = item.Item1.GetType();
                if (next != null)
                {
                    Type nextType = next as Type;
                    if (nextType == null) nextType = next.GetType();

                    //中间件的构造函数
                    ConstructorInfo constructorInfo = nextType.GetConstructor(new Type[] { middleWareType });

                    ParameterInfo[] paramtersInfos = constructorInfo.GetParameters();//获取构造函数所有参数
                    constructorInfo.Invoke(next, new object[] { item.Item1 });
                }

                next = item.Item1;
            }
            var firstMiddleware = listMiddleWare.FirstOrDefault();
            firstMiddleware.Item2.DynamicInvoke(firstMiddleware.Item3);
        }


    }
}
