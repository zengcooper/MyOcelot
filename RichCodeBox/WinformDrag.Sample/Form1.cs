using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace WinformDrag.Sample
{

    /// <summary>
    /// 
    /// </summary>
    public partial class Form1 : Form
    {
        bool isPress = false;

        Point mouseOffset = new Point(0, 0);


        /// <summary>
        /// 
        /// </summary>
        public Form1()
        {
            InitializeComponent();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            this.isPress = true;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Load(object sender, EventArgs e)
        {
            this.dataGridView1.AutoGenerateColumns = false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (this.isPress)
            {
                this.Location = new Point(Control.MousePosition.X, Control.MousePosition.Y);
            }
            this.lblXV.Text = Control.MousePosition.X.ToString();
            this.lblYV.Text = Control.MousePosition.Y.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            this.isPress = false;
        }
    }
}
