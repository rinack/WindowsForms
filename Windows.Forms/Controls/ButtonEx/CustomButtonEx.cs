using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using Windows.Forms.Controls.Methods;

namespace Windows.Forms.Controls.ButtonEx
{
    public class CustomButtonEx : Button
    {

        public CustomButtonEx() 
        {
            SetStyles();
        }

        private void SetStyles()
        {
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            SetStyle(ControlStyles.ResizeRedraw, true);
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            UpdateStyles();
        }

        private int radius = 8;
        [Description("按钮圆角的半径")]
        [DefaultValue(typeof(int), "8")]
        public int Radius
        {
            get { return radius; }
            set { radius = value; }
        }

        private RoundStyle rounStyle;
        [Description("按钮圆角的方向")]
        [DefaultValue(typeof(int), "0")]
        public RoundStyle RounStyle
        {
            get { return rounStyle; }
            set { rounStyle = value; }
        }

        private Size inflateSize = new Size(0, 0);
        [Description("将此 Rectangle 矩形放大指定量")]
        public Size InflateSize
        {
            get { return inflateSize; }
            set { inflateSize = value; }
        }

        [Flags]
        public enum RoundStyle
        {
            None = 0,
            TopLeft = 1,
            TopRight = 2,
            BottomLeft = 4,
            BottomRight = 8,
            All = TopLeft | TopRight | BottomLeft | BottomRight
        }

        protected override void OnPaint(PaintEventArgs pevent)
        {
            base.OnPaint(pevent);
            Graphics g = pevent.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.InterpolationMode = InterpolationMode.HighQualityBilinear;
            SetWindowRegion(this.Width, this.Height);
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            //SetWindowRegion();
        }

        public void SetWindowRegion()
        {
            System.Drawing.Drawing2D.GraphicsPath FormPath;
            FormPath = new System.Drawing.Drawing2D.GraphicsPath();
            Rectangle rect = new Rectangle(0, 0, this.Width, this.Height);
            rect.Inflate(InflateSize);
            FormPath = RenderHelper.CreateRoundPath(rect, Radius);
            this.Region = new Region(FormPath);
        }

        public void SetWindowRegion(int width, int height)
        {
            System.Drawing.Drawing2D.GraphicsPath FormPath = new System.Drawing.Drawing2D.GraphicsPath();
            Rectangle rect = new Rectangle(0, 0, width, height);
            //FormPath = GetRoundedRectPath(rect, radius);
            FormPath = RenderHelper.CreateRoundPath(rect, Radius);
            rect.Inflate(InflateSize);
            this.Region = new Region(FormPath);
        }
    }
}
