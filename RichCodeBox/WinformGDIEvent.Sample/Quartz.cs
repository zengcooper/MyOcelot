using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinformGDIEvent.Sample
{

    /// <summary>
    /// 
    /// </summary>
    public partial class Quartz : Form
    {
        /// <summary>
        /// 
        /// </summary>
        private bool isStart = false;

        /// <summary>
        /// 
        /// </summary>
        private bool isShowQuadrant = true;

        /// <summary>
        /// 
        /// </summary>
        private bool isSimple = false;

        /// <summary>
        /// 
        /// </summary>
        Thread timer = null;

        /// <summary>
        /// 
        /// </summary>
        public Quartz()
        {
            InitializeComponent();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Quartz_Paint(object sender, PaintEventArgs e)
        {
            //graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            DrawCaller(e.Graphics);
            if (timer != null &&
                timer.IsAlive == false)
            {
                timer.Start();//启动线程
            }
        }



        /// <summary>
        /// <![CDATA[绘制时钟]]>
        /// </summary>
        /// <param name="graphics"></param>
        private void DrawCaller(Graphics graphics)
        {
            graphics.SmoothingMode = SmoothingMode.HighQuality; //高质量
            graphics.PixelOffsetMode = PixelOffsetMode.HighQuality; //高像素偏移质量
            using (Pen pen = new Pen(Color.Red, 2))
            {
                float w = 300f, h = 300f;
                float x = (this.Width - w) / 2;
                float y = (this.Height - h) / 2;

                float d = w;//直径
                float r = d / 2;//半径

                graphics.DrawEllipse(pen, new RectangleF(x, y, w, h));//绘制圆

                //绘制象限
                if (isShowQuadrant) DrawQuadrant(graphics, x, y, r);

                //绘制时、分、秒等针
                DrawQuartzShot(graphics, x, y, r);

                if (isSimple == false) DrawQuartzLine(graphics, x, y, r);


            }
        }

        /// <summary>
        /// <![CDATA[绘制象限]]>
        /// </summary>
        /// <param name="graphics"><![CDATA[画布]]></param>
        /// <param name="x"><![CDATA[圆x坐标]]></param>
        /// <param name="y"><![CDATA[圆y坐标]]></param>
        /// <param name="r"><![CDATA[圆半径]]></param>
        private void DrawQuadrant(Graphics graphics, float x, float y, float r)
        {
            float w = r * 2;
            float extend = 100f;
            using (Pen pen = new Pen(Color.Black, 1))
            {
                #region  绘制象限
                PointF point1 = new PointF(x - extend, y + r);//
                PointF point2 = new PointF(x + w + extend, y + r);

                PointF point3 = new PointF(x + r, y - extend);//
                PointF point4 = new PointF(x + r, y + w + extend);

                graphics.DrawLine(pen, point1, point2);

                graphics.DrawLine(pen, point3, point4);
                #endregion 绘制象限
            }

        }

        /// <summary>
        /// <![CDATA[绘制时、分、秒针]]>
        /// </summary>
        /// <param name="graphics"><![CDATA[画布]]></param>
        /// <param name="x"><![CDATA[圆x坐标]]></param>
        /// <param name="y"><![CDATA[圆y坐标]]></param>
        /// <param name="r"><![CDATA[圆半径]]></param>
        private void DrawQuartzShot(Graphics graphics, float x, float y, float r)
        {
            if (this.IsHandleCreated)
            {
                this.Invoke(new Action(() =>
                {
                    //当前时间
                    DateTime dtNow = DateTime.Now;
                    int h = dtNow.Hour;
                    int m = dtNow.Minute;
                    int s = dtNow.Second;
                    float ha = Convert.ToSingle(Math.PI * 2 / 12);//每小时所弧度 360/12格=30
                    float hm = Convert.ToSingle(Math.PI * 2 / 60);
                    float hs = Convert.ToSingle(Math.PI * 2 / 60);
                    float x1, y1, offset = 60f;
                    using (Pen pen = new Pen(Color.Green, 4))
                    {
                        //时针
                        h = h >= 12 ? h - 12 : h;
                        double angle = h * ha;//当前时针所占弧度
                        x1 = x + r + Convert.ToSingle(Math.Sin(angle) * (r - offset));//通过调整offset的大小，可以控制时针的长短
                        y1 = y + r - Convert.ToSingle(Math.Cos(angle) * (r - offset));
                        //圆心
                        PointF pointEclipse = new PointF(x + r, y + r);
                        PointF pointEnd = new PointF(x1, y1);

                        graphics.DrawLine(pen, pointEclipse, pointEnd);//画45度角

                        //分针
                        using (Pen penYellow = new Pen(Color.Red, 2))
                        {
                            offset = 30;
                            //分
                            double angelMinutes = hm * m;//每分钟弧度
                            x1 = x + r + Convert.ToSingle(Math.Sin(angelMinutes) * (r - offset));//通过调整offset的大小，可以控制时针的长短
                            y1 = y + r - Convert.ToSingle(Math.Cos(angelMinutes) * (r - offset));
                            graphics.DrawLine(penYellow, pointEclipse, new PointF(x1, y1));//画45度角
                        }

                        //秒针
                        using (Pen penYellow = new Pen(Color.Blue, 2))
                        {
                            offset = 20;
                            //分
                            double angelSeconds = hs * s;//每秒钟弧度
                            x1 = x + r + Convert.ToSingle(Math.Sin(angelSeconds) * (r - offset));//通过调整offset的大小，可以控制时针的长短
                            y1 = y + r - Convert.ToSingle(Math.Cos(angelSeconds) * (r - offset));
                            graphics.DrawLine(penYellow, pointEclipse, new PointF(x1, y1));//画45度角
                        }

                    }

                    this.lblTime.Text = string.Format("当前时间：{0}:{1}:{2}", h, m, s);
                }));

            }
        }


        /// <summary>
        /// <![CDATA[画时刻线 这里是以9点这个时间坐标为起点 进行360度]]>
        /// </summary>
        /// <param name="graphics"><![CDATA[画布]]></param>
        /// <param name="x"><![CDATA[圆x坐标]]></param>
        /// <param name="y"><![CDATA[圆y坐标]]></param>
        /// <param name="r"><![CDATA[圆x坐标]]></param>
        private void DrawQuartzLine(Graphics graphics, float x, float y, float r)
        {
            //圆心
            PointF pointEclipse = new PointF(x + r, y + r);
            float labelX, labelY;//文本坐标
            float angle = Convert.ToSingle(Math.PI / 6);//角度 30度
            Font font = new Font(FontFamily.GenericSerif, 12);
            float _x, _y;//圆上的坐标点
            using (Brush brush = new System.Drawing.SolidBrush(Color.Red))
            {
                using (Pen pen = new Pen(Color.Black, 0.6f))
                {
                    //一天12H，将圆分为12份  
                    for (int i = 0; i <= 11; i++)
                    {
                        PointF p10;//圆周上的点
                        float pAngle = angle * i;
                        float x1, y1;

                        //三、四象限
                        if (pAngle > Math.PI)
                        {
                            if ((pAngle - Math.PI) > Math.PI / 2)//钝角大于90度  
                            {
                                //第三象限
                                x1 = Convert.ToSingle(r * Math.Cos(Math.PI * 2 - pAngle));
                                y1 = Convert.ToSingle(r * Math.Sin(Math.PI * 2 - pAngle));
                                _x = x + r - x1;
                                _y = y + r + y1;
                                labelX = _x - 8;
                                labelY = _y;
                            }
                            else
                            {
                                //第四象限
                                x1 = Convert.ToSingle(r * Math.Cos(pAngle - Math.PI));
                                y1 = Convert.ToSingle(r * Math.Sin(pAngle - Math.PI));
                                _x = x + r + x1;
                                _y = y + r + y1;
                                labelX = _x;
                                labelY = _y;
                            }
                        }
                        //一、二象限
                        else if (pAngle > Math.PI / 2)//钝角大于90度
                        {
                            //第一象限
                            x1 = Convert.ToSingle(r * Math.Cos(Math.PI - pAngle));
                            y1 = Convert.ToSingle(r * Math.Sin(Math.PI - pAngle));
                            _x = x + r + x1;
                            _y = y + r - y1;
                            labelX = _x;
                            labelY = _y - 20;
                        }
                        else
                        {
                            //第二象限
                            x1 = Convert.ToSingle(r * Math.Cos(pAngle));
                            y1 = Convert.ToSingle(r * Math.Sin(pAngle));
                            _x = x + r - x1;
                            _y = y + r - y1;
                            labelX = _x - 15;
                            labelY = _y - 20;
                        }
                        //上半圆 分成12份，每份 30度
                        if (i + 9 > 12)
                        {
                            graphics.DrawString((i + 9 - 12).ToString(), font, brush, labelX, labelY);
                        }
                        else
                        {
                            if (i + 9 == 9)
                            {
                                labelX = x - 13;
                                labelY = y + r - 6;
                            }
                            graphics.DrawString((i + 9).ToString(), font, brush, labelX, labelY);
                        }
                        p10 = new PointF(_x, _y);
                        graphics.DrawLine(pen, pointEclipse, p10);
                    }
                }
            }
        }

        Graphics _graphics = null;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Quartz_Load(object sender, EventArgs e)
        {
            timer = new Thread(() =>
            {
                if (_graphics == null)
                {
                    _graphics = this.CreateGraphics();
                    _graphics.SmoothingMode = SmoothingMode.HighQuality; //高质量
                    _graphics.PixelOffsetMode = PixelOffsetMode.HighQuality; //高像素偏移质量
                }
                while (true)
                {
                    _graphics.Clear(this.BackColor);
                    DrawCaller(_graphics);
                    System.Threading.Thread.Sleep(1000);
                }
            });
            timer.IsBackground = true;
        }

        /// <summary>
        /// 关闭象限
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnQuadrant_Click(object sender, EventArgs e)
        {
            if (isShowQuadrant)
            {
                isShowQuadrant = false;
                btnQuadrant.Text = "打开象限";
            }
            //open
            else
            {
                isShowQuadrant = true;
                btnQuadrant.Text = "关闭象限";
            }
        }

        /// <summary>
        /// 简易表盘
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSimpe_Click(object sender, EventArgs e)
        {
            if (isSimple)
            {
                isSimple = false;
                btnSimpe.Text = "简易模式";
            }
            else
            {
                isSimple = true;
                btnSimpe.Text = "完整模式";
            }
        }
    }
}
