namespace WinformGDIEvent.Sample
{
    partial class Quartz
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblTime = new System.Windows.Forms.Label();
            this.btnQuadrant = new System.Windows.Forms.Button();
            this.btnSimpe = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblTime
            // 
            this.lblTime.AutoSize = true;
            this.lblTime.Location = new System.Drawing.Point(519, 474);
            this.lblTime.Name = "lblTime";
            this.lblTime.Size = new System.Drawing.Size(53, 12);
            this.lblTime.TabIndex = 0;
            this.lblTime.Text = "当前时间";
            // 
            // btnQuadrant
            // 
            this.btnQuadrant.Location = new System.Drawing.Point(19, 26);
            this.btnQuadrant.Name = "btnQuadrant";
            this.btnQuadrant.Size = new System.Drawing.Size(75, 23);
            this.btnQuadrant.TabIndex = 1;
            this.btnQuadrant.Text = "关闭象限";
            this.btnQuadrant.UseVisualStyleBackColor = true;
            this.btnQuadrant.Click += new System.EventHandler(this.btnQuadrant_Click);
            // 
            // btnSimpe
            // 
            this.btnSimpe.Location = new System.Drawing.Point(100, 25);
            this.btnSimpe.Name = "btnSimpe";
            this.btnSimpe.Size = new System.Drawing.Size(75, 23);
            this.btnSimpe.TabIndex = 2;
            this.btnSimpe.Text = "简易表盘";
            this.btnSimpe.UseVisualStyleBackColor = true;
            this.btnSimpe.Click += new System.EventHandler(this.btnSimpe_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnQuadrant);
            this.groupBox1.Controls.Add(this.btnSimpe);
            this.groupBox1.Location = new System.Drawing.Point(6, 441);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(671, 62);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "功能";
            // 
            // Quartz
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(689, 512);
            this.Controls.Add(this.lblTime);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "Quartz";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Quartz时钟 www.lichaoqiang.com";
            this.Load += new System.EventHandler(this.Quartz_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Quartz_Paint);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTime;
        private System.Windows.Forms.Button btnQuadrant;
        private System.Windows.Forms.Button btnSimpe;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}