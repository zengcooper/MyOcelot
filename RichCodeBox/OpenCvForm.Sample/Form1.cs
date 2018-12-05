using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using Emgu.CV.Util;
using FeatureMatchingExample;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace OpenCvForm.Sample
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
        private void btnRetangle_Click(object sender, EventArgs e)
        {
            PerformShapeDetection();
        }


        /// <summary>
        /// 
        /// </summary>
        public void PerformShapeDetection()
        {
            {
                StringBuilder msgBuilder = new StringBuilder("Performance: ");

                int w = 1665, h = 2286;
                //Load the image from file and resize it for display
                Image<Bgr, Byte> img = new Image<Bgr, byte>(@"D:\paper\1456141019_地理_p1.tif").Resize(w, h, Emgu.CV.CvEnum.Inter.Linear, true);

                //图片转换为灰度
                UMat uimage = new UMat();
                CvInvoke.CvtColor(img, uimage, ColorConversion.Bgr2Gray);

                //去噪处理
                UMat pyrDown = new UMat();
                CvInvoke.PyrDown(uimage, pyrDown);
                CvInvoke.PyrUp(pyrDown, uimage);
                Stopwatch watch = Stopwatch.StartNew();
                UMat cannyEdges = new UMat();
                //Image<Gray, Byte> gray = img.Convert<Gray, Byte>().PyrDown().PyrUp();
                double cannyThreshold = 360.0;
#if false
                #region 1、识别圆形

              
                double circleAccumulatorThreshold = 120;
                CircleF[] circles = CvInvoke.HoughCircles(uimage, HoughType.Gradient, 2.0, 20.0, cannyThreshold, circleAccumulatorThreshold, 5);

                watch.Stop();
                msgBuilder.Append(String.Format("Hough circles - {0} ms; ", watch.ElapsedMilliseconds));
                #endregion
#endif
                #region Canny and edge detection
                watch.Reset(); watch.Start();
                double cannyThresholdLinking = 120.0;

                CvInvoke.Canny(uimage, cannyEdges, cannyThreshold, cannyThresholdLinking);

                LineSegment2D[] lines = CvInvoke.HoughLinesP(
                   cannyEdges,
                   1, //Distance resolution in pixel-related units
                   Math.PI, //Angle resolution measured in radians.
                   20, //threshold
                   30, //min Line width
                   10); //gap between lines 间隔

                watch.Stop();
                msgBuilder.Append(String.Format("Canny & Hough lines - {0} ms; ", watch.ElapsedMilliseconds));
                #endregion

                #region Find triangles and rectangles
                watch.Reset(); watch.Start();
                List<Triangle2DF> triangleList = new List<Triangle2DF>();
                List<RotatedRect> boxList = new List<RotatedRect>(); //a box is a rotated rectangle

                using (VectorOfVectorOfPoint contours = new VectorOfVectorOfPoint())
                {
                    CvInvoke.FindContours(cannyEdges, contours, null, RetrType.List, ChainApproxMethod.ChainApproxSimple);
                    int count = contours.Size;
                    for (int i = 0; i < count; i++)
                    {
                        using (VectorOfPoint contour = contours[i])
                        using (VectorOfPoint approxContour = new VectorOfPoint())
                        {
                            CvInvoke.ApproxPolyDP(contour, approxContour, CvInvoke.ArcLength(contour, true) * 0.05, true);
                            if (CvInvoke.ContourArea(approxContour, false) > 25) //only consider contours with area greater than 250
                            {
                                //三角形
                                if (approxContour.Size == 3) //The contour has 3 vertices, it is a triangle
                                {
                                    Point[] pts = approxContour.ToArray();
                                    triangleList.Add(new Triangle2DF(
                                       pts[0],
                                       pts[1],
                                       pts[2]));
                                }
                                //四条边的矩形
                                else if (approxContour.Size == 4) //The contour has 4 vertices.
                                {
                                    #region 确定轮廓所有的角度都在[ 80, 100 ]度之间
                                    bool isRectangle = true;
                                    Point[] pts = approxContour.ToArray();
                                    LineSegment2D[] edges = PointCollection.PolyLine(pts, true);

                                    for (int j = 0; j < edges.Length; j++)
                                    {
                                        double angle = Math.Abs(edges[(j + 1) % edges.Length].GetExteriorAngleDegree(edges[j]));//两个线条间的夹角
                                        if (angle < 80 || angle > 100)
                                        {
                                            isRectangle = false;
                                            break;
                                        }
                                    }
                                    #endregion

                                    if (isRectangle) boxList.Add(CvInvoke.MinAreaRect(approxContour));
                                }
                            }
                        }
                    }
                }

                watch.Stop();
                msgBuilder.Append(String.Format("Triangles & Rectangles - {0} ms; ", watch.ElapsedMilliseconds));
                #endregion 1、识别圆形

                this.Text = msgBuilder.ToString();
                img.Save("D:\\aaaa.tiff");
                #region draw triangles and rectangles
                Mat triangleRectangleImage = new Mat(img.Size, DepthType.Cv8U, 3);
                triangleRectangleImage.SetTo(new MCvScalar(0));//三角形
                //foreach (Triangle2DF triangle in triangleList)
                //{
                //    CvInvoke.Polylines(triangleRectangleImage, Array.ConvertAll(triangle.GetVertices(), Point.Round), true, new Bgr(Color.DarkBlue).MCvScalar, 2);
                //}
                foreach (RotatedRect box in boxList)
                {
                    CvInvoke.Polylines(triangleRectangleImage, Array.ConvertAll(box.GetVertices(), Point.Round), true, new Bgr(Color.DarkOrange).MCvScalar, 2);
                }
                triangleRectangleImage.Save("D:\\ccc.tiff");
                this.pictureBox1.Image = triangleRectangleImage.Bitmap;
                #endregion

            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnMatch_Click(object sender, EventArgs e)
        {

            long matchTime;
            using (Mat modelImage = CvInvoke.Imread("Template.png", ImreadModes.Grayscale))
            using (Mat observedImage = CvInvoke.Imread("1456141019_地理_p1.tif", ImreadModes.Grayscale))
            {
                VectorOfKeyPoint vectorOfKeyPoint = new VectorOfKeyPoint();
                VectorOfKeyPoint vectorOfKeyPointObserved = new VectorOfKeyPoint();
                VectorOfVectorOfDMatch vectorOfDMatch = new VectorOfVectorOfDMatch();
                MDMatch[][] mDMatch = vectorOfDMatch.ToArrayOfArray();
                Mat mask = null;
                Mat homography = null;
                DrawMatches.FindMatch(modelImage,
                    observedImage,
                    out matchTime,
                    out vectorOfKeyPoint,
                    out vectorOfKeyPointObserved,
                    vectorOfDMatch,
                    out mask,
                    out homography);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnTemplate_Click(object sender, EventArgs e)
        {
            Mat src = CvInvoke.Imread("1456141019_地理_p1.tif");
            Mat tempate = CvInvoke.Imread("Template.png");

            //var result = src.ToImage<Gray, float>().MatchTemplate(model.ToImage<Gray, float>(), TemplateMatchingType.Sqdiff);
            //result.Save(@"D:\ddddfadfd.jpg");
            /*
             * 
             *              （x, y）都包含了匹配矩阵的计算结果。在OpenCV中提供了6种匹配度量方法。 
                             (1).平方差匹配法CV_TM_SQDIFF 
                             这里写图片描述 
                             (2)归一化平方差匹配法CV_TM_SQDIFF_NORMED 
                             这里写图片描述 
                             (3)相关匹配法CV_TM_CCORR 
                             这里写图片描述 
                             (4)归一化相关匹配法CV_TM_CCORR_NORMED 
                             这里写图片描述 
                             (5)系数匹配法CV_TM_CCOEFF 
                              这里写图片描述,其中 这里写图片描述
                             (6)化相关系数匹配法CV_TMCCOEFF_NORMED 
                             这里写图片描述
                             opencv中图像的x,y 坐标以及 height, width,rows,cols 他们的关系经常混淆。
                             rows 其实就是行，一行一行也就是y 啦。height高度也就是y啦。
                             cols  也就是列，一列一列也就是x啦。width宽度也就是x啦。   
             
             * 
             * 
             */
            Mat result = new Mat();
            CvInvoke.MatchTemplate(src, tempate, result, TemplateMatchingType.Sqdiff);
            this.pictureBox1.Image = result.Bitmap;
            result.Save(@"D:\aaaaaaaaaaaaaa.jpg");
        }
    }
}
