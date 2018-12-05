using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormGDI.Sample
{

    /// <summary>
    /// 
    /// </summary>
    public partial class FormGraphics : Form
    {
        float startX = 0f, startY = 0f;
        float endX = 0f, endY = 0f;
        Bitmap image = null;
        Graphics graphics = null;
        bool isDrawing = false;


        /// <summary>
        /// 
        /// </summary>
        public FormGraphics()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.UserPaint, true);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormGraphics_Load(object sender, EventArgs e)
        {
            //graphics = Graphics.FromImage(new Bitmap(@"C:\Users\LiChaoQiang\Desktop\20180123\2009年中医执业医师资格考试真题参考答案及解析.png"));
            //image = new Bitmap(this.pictureBox1.Width, this.pictureBox1.Height, graphics);

            //实例化image对象
            image = new Bitmap(this.pictureBox1.Width, this.pictureBox1.Height);

            //从image对象中获取画布
            graphics = Graphics.FromImage(image);
            graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            using (Font font = new Font(FontFamily.GenericMonospace, 30))
            {
                using (Brush brush = Brushes.SkyBlue)
                {
                    this.graphics.DrawString("www.lichaoqiang.com", font, brush, new PointF(101, 100));
                    this.graphics.DrawString("www.lichaoqiang.com", font, brush, new PointF(101, 200));
                }
            }
            this.pictureBox1.Image = image;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            this.startX = e.X;
            this.startY = e.Y;
            this.isDrawing = true;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            this.endX = e.X;
            this.endY = e.Y;
            this.isDrawing = false;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDrawing)
            {
                using (Pen pen = new Pen(Color.Black, 4))
                {
                    this.endX = e.X;
                    this.endY = e.Y;
                    //graphics.DrawLine(pen, startX, startY, endX, endY);
                    //graphics.Clear(this.pictureBox1.BackColor);
                    graphics.DrawEllipse(pen, startX, startY, endX - startX, endY - startY);
                    //this.startX = e.X;
                    //this.startY = e.Y;
                    this.pictureBox1.Image = image;
                }
            }
        }
    }
}
