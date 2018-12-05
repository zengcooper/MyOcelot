using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinformGDIEvent.Sample
{

    /// <summary>
    /// 
    /// </summary>
    public partial class Form1 : Form
    {

        /// <summary>
        /// 
        /// </summary>
        public Form1()
        {
            //采用双缓冲技术的控件必需的设置
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.UserPaint, true);
            InitializeComponent();
        }

        /// <summary>
        /// 
        /// </summary>
        RectangleF rectangle;


        /// <summary>
        /// 委托
        /// </summary>
        private EventHandler _RectangelClickEvent = null;


        /// <summary>
        /// 定义事件
        /// </summary>
        event EventHandler RectangelClickEvent
        {
            add
            {
                _RectangelClickEvent += value;
            }
            remove
            {
                _RectangelClickEvent -= value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        bool isMoving = false;


        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gpbUI_Paint(object sender, PaintEventArgs e)
        {

        }


        /// <summary>
        /// 鼠标点击
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (CheckActive(e.X, e.Y))
                {
                    if (_RectangelClickEvent != null)
                    {
                        _RectangelClickEvent(this, e);
                    }
                }
            }
        }

        /// <summary>
        /// 窗体渲染
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            using (Pen pen = new Pen(Color.Black, 1))
            {
                float w = 100f, h = 100f;
                float x = (this.Width - this.Location.X - w) / 2;
                float y = (this.Height - this.Location.Y - h) / 2;
                rectangle = new RectangleF(x, y, w, h);
                DrawRectangle(e.Graphics, x, y);

            }
        }

        /// <summary>
        /// 窗体鼠标移动事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            ShowLocation(e.X, e.Y);
            if (CheckActive(e.X, e.Y))
            {
                Cursor = Cursors.Hand;
            }
            else
            {
                Cursor = Cursors.Default;
            }
            if (isMoving)
            {
                MoveRectangel(e.X, e.Y);
                startX = e.X;
                startY = e.Y;
            }
        }


        /// <summary>
        /// 验证鼠标是否在可用区域
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        private bool CheckActive(float x, float y)
        {
            return (x >= rectangle.X && x <= rectangle.Right &&
                    y >= rectangle.Y && y <= rectangle.Y + rectangle.Width);
        }

        /// <summary>
        /// 窗体载入事件 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Load(object sender, EventArgs e)
        {
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw | ControlStyles.AllPaintingInWmPaint, true);
            this.RectangelClickEvent += new EventHandler((o, ar) =>
            {
                MessageBox.Show("矩形区域点击事件");
            });
        }


        float startX, startY;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if (CheckActive(e.X, e.Y))
            {
                this.startX = e.X;
                this.startY = e.Y;
                this.Cursor = Cursors.Cross;
                isMoving = true;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            this.Cursor = Cursors.Default;
            isMoving = false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        private void ShowLocation(float x, float y)
        {
            lblPostion.Text = string.Format("当前坐标：x:{0}  y:{1}", x, y);
        }

        Graphics graphics = null;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        private void MoveRectangel(float x, float y)
        {
            //鼠标点击位置与矩形远点的差值
            float x1 = startX - rectangle.X;
            float y1 = startY - rectangle.Y;

            float x2 = x - x1;
            float y2 = y - y1;

            DrawRectangle(graphics ?? this.CreateGraphics(), x2, y2);
        }


        /// <summary>
        /// 绘制矩形区域
        /// </summary>
        /// <param name="graphics"></param>
        private void DrawRectangle(Graphics graphics, float x, float y)
        {
            graphics.Clear(this.BackColor);
            //graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            graphics.SmoothingMode = SmoothingMode.HighQuality; //高质量
            graphics.PixelOffsetMode = PixelOffsetMode.HighQuality; //高像素偏移质量
            using (Pen pen = new Pen(Color.Black, 1))
            {
                float w = 100, h = 100;
                rectangle = new RectangleF(x, y, w, h);
                graphics.DrawRectangle(pen, x, y, w, h);
                using (Brush brush = new SolidBrush(Color.Red))
                {
                    graphics.FillRectangle(brush, rectangle);
                }
            }

        }
    }
}
