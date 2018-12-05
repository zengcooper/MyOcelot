using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using OfficeOpenXml;
using OfficeOpenXml.Style;

namespace EPPlusApp
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
                Console.Title = "EPPlus操作Excel用法示例 www.lichaoqiang.com";
                string strExcelFile = @"E:\c.xlsx";
                FileInfo fileInfo = new FileInfo(strExcelFile);
                if (fileInfo.Exists) fileInfo.Delete();
                using (ExcelPackage paePackage = new ExcelPackage(fileInfo))
                {
                    ExcelWorkbook workbook = paePackage.Workbook;

                    ExcelWorksheet sheet = paePackage.Workbook.Worksheets.Add("000");
                    //sheet.Cells["A1:H6"].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                    //sheet.Cells.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                    //sheet.Cells.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    //sheet.Cells.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                    sheet.Cells[1, 1].Value = "A";

                    //    #region 读取
                    //    foreach (var sheet in workbook.Worksheets)
                    //    {
                    //        Console.WriteLine(sheet.Dimension.Start.Column);
                    //        for (int j = sheet.Dimension.Start.Row; j <= sheet.Dimension.End.Row; j++)
                    //        {
                    //            for (int i = sheet.Dimension.Start.Column; i <= sheet.Dimension.End.Column; i++)
                    //            {

                    //                string str = sheet.Cells[j, i].Text;
                    //                Console.WriteLine(str);
                    //            }
                    //        }

                    //        Console.WriteLine(sheet.Name);
                    //    }
                    //   
                    //}
                    //    #endregion 读取
                    paePackage.Save();

                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }
            finally
            {
                Console.ReadLine();
            }
        }
    }
}
