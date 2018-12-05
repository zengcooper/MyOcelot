using System;
using System.IO;

namespace IOApp
{

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
            for (var j = 0; j < 10; j++)
            {
                System.Threading.ThreadPool.QueueUserWorkItem((stat) =>
                {
                    int i = 0;
                    while (true)
                    {
                        byte[] buffer = System.Text.Encoding.UTF8.GetBytes($"{i}-This is a text!" + Environment.NewLine);
                        Write(buffer);
                        Console.WriteLine("Write Ok!");
                        if (i > 10) break;
                        i++;
                    }
                });
            }
            Console.ReadLine();
        }

        /// <summary>
        /// 
        /// </summary>
        static object objLocker = new object();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="buffer"></param>
        static void Write(byte[] buffer)
        {
            string strSaveToPath = @"D:\demo.txt";
            using (FileStream fileStream = new FileStream(strSaveToPath, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite))
            {
                lock (objLocker)
                {
                    if (fileStream.CanWrite && fileStream.CanRead)
                    {
                        long length = fileStream.Length;
                        fileStream.Seek(length, SeekOrigin.Current);
                        fileStream.Lock(fileStream.Position, buffer.Length);
                        fileStream.Write(buffer, 0, buffer.Length);
                        fileStream.Unlock(length, buffer.Length);
                        fileStream.Flush();
                    }
                }

            }
        }
    }
}
