using Polly;
using System;
namespace PollyApp
{
    class Program
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            try
            {
                Console.Title = "Polly示例";
                //重试3次
                //Policy.Handle<ArgumentNullException>().Retry(3, (e, i) =>
                //{
                //    Console.WriteLine("Retry:{0}", i);

                //}).Execute(() =>
                //{
                //    throw new ArgumentNullException("id");
                //});

                //2>
                //Policy.Handle<ArgumentNullException>((e) =>
                //{
                //    Console.WriteLine(e.Message);
                //    return true;

                //}).Or<ArgumentOutOfRangeException>(e =>
                //{
                //    Console.WriteLine(e.Message);
                //    return (e.HResult == -2146233086);

                //}).Fallback(() =>
                //{
                //    Console.WriteLine("Fallback");

                //}).Execute(() =>
                //{
                //    throw new ArgumentOutOfRangeException();
                //});


                Circuit();
                Circuit();
                Circuit();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

            }
            finally { Console.ReadLine(); }

        }

        /// <summary>
        /// 
        /// </summary>
        static void Circuit()
        {
            var CircuitBreaker = Policy.Handle<InvalidCastException>().CircuitBreaker(1, TimeSpan.FromSeconds(5),
                     (e, t) =>
                     {

                         //Console.WriteLine(e.Message);
                         //阻断
                         Console.WriteLine("break");
                     },
                     () =>
                     {
                         //重置
                         Console.WriteLine("OnReset");

                     });
            //CircuitBreaker.Isolate();
            //CircuitBreaker.Reset();
            CircuitBreaker.Execute(() =>
            {
                throw new InvalidCastException();
            });
        }
    }
}
