namespace WinFormGDI.Sample
{
    partial class FormImageEditor
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
            this.gpbTools = new System.Windows.Forms.GroupBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.文件ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SaveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.OpenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblEvent = new System.Windows.Forms.Label();
            this.lblCursor = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lblToolState = new System.Windows.Forms.Label();
            this.ExitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnEraser = new System.Windows.Forms.Button();
            this.btnRetancle = new System.Windows.Forms.Button();
            this.btnLine = new System.Windows.Forms.Button();
            this.btnCircle = new System.Windows.Forms.Button();
            this.btnColor = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.pboxContainer = new System.Windows.Forms.PictureBox();
            this.gpbTools.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pboxContainer)).BeginInit();
            this.SuspendLayout();
            // 
            // gpbTools
            // 
            this.gpbTools.Controls.Add(this.lblToolState);
            this.gpbTools.Controls.Add(this.btnRetancle);
            this.gpbTools.Controls.Add(this.btnLine);
            this.gpbTools.Controls.Add(this.btnCircle);
            this.gpbTools.Controls.Add(this.btnColor);
            this.gpbTools.Controls.Add(this.btnClear);
            this.gpbTools.Controls.Add(this.groupBox2);
            this.gpbTools.Location = new System.Drawing.Point(684, 12);
            this.gpbTools.Name = "gpbTools";
            this.gpbTools.Size = new System.Drawing.Size(158, 460);
            this.gpbTools.TabIndex = 1;
            this.gpbTools.TabStop = false;
            this.gpbTools.Text = "工具";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.文件ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(854, 25);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 文件ToolStripMenuItem
            // 
            this.文件ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.SaveAsToolStripMenuItem,
            this.OpenToolStripMenuItem,
            this.ExitToolStripMenuItem});
            this.文件ToolStripMenuItem.Name = "文件ToolStripMenuItem";
            this.文件ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.文件ToolStripMenuItem.Text = "文件";
            // 
            // SaveAsToolStripMenuItem
            // 
            this.SaveAsToolStripMenuItem.Name = "SaveAsToolStripMenuItem";
            this.SaveAsToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.SaveAsToolStripMenuItem.Text = "另存为";
            this.SaveAsToolStripMenuItem.Click += new System.EventHandler(this.SaveAsToolStripMenuItem_Click);
            // 
            // OpenToolStripMenuItem
            // 
            this.OpenToolStripMenuItem.Name = "OpenToolStripMenuItem";
            this.OpenToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.OpenToolStripMenuItem.Text = "打开图片";
            this.OpenToolStripMenuItem.Click += new System.EventHandler(this.OpenToolStripMenuItem_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.Filter = "图片文件(*.jpg)|*.jpg|png|*.png|gif|*.gif";
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.DefaultExt = "jpg";
            this.saveFileDialog1.Filter = "(*.jpg)|*.jpg|PNG(*.PNG)|*.png|bmp|*.bmp";
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.SystemColors.Control;
            this.groupBox1.Location = new System.Drawing.Point(12, 26);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(666, 446);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "画板";
            // 
            // lblEvent
            // 
            this.lblEvent.AutoSize = true;
            this.lblEvent.Location = new System.Drawing.Point(17, 483);
            this.lblEvent.Name = "lblEvent";
            this.lblEvent.Size = new System.Drawing.Size(29, 12);
            this.lblEvent.TabIndex = 4;
            this.lblEvent.Text = "事件";
            // 
            // lblCursor
            // 
            this.lblCursor.AutoSize = true;
            this.lblCursor.Location = new System.Drawing.Point(588, 484);
            this.lblCursor.Name = "lblCursor";
            this.lblCursor.Size = new System.Drawing.Size(29, 12);
            this.lblCursor.TabIndex = 5;
            this.lblCursor.Text = "坐标";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnEraser);
            this.groupBox2.Location = new System.Drawing.Point(7, 129);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(145, 109);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            // 
            // lblToolState
            // 
            this.lblToolState.AutoSize = true;
            this.lblToolState.Location = new System.Drawing.Point(13, 119);
            this.lblToolState.Name = "lblToolState";
            this.lblToolState.Size = new System.Drawing.Size(29, 12);
            this.lblToolState.TabIndex = 6;
            this.lblToolState.Text = "工具";
            // 
            // ExitToolStripMenuItem
            // 
            this.ExitToolStripMenuItem.Name = "ExitToolStripMenuItem";
            this.ExitToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.ExitToolStripMenuItem.Text = "退出";
            this.ExitToolStripMenuItem.Click += new System.EventHandler(this.ExitToolStripMenuItem_Click);
            // 
            // btnEraser
            // 
            this.btnEraser.Image = global::WinFormGDI.Sample.Properties.Resources.eraser;
            this.btnEraser.Location = new System.Drawing.Point(52, 65);
            this.btnEraser.Name = "btnEraser";
            this.btnEraser.Size = new System.Drawing.Size(36, 31);
            this.btnEraser.TabIndex = 7;
            this.btnEraser.UseVisualStyleBackColor = true;
            this.btnEraser.Click += new System.EventHandler(this.btnEraser_Click);
            // 
            // btnRetancle
            // 
            this.btnRetancle.Image = global::WinFormGDI.Sample.Properties.Resources.矩形1;
            this.btnRetancle.Location = new System.Drawing.Point(20, 157);
            this.btnRetancle.Name = "btnRetancle";
            this.btnRetancle.Size = new System.Drawing.Size(36, 31);
            this.btnRetancle.TabIndex = 4;
            this.btnRetancle.UseVisualStyleBackColor = true;
            this.btnRetancle.Click += new System.EventHandler(this.btnRetancle_Click);
            // 
            // btnLine
            // 
            this.btnLine.Image = global::WinFormGDI.Sample.Properties.Resources.线条;
            this.btnLine.Location = new System.Drawing.Point(59, 157);
            this.btnLine.Name = "btnLine";
            this.btnLine.Size = new System.Drawing.Size(36, 31);
            this.btnLine.TabIndex = 3;
            this.btnLine.UseVisualStyleBackColor = true;
            this.btnLine.Click += new System.EventHandler(this.btnLine_Click);
            // 
            // btnCircle
            // 
            this.btnCircle.Image = global::WinFormGDI.Sample.Properties.Resources.椭圆线条;
            this.btnCircle.Location = new System.Drawing.Point(101, 158);
            this.btnCircle.Name = "btnCircle";
            this.btnCircle.Size = new System.Drawing.Size(36, 31);
            this.btnCircle.TabIndex = 2;
            this.btnCircle.UseVisualStyleBackColor = true;
            this.btnCircle.Click += new System.EventHandler(this.btnCircle_Click);
            // 
            // btnColor
            // 
            this.btnColor.Image = global::WinFormGDI.Sample.Properties.Resources.调色板_palette3;
            this.btnColor.Location = new System.Drawing.Point(20, 194);
            this.btnColor.Name = "btnColor";
            this.btnColor.Size = new System.Drawing.Size(36, 31);
            this.btnColor.TabIndex = 1;
            this.btnColor.UseVisualStyleBackColor = true;
            this.btnColor.Click += new System.EventHandler(this.btnColor_Click);
            // 
            // btnClear
            // 
            this.btnClear.Image = global::WinFormGDI.Sample.Properties.Resources._721编辑器_清空文档;
            this.btnClear.Location = new System.Drawing.Point(111, 414);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(41, 40);
            this.btnClear.TabIndex = 0;
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // pboxContainer
            // 
            this.pboxContainer.BackColor = System.Drawing.Color.White;
            this.pboxContainer.Location = new System.Drawing.Point(16, 39);
            this.pboxContainer.Name = "pboxContainer";
            this.pboxContainer.Size = new System.Drawing.Size(658, 430);
            this.pboxContainer.TabIndex = 0;
            this.pboxContainer.TabStop = false;
            this.pboxContainer.Paint += new System.Windows.Forms.PaintEventHandler(this.pboxContainer_Paint);
            this.pboxContainer.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pboxContainer_MouseDown);
            this.pboxContainer.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pboxContainer_MouseMove);
            this.pboxContainer.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pboxContainer_MouseUp);
            // 
            // FormImageEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(854, 506);
            this.Controls.Add(this.lblCursor);
            this.Controls.Add(this.lblEvent);
            this.Controls.Add(this.gpbTools);
            this.Controls.Add(this.pboxContainer);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FormImageEditor";
            this.Text = "GDI+示例程序 www.lichaoqiang.com";
            this.Load += new System.EventHandler(this.FormImageEditor_Load);
            this.gpbTools.ResumeLayout(false);
            this.gpbTools.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pboxContainer)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pboxContainer;
        private System.Windows.Forms.GroupBox gpbTools;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 文件ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem SaveAsToolStripMenuItem;
        private System.Windows.Forms.Button btnColor;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblEvent;
        private System.Windows.Forms.Label lblCursor;
        private System.Windows.Forms.ToolStripMenuItem OpenToolStripMenuItem;
        private System.Windows.Forms.Button btnCircle;
        private System.Windows.Forms.Button btnLine;
        private System.Windows.Forms.Button btnRetancle;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label lblToolState;
        private System.Windows.Forms.ToolStripMenuItem ExitToolStripMenuItem;
        private System.Windows.Forms.Button btnEraser;
    }
}