using System;
using System.Collections.Generic;

using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Reflection;

namespace Windows.Forms.Controls.TextBoxEx
{
    internal class DrawHelper
    {
        public static GraphicsPath DrawRoundRect(int x, int y, int width, int height, int radius)
        {
            GraphicsPath gp = new GraphicsPath();
            gp.AddArc(x, y, radius, radius, 180, 90);
            gp.AddArc(width - radius, y, radius, radius, 270, 90);
            gp.AddArc(width - radius, height - radius, radius, radius, 0, 90);
            gp.AddArc(x, height - radius, radius, radius, 90, 90);
            gp.CloseAllFigures();
            return gp;
        }

        /// <summary>
        /// 绘制圆角矩形
        /// </summary>
        /// <param name="rect">矩形</param>
        /// <param name="radius">弯曲程度(0-10)，越大越弯曲</param>
        /// <returns></returns>
        public static GraphicsPath DrawRoundRect(Rectangle rect, int radius)
        {
            int x = rect.X;
            int y = rect.Y;
            int width = rect.Width;
            int height = rect.Height;
            return DrawRoundRect(x, y, width - 2, height - 1, radius);
        }

        /// <summary>
        /// 得到两种颜色的过渡色（1代表开始色，100表示结束色）
        /// </summary>
        /// <param name="c">开始色</param>
        /// <param name="c2">结束色</param>
        /// <param name="value">需要获得的度</param>
        /// <returns></returns>
        public static Color GetIntermediateColor(Color c, Color c2, int value)
        {
            float pc = value * 1.0F / 100;

            int ca = c.A, cr = c.R, cg = c.G, cb = c.B;
            int c2a = c2.A, c2r = c2.R, c2g = c2.G, c2b = c2.B;

            int a = (int)Math.Abs(ca + (ca - c2a) * pc);
            int r = (int)Math.Abs(cr - ((cr - c2r) * pc));
            int g = (int)Math.Abs(cg - ((cg - c2g) * pc));
            int b = (int)Math.Abs(cb - ((cb - c2b) * pc));

            if (a > 255) { a = 255; }
            if (r > 255) { r = 255; }
            if (g > 255) { g = 255; }
            if (b > 255) { b = 255; }

            return (Color.FromArgb(a, r, g, b));
        }

        public static StringFormat StringFormatAlignment(ContentAlignment textalign)
        {
            StringFormat sf = new StringFormat();
            switch (textalign)
            {
                case ContentAlignment.TopLeft:
                case ContentAlignment.TopCenter:
                case ContentAlignment.TopRight:
                    sf.LineAlignment = StringAlignment.Near;
                    break;
                case ContentAlignment.MiddleLeft:
                case ContentAlignment.MiddleCenter:
                case ContentAlignment.MiddleRight:
                    sf.LineAlignment = StringAlignment.Center;
                    break;
                case ContentAlignment.BottomLeft:
                case ContentAlignment.BottomCenter:
                case ContentAlignment.BottomRight:
                    sf.LineAlignment = StringAlignment.Far;
                    break;
            }
            return sf;
        }

