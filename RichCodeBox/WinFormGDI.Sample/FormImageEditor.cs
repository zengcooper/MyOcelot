using System.Windows.Forms;
using System.Drawing;
using System;
using System.Drawing.Drawing2D;
using System.Collections.Generic;
using System.Drawing.Imaging;

/*=================================================================================================
*
* Title:GDI+示例程序
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
namespace WinFormGDI.Sample
{

    /// <summary>
    /// 
    /// </summary>
    public partial class FormImageEditor : Form
    {

        float startX, startY;
        float endX, endY;
        bool isDrawing = false;
        bool isReady = false;
        Color brushColor = Color.Black;

        DrawTool tool = DrawTool.Line;//默认线条工具

        /// <summary>
        /// 
        /// </summary>
        Graphics graphics = null;

        /// <summary>
        /// 
        /// </summary>
        Image image = null;


        /// <summary>
        /// 画图轨迹
        /// </summary>
        List<Domain.GraphicsData> graphicsPath = new List<Domain.GraphicsData>();

        /// <summary>
        /// 
        /// </summary>
        public FormImageEditor()
        {
            InitializeComponent();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pboxContainer_MouseDown(object sender, MouseEventArgs e)
        {
            this.Cursor = Cursors.Hand;
            isDrawing = true;
            this.startX = e.X;
            this.startY = e.Y;
            ShowPoint("事件：按下鼠标事件", e.X, e.Y);
            Bitmap b;
            
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pboxContainer_MouseMove(object sender, MouseEventArgs e)
        {
            Bitmap bitmap;
            bitmap.LockBits(null,ImageLockMode.ReadOnly,PixelFormat.b)
    
            this.endX = e.X;
            this.endY = e.Y;
            ShowPoint("事件：鼠标移动事件", e.X, e.Y);
            if (isDrawing)
            {
                if (graphics == null)
                {
                    graphics = Graphics.FromImage(image);
                }
                switch (tool)
                {
                    //线条
                    case DrawTool.Line:
                        DrawLine(graphics);
                        this.startX = e.X;
                        this.startY = e.Y;
                        break;
                    //椭圆
                    case DrawTool.Eclips:
                        graphics.Clear(Color.White);
                        DrawEclips(graphics);
                        break;
                    //矩形工具
                    case DrawTool.Rectangle:
                        graphics.Clear(Color.White);
                        DrawRectangle(graphics);
                        break;
                    //橡皮擦功能
                    case DrawTool.Eraser:
                        Eraser(e.X, e.Y, 15, 15);
                        break;
                }
            }

        }

        /// <summary>
        /// 清空
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClear_Click(object sender, EventArgs e)
        {
            if (graphics != null)
            {
                image = new Bitmap(this.pboxContainer.ClientRectangle.Width, this.pboxContainer.ClientRectangle.Height);
                graphics.Clear(Color.White);
                this.pboxContainer.Image = image;
                graphics = null;
                this.graphicsPath.Clear();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormImageEditor_Load(object sender, EventArgs e)
        {
            image = new Bitmap(this.pboxContainer.ClientRectangle.Width, this.pboxContainer.ClientRectangle.Height);
            ShowToolState();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pboxContainer_Paint(object sender, PaintEventArgs e)
        {
            if (isReady == false)
            {
                e.Graphics.Clear(Color.White);
                isReady = true;
            }
        }

        /// <summary>
        /// 另存为
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string strSavaPath = this.saveFileDialog1.FileName;
                image.Save(strSavaPath);
            }

        }


        /// <summary>
        /// 配色
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnColor_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                brushColor = colorDialog1.Color;
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pboxContainer_MouseUp(object sender, MouseEventArgs e)
        {
            this.Cursor = Cursors.Default;
            isDrawing = false;
            this.endX = e.X;
            this.endY = e.Y;
            ShowPoint("事件：松开鼠标事件", e.X, e.Y);
            float width = endX - startX;
            float height = endY - startY;

            //保存路径信息
            if (tool == DrawTool.Line)
            {
                //保存路径信息
                graphicsPath.Add(new Domain.GraphicsPointData()
                {
                    Tool = tool,
                    BrushWidth = 2,
                    Color = brushColor,
                    X = startX,
                    Y = startY,
                    Width = width,
                    Height = height,
                    EndX = e.X,
                    EndY = e.Y
                });
            }
            else
                graphicsPath.Add(new Domain.GraphicsData()
                {
                    Tool = tool,
                    BrushWidth = 2,
                    Color = brushColor,
                    X = startX,
                    Y = startY,
                    Width = width,
                    Height = height
                });
            //
            switch (tool)
            {
                //线条工具
                case DrawTool.Line:

                    break;
                //矩形工具
                case DrawTool.Rectangle:
                    break;
                //画圆
                case DrawTool.Eclips:

                    break;
            }

        }

        /// <summary>
        /// 打开图片
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OpenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                image = image = new Bitmap(this.openFileDialog1.FileName);
                this.pboxContainer.Image = image;
                this.graphics = null;
            }
        }


        /// <summary>
        /// 椭圆工具
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCircle_Click(object sender, EventArgs e)
        {
            Pen pen = new Pen(this.brushColor, 2);
            this.tool = DrawTool.Eclips;
            ShowToolState();
        }


        /// <summary>
        /// 线条
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLine_Click(object sender, EventArgs e)
        {
            this.tool = DrawTool.Line;
            ShowToolState();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRetancle_Click(object sender, EventArgs e)
        {
            this.tool = DrawTool.Rectangle;
            ShowToolState();
        }

        #region 私有方法
        /// <summary>
        /// 画线
        /// </summary>
        /// <param name="graphics"></param>
        private void DrawLine(Graphics graphics)
        {
            try
            {
                graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                //字体
                System.Drawing.Font font = new Font(FontFamily.GenericSansSerif, 20f);

                this.ReDraw();

                Domain.GraphicsPointData graphicsPointData = new Domain.GraphicsPointData()
                {
                    Tool = DrawTool.Line,
                    X = this.startX,
                    Y = this.startY,
                    EndX = this.endX,
                    EndY = this.endY,
                    Color = brushColor,
                    BrushWidth = 2,
                };
                this.graphicsPath.Add(graphicsPointData);

                //起止坐标点
                PointF point1 = new PointF(this.startX, this.startY);
                PointF point2 = new PointF(this.endX, this.endY);
                //笔刷
                {
                    using (Pen pen = new Pen(this.brushColor, 2))
                    {
                        graphics.DrawLine(pen, point1, point2);
                    }
                }
                this.pboxContainer.Image = image;

            }
            catch (OutOfMemoryException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        /// <summary>
        /// 画圆
        /// </summary>
        /// <param name="graphics"></param>
        private void DrawEclips(Graphics graphics)
        {
            graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            //笔刷
            float width = endX - startX;
            float height = endY - startY;

            //还原轨迹
            this.ReDraw();

            using (Pen pen = new Pen(brushColor, 2))
            {
                graphics.DrawEllipse(pen, startX, startY, width, height);
                pen.Dispose();
            }
            this.pboxContainer.Image = image;

        }

        /// <summary>
        /// 重绘
        /// </summary>
        private void ReDraw()
        {
            foreach (var item in graphicsPath)
            {
                using (Pen pen = new Pen(item.Color, item.BrushWidth))
                {
                    switch (item.Tool)
                    {
                        //线条 Domain.GraphicsPointData
                        case DrawTool.Line:
                            Domain.GraphicsPointData graphicsPointData = (Domain.GraphicsPointData)item;
                            graphics.DrawLine(pen, graphicsPointData.X, graphicsPointData.Y, graphicsPointData.EndX, graphicsPointData.EndY);
                            break;
                        //椭圆
                        case DrawTool.Eclips:
                            graphics.DrawEllipse(pen, item.X, item.Y, item.Width, item.Height);
                            break;
                        //矩形
                        case DrawTool.Rectangle:
                            graphics.DrawRectangle(pen, item.X, item.Y, item.Width, item.Height);
                            break;
                        //橡皮擦
                        case DrawTool.Eraser:
                            graphics.CompositingMode = CompositingMode.SourceCopy;
                            SolidBrush solidBrush = new SolidBrush(Color.Transparent);
                            graphics.FillRectangle(solidBrush, new RectangleF(item.X, item.Y, item.Width, item.Height));
                            this.pboxContainer.Image = image;
                            break;
                    }
                    pen.Dispose();
                }
            }
        }

        /// <summary>
        /// 橡皮擦
        /// </summary>
        private void Eraser(float x, float y, float w, float h)
        {
            if (graphics != null)
            {
                Domain.GraphicsPointData graphicsPointData = new Domain.GraphicsPointData()
                {
                    Tool = DrawTool.Eraser,
                    X = x,
                    Y = y,
                    EndX = x + w,
                    EndY = y + h,
                    Color = brushColor,
                    BrushWidth = 2,
                    Width = 15,
                    Height = 15,
                };
                this.graphicsPath.Add(graphicsPointData);
                graphics.CompositingMode = CompositingMode.SourceCopy;
                SolidBrush solidBrush = new SolidBrush(Color.Transparent);
                graphics.FillRectangle(solidBrush, new RectangleF(x, y, w, h));
                this.pboxContainer.Image = image;
            }
        }

        /// <summary>
        /// 矩形
        /// </summary>
        /// <param name="graphics"></param>
        private void DrawRectangle(Graphics graphics)
        {
            graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            //字体
            using (System.Drawing.Font font = new Font(FontFamily.GenericSansSerif, 20f))
            {

                //还原轨迹
                this.ReDraw();

                //笔刷
                using (Pen pen = new Pen(this.brushColor, 2))
                {
                    float width = (endX - startX);
                    float height = (endY - startY);
                    graphics.DrawRectangle(pen, startX, startY, width, height);
                    pen.Dispose();
                }
            }
            this.pboxContainer.Image = image;

        }

        /// <summary>
        /// 退出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
            Application.Exit();
        }

        /// <summary>
        /// 橡皮擦
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEraser_Click(object sender, EventArgs e)
        {
            this.tool = DrawTool.Eraser;
            ShowToolState();
        }

        /// <summary>
        ///显示坐标信息
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        private void ShowPoint(string format, float x, float y, params object[] args)
        {
            lblCursor.Text = string.Format("坐标 x:{0} y:{1}", x, y);
            lblEvent.Text = string.Format(format, args);
        }

        /// <summary>
        /// 显示当前工具
        /// </summary>
        private void ShowToolState()
        {
            switch (tool)
            {
                case DrawTool.Line:
                    lblToolState.Text = "当前工具：线条工具";
                    break;
                case DrawTool.Eclips:
                    lblToolState.Text = "当前工具：椭圆工具";
                    break;
                case DrawTool.Rectangle:
                    lblToolState.Text = "当前工具：矩形工具";
                    break;
                case DrawTool.Eraser:
                    lblToolState.Text = "当前工具：橡皮擦";
                    break;
            }
        }
        #endregion 私有方法

    }
}
