﻿using System;
using System.Windows.Forms;

namespace WinformGDIEvent.Sample
{

    /// <summary>
    /// 
    /// </summary>
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
    

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new CSharpQuartzSample());
        }
    }
}