        /// <summary>
        /// 绘图对像
        /// </summary>
        /// <param name="g">绘图对像</param>
        /// <param name="img">图片</param>
        /// <param name="r">绘置的图片大小、坐标</param>
        /// <param name="lr">绘置的图片边界</param>
        /// <param name="index">当前状态</param>
        /// <param name="Totalindex">状态总数</param>
        public static void DrawRect(Graphics g, Bitmap img, Rectangle r, Rectangle lr, int index, int Totalindex)
        {
            if (img == null) return;
            Rectangle r1, r2;
            int x = (index - 1) * img.Width / Totalindex;
            int y = 0;
            int x1 = r.Left;
            int y1 = r.Top;

            if (r.Height > img.Height && r.Width <= img.Width / Totalindex)
            {
                r1 = new Rectangle(x, y, img.Width / Totalindex, lr.Top);
                r2 = new Rectangle(x1, y1, r.Width, lr.Top);
                g.DrawImage(img, r2, r1, GraphicsUnit.Pixel);

                r1 = new Rectangle(x, y + lr.Top, img.Width / Totalindex, img.Height - lr.Top - lr.Bottom);
                r2 = new Rectangle(x1, y1 + lr.Top, r.Width, r.Height - lr.Top - lr.Bottom);
                if ((lr.Top + lr.Bottom) == 0) r1.Height = r1.Height - 1;
                g.DrawImage(img, r2, r1, GraphicsUnit.Pixel);

                r1 = new Rectangle(x, y + img.Height - lr.Bottom, img.Width / Totalindex, lr.Bottom);
                r2 = new Rectangle(x1, y1 + r.Height - lr.Bottom, r.Width, lr.Bottom);
                g.DrawImage(img, r2, r1, GraphicsUnit.Pixel);
            }
            else
                if (r.Height <= img.Height && r.Width > img.Width / Totalindex)
                {
                    r1 = new Rectangle(x, y, lr.Left, img.Height);
                    r2 = new Rectangle(x1, y1, lr.Left, r.Height);
                    g.DrawImage(img, r2, r1, GraphicsUnit.Pixel);
                    r1 = new Rectangle(x + lr.Left, y, img.Width / Totalindex - lr.Left - lr.Right, img.Height);
                    r2 = new Rectangle(x1 + lr.Left, y1, r.Width - lr.Left - lr.Right, r.Height);
                    g.DrawImage(img, r2, r1, GraphicsUnit.Pixel);
                    r1 = new Rectangle(x + img.Width / Totalindex - lr.Right, y, lr.Right, img.Height);
                    r2 = new Rectangle(x1 + r.Width - lr.Right, y1, lr.Right, r.Height);
                    g.DrawImage(img, r2, r1, GraphicsUnit.Pixel);
                }
                else
                    if (r.Height <= img.Height && r.Width <= img.Width / Totalindex) { r1 = new Rectangle((index - 1) * img.Width / Totalindex, 0, img.Width / Totalindex, img.Height); g.DrawImage(img, new Rectangle(x1, y1, r.Width, r.Height), r1, GraphicsUnit.Pixel); }
                    else if (r.Height > img.Height && r.Width > img.Width / Totalindex)
                    {
                        //top-left
                        r1 = new Rectangle(x, y, lr.Left, lr.Top);
                        r2 = new Rectangle(x1, y1, lr.Left, lr.Top);
                        g.DrawImage(img, r2, r1, GraphicsUnit.Pixel);

                        //top-bottom
                        r1 = new Rectangle(x, y + img.Height - lr.Bottom, lr.Left, lr.Bottom);
                        r2 = new Rectangle(x1, y1 + r.Height - lr.Bottom, lr.Left, lr.Bottom);
                        g.DrawImage(img, r2, r1, GraphicsUnit.Pixel);

                        //left
                        r1 = new Rectangle(x, y + lr.Top, lr.Left, img.Height - lr.Top - lr.Bottom);
                        r2 = new Rectangle(x1, y1 + lr.Top, lr.Left, r.Height - lr.Top - lr.Bottom);
                        g.DrawImage(img, r2, r1, GraphicsUnit.Pixel);

                        //top
                        r1 = new Rectangle(x + lr.Left, y,
                            img.Width / Totalindex - lr.Left - lr.Right, lr.Top);
                        r2 = new Rectangle(x1 + lr.Left, y1,
                            r.Width - lr.Left - lr.Right, lr.Top);
                        g.DrawImage(img, r2, r1, GraphicsUnit.Pixel);

                        //right-top
                        r1 = new Rectangle(x + img.Width / Totalindex - lr.Right, y, lr.Right, lr.Top);
                        r2 = new Rectangle(x1 + r.Width - lr.Right, y1, lr.Right, lr.Top);
                        g.DrawImage(img, r2, r1, GraphicsUnit.Pixel);

                        //Right
                        r1 = new Rectangle(x + img.Width / Totalindex - lr.Right, y + lr.Top,
                            lr.Right, img.Height - lr.Top - lr.Bottom);
                        r2 = new Rectangle(x1 + r.Width - lr.Right, y1 + lr.Top,
                            lr.Right, r.Height - lr.Top - lr.Bottom);
                        g.DrawImage(img, r2, r1, GraphicsUnit.Pixel);

                        //right-bottom
                        r1 = new Rectangle(x + img.Width / Totalindex - lr.Right, y + img.Height - lr.Bottom,
                            lr.Right, lr.Bottom);
                        r2 = new Rectangle(x1 + r.Width - lr.Right, y1 + r.Height - lr.Bottom,
                            lr.Right, lr.Bottom);
                        g.DrawImage(img, r2, r1, GraphicsUnit.Pixel);

                        //bottom
                        r1 = new Rectangle(x + lr.Left, y + img.Height - lr.Bottom,
                            img.Width / Totalindex - lr.Left - lr.Right, lr.Bottom);
                        r2 = new Rectangle(x1 + lr.Left, y1 + r.Height - lr.Bottom,
                            r.Width - lr.Left - lr.Right, lr.Bottom);
                        g.DrawImage(img, r2, r1, GraphicsUnit.Pixel);

                        //Center
                        r1 = new Rectangle(x + lr.Left, y + lr.Top,
                            img.Width / Totalindex - lr.Left - lr.Right, img.Height - lr.Top - lr.Bottom);
                        r2 = new Rectangle(x1 + lr.Left, y1 + lr.Top,
                            r.Width - lr.Left - lr.Right, r.Height - lr.Top - lr.Bottom);
                        g.DrawImage(img, r2, r1, GraphicsUnit.Pixel);
                    }
        }

        /// <summary>
        /// 绘图对像
        /// </summary>
        /// <param name="g"> 绘图对像</param>
        /// <param name="obj">图片对像</param>
        /// <param name="r">绘置的图片大小、坐标</param>
        /// <param name="index">当前状态</param>
        /// <param name="Totalindex">状态总数</param>
        public static void DrawRect(Graphics g, Bitmap img, Rectangle r, int index, int Totalindex)
        {
            if (img == null) return;
            int width = img.Width / Totalindex;
            int height = img.Height;
            Rectangle r1, r2;
            int x = (index - 1) * width;
            int y = 0;
            r1 = new Rectangle(x, y, width, height);
            r2 = new Rectangle(r.Left, r.Top, r.Width, r.Height);
            g.DrawImage(img, r2, r1, GraphicsUnit.Pixel);
        }

        /// <summary>
        /// 得到要绘置的图片对像
        /// </summary>
        /// <param name="str">图像在程序集中的地址</param>
        /// <returns></returns>
        public static Bitmap GetResBitmap(string str)
        {
            Stream sm;
            sm = FindStream(str);
            if (sm == null) return null;
            return new Bitmap(sm);
        }

        /// <summary>
        /// 得到图程序集中的图片对像
        /// </summary>
        /// <param name="str">图像在程序集中的地址</param>
        /// <returns></returns>
        private static Stream FindStream(string str)
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            string[] resNames = assembly.GetManifestResourceNames();
            foreach (string s in resNames)
            {
                if (s == str)
                {
                    return assembly.GetManifestResourceStream(s);
                }
            }
            return null;
        }

    }
}
