using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormGDI.Sample.Domain
{
    /// <summary>
    /// 图形数据
    /// </summary>
    public class GraphicsData
    {

        /// <summary>
        /// 
        /// </summary>
        public Color Color { get; set; }


        /// <summary>
        /// 
        /// </summary>
        public DrawTool Tool { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public float BrushWidth { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public float X { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public float Y { get; set; }


        /// <summary>
        /// 
        /// </summary>
        public float Width { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public float Height { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class GraphicsPointData : GraphicsData
    {
        /// <summary>
        /// 
        /// </summary>
        public float EndX { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public float EndY { get; set; }
    }
}
