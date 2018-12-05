using AutoMapperApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace AutoMapperApp
{
    /// <summary>
    /// <![CDATA[枚举]]>
    /// </summary>
    [Flags]
    public enum Operation
    {
        Create = 2,
        Delete = 4,
        Query = 5
    }

    /// <summary>
    /// 
    /// </summary>
    class Program
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {

            Console.WriteLine(4 % 5);
            Console.ReadLine();
            Operation operation = Operation.Create | Operation.Query;
            Console.WriteLine(operation);
            Console.ReadLine();
            //配置初始化
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<MyBook, Book>();
                cfg.RecognizeAlias("book_number", "ISBN");
            });

            //书本
            MyBook book = new MyBook()
            {
                book_number = "55552445545",
                Name = "朝花夕拾",
                author = new Author()
                {
                    firstName = "鲁",
                    lastName = "迅"
                }
            };

            var _book = AutoMapper.Mapper.Map<MyBook, Book>(book);
        }
    }
}
