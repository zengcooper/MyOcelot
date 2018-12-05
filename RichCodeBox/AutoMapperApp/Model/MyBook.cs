using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoMapperApp.Model
{

    /// <summary>
    /// 
    /// </summary>
    public class MyBook
    {

        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Author author { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string book_number { get; set; }
    }
}
