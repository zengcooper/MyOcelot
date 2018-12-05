using UnityApp.Sample.Strategy;

namespace UnityApp.Sample.Domain
{
    /// <summary>
    /// 
    /// </summary>
    public class User : IUser
    {
        /// <summary>
        /// 
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Name
        {
            get; set;
        }

        /// <summary>
        /// 
        /// </summary>
        public string Mobile
        {
            get; set;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Attributes.TestHandler]
        public IUser GetUserById()
        {
            Id = 1;
            Name = "lichaoqiang";
            Mobile = "1589067XXXX";
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"Id:{Id} Name:{Name} Mobile:{Mobile}";
        }
    }
}
