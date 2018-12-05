using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqToObject
{
    class Program
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            tb_CourseInfo course = new tb_CourseInfo();
            course.Names = "测试科目00000";
            course.ID = 9999;
            course.Orders = 100;
            string strConnectionString = "server=.;database=TiKuX2.0;uid=sa;pwd=zpdx0315";

            DataClasses1DataContext dbContext = new DataClasses1DataContext(strConnectionString);
            var updateCourseInfo = dbContext.GetTable<tb_CourseInfo>().FirstOrDefault((m) => m.ID == 9999);
            updateCourseInfo.Names = "测试科目，更新";

            //dbContext.tb_CourseInfo.InsertOnSubmit(course);
            dbContext.SubmitChanges();
            dbContext.Dispose();

            //var result = (from c in dbContext.tb_Chapter
            //              where c.CourseID == 1210
            //              select new { c.CptName, c.ID });

            //foreach (var item in result.ToList())
            //{
            //    Console.WriteLine(item.CptName);
            //}


            var result = (from c in dbContext.tb_Chapter
                          join b in dbContext.tb_CourseInfo
                              on c.CourseID equals b.ID
                          where c.CourseID == 1210
                          select new
                          {
                              c.CptName,
                              c.ID,
                              b.Names
                          });
            foreach (var item in result.ToList())
            {
                Console.WriteLine("【{0}】{1}", item.Names, item.CptName);
            }
        }
    }
}
