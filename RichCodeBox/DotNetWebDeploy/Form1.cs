using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DotNetWebDeploy
{
    public partial class Form1 : Form
    {

        /// <summary>
        /// 
        /// </summary>
        public Form1()
        {
            InitializeComponent();
        }


        /// <summary>
        /// 生成解决方案
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnBuild_Click(object sender, EventArgs e)
        {
            string strSolutionFile = SolutionFileDialog.FileName;
            if (string.IsNullOrEmpty(strSolutionFile)) return;
            System.Diagnostics.Process process = new System.Diagnostics.Process();
            System.Diagnostics.ProcessStartInfo processStartInfo = new System.Diagnostics.ProcessStartInfo();
            processStartInfo.RedirectStandardOutput = true;
            processStartInfo.RedirectStandardInput = true;
            processStartInfo.UseShellExecute = false;
            processStartInfo.CreateNoWindow = true;
            processStartInfo.RedirectStandardError = true;
            processStartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Normal;
            processStartInfo.FileName = @"cmd.exe";

            process.StartInfo = processStartInfo;
            process.Start();

            process.StandardInput.WriteLine($@"C:\Windows\Microsoft.NET\Framework64\v4.0.30319\MSBuild.exe {strSolutionFile} /t:Rebuild /p:Configuration=Release /p:WebProjectOutputDir=D:\Jenkins_Publish /p:OutputPath=D:\Jenkins_Publish\bin");
            process.StandardInput.WriteLine("exit");
            string strRead = process.StandardOutput.ReadToEnd();
            txtOutput.Text = strRead;
            process.Close();

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSelectFile_Click(object sender, EventArgs e)
        {
            if (SolutionFileDialog.ShowDialog() == DialogResult.OK)
            {
                txtProjectFile.Text = SolutionFileDialog.FileName;
            }
        }
    }
}
