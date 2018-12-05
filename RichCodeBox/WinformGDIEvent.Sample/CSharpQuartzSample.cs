using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinformGDIEvent.Sample
{

    /// <summary>
    /// 
    /// </summary>
    public partial class CSharpQuartzSample : Form
    {

        /// <summary>
        /// 
        /// </summary>
        public CSharpQuartzSample()
        {
            this.DoubleBuffered = true;
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw | ControlStyles.AllPaintingInWmPaint, true);
            InitializeComponent();
        }

        /// <summary>
        /// 
        /// </summary>
        private CSharpQuartz sharpQuartz = null;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CSharpQuartzSample_Load(object sender, EventArgs e)
        {
            float w = 300f, h = 300f;
            float x = (this.Width - w) / 2;
            float y = (this.Height - h) / 2;
            sharpQuartz = new CSharpQuartz(this, x, y, w);
            sharpQuartz.OnChanged += SharpQuartz_OnChanged;
            sharpQuartz.Start();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SharpQuartz_OnChanged(object sender, EventArgs e)
        {

            if (lblTime.IsHandleCreated)
            {
                lblTime.Invoke(new Action(() =>
                {
                    lblTime.Text = DateTime.Now.ToString("当前时间：HH:mm:ss");
                }));
            }
        }
    }
}
