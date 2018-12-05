using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImageProcessingForm.Sample
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            Bitmap bitmap = new Bitmap(@"D:\paper\1456141019_地理_p1.tif");
            cn(bitmap);
        }

        /// <summary>
        /// <![CDATA[找出轮廓]]>
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public Bitmap cn(Bitmap x)
        {
            Color c1 = new Color();
            Color c2 = new Color();
            Color c3 = new Color();
            Color c4 = new Color();
            int i, j;
            int rr, gg, bb;//  
            int r1, r2, r3, r4, fxr, fyr;//  
            int g1, g2, g3, g4, fxg, fyg;//  
            int b1, b2, b3, b4, fxb, fyb;//  
                                         //把图片中的图片传给一个ＢＩＴＭＡＰ类型  
            Bitmap box1 = new Bitmap(x);
            for (i = 0; i <= x.Width - 2; i++)
            {
                for (j = 0; j <= x.Height - 2; j++)
                {
                    c1 = box1.GetPixel(i, j);
                    c2 = box1.GetPixel(i + 1, j + 1);
                    c3 = box1.GetPixel(i + 1, j);
                    c4 = box1.GetPixel(i, j + 1);
                    r1 = c1.R;
                    r2 = c2.R;
                    r3 = c3.R;
                    r4 = c4.R;
                    fxr = r1 - r2;
                    fyr = r3 - r4;
                    rr = Math.Abs(fxr) + Math.Abs(fyr) + 128;
                    if (rr < 0) rr = 0;
                    if (rr > 255) rr = 255;
                    g1 = c1.G;
                    g2 = c2.G;
                    g3 = c3.G;
                    g4 = c4.G;
                    fxg = g1 - g2;
                    fyg = g3 - g4;
                    gg = Math.Abs(fxg) + Math.Abs(fyg) + 128;
                    if (gg < 0) gg = 0;
                    if (gg > 255) gg = 255;
                    b1 = c1.B;
                    b2 = c2.B;
                    b3 = c3.B;
                    b4 = c4.B;
                    fxb = b1 - b2;
                    fyb = b3 - b4;
                    bb = Math.Abs(fxb) + Math.Abs(fyb) + 128;
                    if (bb < 0) bb = 0;
                    if (bb > 255) bb = 255;
                    Color cc = Color.FromArgb(rr, gg, bb);
                    box1.SetPixel(i, j, cc);
                }


            }
            box1.Save(@"D:\dd.jpg");
            return box1;
        }


    }
}
