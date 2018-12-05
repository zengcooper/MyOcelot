namespace DotNetWebDeploy
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.SolutionFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.btnBuild = new System.Windows.Forms.Button();
            this.txtProjectFile = new System.Windows.Forms.TextBox();
            this.btnSelectFile = new System.Windows.Forms.Button();
            this.txtOutput = new System.Windows.Forms.TextBox();
            this.gpx_action = new System.Windows.Forms.GroupBox();
            this.SuspendLayout();
            // 
            // SolutionFileDialog
            // 
            this.SolutionFileDialog.DefaultExt = "*.sln|*.sln|*.csproj|*..csproj";
            this.SolutionFileDialog.Filter = "*.sln|*.sln|*.csproj|*.csproj";
            // 
            // btnBuild
            // 
            this.btnBuild.Location = new System.Drawing.Point(662, 73);
            this.btnBuild.Name = "btnBuild";
            this.btnBuild.Size = new System.Drawing.Size(112, 43);
            this.btnBuild.TabIndex = 0;
            this.btnBuild.Text = "生成解决方案";
            this.btnBuild.UseVisualStyleBackColor = true;
            this.btnBuild.Click += new System.EventHandler(this.btnBuild_Click);
            // 
            // txtProjectFile
            // 
            this.txtProjectFile.BackColor = System.Drawing.Color.White;
            this.txtProjectFile.Location = new System.Drawing.Point(33, 73);
            this.txtProjectFile.Multiline = true;
            this.txtProjectFile.Name = "txtProjectFile";
            this.txtProjectFile.ReadOnly = true;
            this.txtProjectFile.Size = new System.Drawing.Size(359, 27);
            this.txtProjectFile.TabIndex = 1;
            // 
            // btnSelectFile
            // 
            this.btnSelectFile.Location = new System.Drawing.Point(409, 76);
            this.btnSelectFile.Name = "btnSelectFile";
            this.btnSelectFile.Size = new System.Drawing.Size(75, 23);
            this.btnSelectFile.TabIndex = 2;
            this.btnSelectFile.Text = "选择项目文件";
            this.btnSelectFile.UseVisualStyleBackColor = true;
            this.btnSelectFile.Click += new System.EventHandler(this.btnSelectFile_Click);
            // 
            // txtOutput
            // 
            this.txtOutput.Location = new System.Drawing.Point(10, 297);
            this.txtOutput.Multiline = true;
            this.txtOutput.Name = "txtOutput";
            this.txtOutput.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtOutput.Size = new System.Drawing.Size(915, 206);
            this.txtOutput.TabIndex = 3;
            // 
            // gpx_action
            // 
            this.gpx_action.Location = new System.Drawing.Point(10, 16);
            this.gpx_action.Name = "gpx_action";
            this.gpx_action.Size = new System.Drawing.Size(915, 266);
            this.gpx_action.TabIndex = 4;
            this.gpx_action.TabStop = false;
            this.gpx_action.Text = "操作";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(946, 522);
            this.Controls.Add(this.txtOutput);
            this.Controls.Add(this.btnSelectFile);
            this.Controls.Add(this.txtProjectFile);
            this.Controls.Add(this.btnBuild);
            this.Controls.Add(this.gpx_action);
            this.Name = "Form1";
            this.Text = "DotNet部署工具";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog SolutionFileDialog;
        private System.Windows.Forms.Button btnBuild;
        private System.Windows.Forms.TextBox txtProjectFile;
        private System.Windows.Forms.Button btnSelectFile;
        private System.Windows.Forms.TextBox txtOutput;
        private System.Windows.Forms.GroupBox gpx_action;
    }
}

