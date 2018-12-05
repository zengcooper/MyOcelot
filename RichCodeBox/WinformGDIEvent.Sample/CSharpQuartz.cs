using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

/*=================================================================================================
*
* Title:C#开发的简易时钟
* Author:李朝强
* Description:模块描述
* CreatedBy:lichaoqiang.com
* CreatedOn:
* ModifyBy:暂无...
* ModifyOn:
* Company:河南天一文化传播股份有限公司
* Blog:http://www.lichaoqiang.com
* Mark:
*
*** ================================================================================================*/
namespace WinformGDIEvent.Sample
{

    /// <summary>
    /// <![CDATA[CSharpQuarz GDI时钟]]>
    /// </summary>
    public class CSharpQuartz : IDisposable
    {

        /// <summary>
        /// 定时器
        /// </summary>
        private Thread timer = null;

        /// <summary>
        /// X坐标
        /// </summary>
        public float X { get; private set; }

        /// <summary>
        /// Y坐标
        /// </summary>
        public float Y { get; private set; }

        /// <summary>
        /// 半径
        /// </summary>
        private float r;

        /// <summary>
        /// 画布
        /// </summary>
        private Graphics Graphics = null;

        /// <summary>
        /// 直径
        /// </summary>
        public float Diameter { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public Form CurrentWinform { get; private set; }


        /// <summary>
        /// 
        /// </summary>
        private EventHandler _OnChanged;

        /// <summary>
        /// 事件，时钟状态更新后，当前频次1秒钟
        /// </summary>
        public event EventHandler OnChanged
        {
            add
            {
                this._OnChanged += value;
            }
            remove
            {
                this._OnChanged -= value;
            }
        }


        /// <summary>
        /// 构造函数
        /// </summary>
        CSharpQuartz()
        {
            //
            timer = new Thread((() =>
            {
                if (Graphics == null)
                {
                    Graphics = CurrentWinform.CreateGraphics();//创建画布
                    Graphics.SmoothingMode = SmoothingMode.HighQuality; //高质量
                    Graphics.PixelOffsetMode = PixelOffsetMode.HighQuality; //高像素偏移质量
                }
                while (true)
                {
                    //清除画布颜色，以窗体底色填充
                    Graphics.Clear(CurrentWinform.BackColor);
                    DrawCaller();//绘制时钟
                    //事件
                    if (_OnChanged != null) _OnChanged(this, null);
                    System.Threading.Thread.Sleep(1000);                                                                                                                                                                                           
                }
            }));
            timer.IsBackground = true;
        }

        /// <summary>
        /// <![CDATA[构造函数]]>
        /// </summary>
        /// <param name="x"><![CDATA[圆x坐标]]></param>
        /// <param name="y"><![CDATA[圆y坐标]]></param>
        /// <param name="d"><![CDATA[圆直径]]></param>
        public CSharpQuartz(Form form, float x, float y, float d)
            : this()
        {
            this.CurrentWinform = form;
            this.X = x;
            this.Y = y;
            this.Diameter = d;
            r = d / 2;
        }


        /// <summary>
        /// 
        /// </summary>
        public void Start()
        {
            if (timer.IsAlive == false) timer.Start();//启动工作线程
        }

        /// <summary>
        /// 终止
        /// </summary>
        public void Stop()
        {
            if (timer.IsAlive == true) timer.Abort();//终止工作线程
        }

        /// <summary>
        /// <![CDATA[调用绘图]]>
        /// </summary>
        private void DrawCaller()
        {
            Graphics.SmoothingMode = SmoothingMode.HighQuality; //高质量
            Graphics.PixelOffsetMode = PixelOffsetMode.HighQuality; //高像素偏移质量
            using (Pen pen = new Pen(Color.Red, 2))
            {
                if (this.CurrentWinform.IsHandleCreated)
                {
                    this.CurrentWinform.Invoke(new Action(() =>
                    {
                        //绘制圆
                        Graphics.DrawEllipse(pen, new RectangleF(X, Y, Diameter, Diameter));

                        //绘制象限
                        DrawQuadrant();

                        //绘制时、分、秒等针
                        DrawQuartzShot();

                        //绘制时刻线
                        DrawQuartzLine();

                        //写入版本信息
                        WriteVersion();
                    }));
                }
            }
        }


        /// <summary>
        /// <![CDATA[绘制象限]]>
        /// </summary>
        private void DrawQuadrant()
        {
            #region  绘制象限
            float w = Diameter;
            float extend = 100f;
            using (Pen pen = new Pen(Color.Black, 1))
            {

                PointF point1 = new PointF(X - extend, Y + r);//
                PointF point2 = new PointF(X + w + extend, Y + r);

                PointF point3 = new PointF(X + r, Y - extend);//
                PointF point4 = new PointF(X + r, Y + w + extend);

                Graphics.DrawLine(pen, point1, point2);

                Graphics.DrawLine(pen, point3, point4);

            }
            #endregion 绘制象限
        }


        /// <summary>
        /// <![CDATA[绘制时、分、秒针]]>
        /// </summary>
        private void DrawQuartzShot()
        {
            //当前时间
            DateTime dtNow = DateTime.Now;
            int h = dtNow.Hour;
            int m = dtNow.Minute;
            int s = dtNow.Second;
            float ha = Convert.ToSingle(Math.PI * 2 / 12);//每小时所弧度 360/12格=30
            float radian = Convert.ToSingle(Math.PI * 2 / 60);//分秒偏移弧度 
            float x1, y1, offset = 60f;
            using (Pen pen = new Pen(Color.Green, 4))
            {
                //时针
                h = h >= 12 ? h - 12 : h;
                double angle = h * ha;//当前时针所占弧度
                x1 = X + r + Convert.ToSingle(Math.Sin(angle) * (r - offset));//通过调整offset的大小，可以控制时针的长短
                y1 = Y + r - Convert.ToSingle(Math.Cos(angle) * (r - offset));
                //圆心
                PointF pointEclipse = new PointF(X + r, Y + r);
                PointF pointEnd = new PointF(x1, y1);

                Graphics.DrawLine(pen, pointEclipse, pointEnd);//画45度角

                //分针
                using (Pen penYellow = new Pen(Color.Red, 2))
                {
                    offset = 30;//与分针长度成反比
                    //分
                    double angelMinutes = radian * m;//每分钟弧度
                    x1 = X + r + Convert.ToSingle(Math.Sin(angelMinutes) * (r - offset));//通过调整offset的大小，可以控制时针的长短
                    y1 = Y + r - Convert.ToSingle(Math.Cos(angelMinutes) * (r - offset));
                    Graphics.DrawLine(penYellow, pointEclipse, new PointF(x1, y1));//画45度角
                }

                //秒针
                using (Pen penYellow = new Pen(Color.Blue, 2))
                {
                    offset = 20;
                    //分
                    double angelSeconds = radian * s;//每秒钟弧度
                    x1 = X + r + Convert.ToSingle(Math.Sin(angelSeconds) * (r - offset));//通过调整offset的大小，可以控制时针的长短
                    y1 = Y + r - Convert.ToSingle(Math.Cos(angelSeconds) * (r - offset));
                    Graphics.DrawLine(penYellow, pointEclipse, new PointF(x1, y1));//画45度角
                }
            }
        }


        /// <summary>
        /// <![CDATA[绘制时刻线]]>
        /// </summary>
        private void DrawQuartzLine()
        {
            //圆心
            PointF pointEclipse = new PointF(X + r, Y + r);
            float labelX, labelY;//文本坐标
            float angle = Convert.ToSingle(Math.PI / 6);//角度 30度
            using (Font font = new Font(FontFamily.GenericSerif, 12))
            {
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
                                    _x = X + r - x1;
                                    _y = Y + r + y1;
                                    labelX = _x - 8;
                                    labelY = _y;
                                }
                                else
                                {
                                    //第四象限
                                    x1 = Convert.ToSingle(r * Math.Cos(pAngle - Math.PI));
                                    y1 = Convert.ToSingle(r * Math.Sin(pAngle - Math.PI));
                                    _x = X + r + x1;
                                    _y = Y + r + y1;
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
                                _x = X + r + x1;
                                _y = Y + r - y1;
                                labelX = _x;
                                labelY = _y - 20;
                            }
                            else
                            {
                                //第二象限
                                x1 = Convert.ToSingle(r * Math.Cos(pAngle));
                                y1 = Convert.ToSingle(r * Math.Sin(pAngle));
                                _x = X + r - x1;
                                _y = Y + r - y1;
                                labelX = _x - 15;
                                labelY = _y - 20;
                            }
                            //上半圆 分成12份，每份 30度
                            if (i + 9 > 12)
                            {
                                Graphics.DrawString((i + 9 - 12).ToString(), font, brush, labelX, labelY);
                            }
                            else
                            {
                                if (i + 9 == 9)
                                {
                                    labelX = X - 13;
                                    labelY = Y + r - 6;
                                }
                                Graphics.DrawString((i + 9).ToString(), font, brush, labelX, labelY);
                            }
                            p10 = new PointF(_x, _y);
                            Graphics.DrawLine(pen, pointEclipse, p10);
                        }
                    }
                }
            }
        }


        /// <summary>
        /// <![CDATA[写入版本信息]]>
        /// </summary>
        private void WriteVersion()
        {
            PointF point = new PointF(X + r / 4, Y + r - 30);
            using (Font font = new Font(FontFamily.GenericSansSerif, 18))
            {
                using (Brush brush = new SolidBrush(Color.Black))
                {
                    this.Graphics.DrawString("Quartz", font, brush, point);
                }
            }
        }


        /// <summary>
        /// <![CDATA[释放]]>
        /// </summary>
        /// <param name="isDispose"></param>
        private void Dispose(bool isDispose)
        {
           
            if (isDispose)
            {
                timer.Abort();
                this.Graphics.Dispose();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
        }
    }
}