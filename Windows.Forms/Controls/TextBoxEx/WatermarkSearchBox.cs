using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using Windows.Forms.Controls.Enums;

namespace Windows.Forms.Controls.TextBoxEx
{
    public class WatermarkSearchBox : Control
    {
        private WatermarkTextBox _textBox;
        private Image _image;
        private Color _borderColor = Color.FromArgb(38, 128, 160);
        private Color _dropDownButtonBackColor = Color.FromArgb(166, 222, 255);
        private ControlState _state;
        private ContextMenuStrip _dropDownMenu;

        private static readonly int DefaultMarginWidth = 24;
        public static readonly object EventDropDownButtonClick = new object();

        public WatermarkSearchBox()
            : base()
        {
            SetStyle(
                ControlStyles.UserPaint |
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.SupportsTransparentBackColor |
                ControlStyles.ResizeRedraw, true);
            Init();
        }

        public event EventHandler DropDownButtonClick
        {
            add { Events.AddHandler(EventDropDownButtonClick, value); }
            remove { Events.RemoveHandler(EventDropDownButtonClick, value); }
        }

        [DefaultValue("")]
        public string EmptyTextTip
        {
            get { return _textBox.EmptyTextTip; }
            set
            {
                _textBox.EmptyTextTip = value;
            }
        }

        [DefaultValue(typeof(Color), "DarkGray")]
        public Color EmptyTextTipColor
        {
            get { return _textBox.EmptyTextTipColor; }
            set
            {
                _textBox.EmptyTextTipColor = value;
            }
        }

        [DefaultValue(typeof(Image), "")]
        public Image Image
        {
            get { return _image; }
            set
            {
                _image = value;
                base.Invalidate();
            }
        }

        [DefaultValue(typeof(Color), "38, 128, 160")]
        public Color BorderColor
        {
            get { return _borderColor; }
            set
            {
                _borderColor = value;
                base.Invalidate();
            }
        }

        [DefaultValue(typeof(Color), "166, 222, 255")]
        public Color DropDownButtonBackColor
        {
            get { return _dropDownButtonBackColor; }
            set
            {
                _dropDownButtonBackColor = value;
                base.Invalidate(ButtonRect);
            }
        }

        [DefaultValue(typeof(MenuStrip), "null")]
        public ContextMenuStrip DropDownMenu
        {
            get { return _dropDownMenu; }
            set { _dropDownMenu = value; }
        }

        protected override Size DefaultSize
        {
            get { return new Size(200, 24); }
        }

        private Rectangle ButtonRect
        {
            get
            {
                return new Rectangle(
                        Width - (DefaultMarginWidth - 8) - 6,
                        (Height - (DefaultMarginWidth - 8)) / 2,
                        DefaultMarginWidth - 8,
                        DefaultMarginWidth - 8);
            }
        }

        private ControlState State
        {
            set
            {
                if (_state != value)
                {
                    _state = value;
                    base.Invalidate(ButtonRect);
                }
            }
        }

        private void Init()
        {
            _textBox = new WatermarkTextBox();
            _textBox.BorderStyle = BorderStyle.None;
            _textBox.Location = new Point(DefaultMarginWidth + 2, 5);
            _textBox.Width = Width - DefaultMarginWidth * 2 - 4;
            base.Controls.Add(_textBox);
            base.BackColor = Color.Transparent;
        }

        private void SetTextBoxBounds()
        {
            _textBox.Location = new Point(
                DefaultMarginWidth + 2, (Height - _textBox.Height) / 2);
            _textBox.Width = Width - DefaultMarginWidth * 2 - 4;
        }

        protected virtual void OnDropDownButtonClick(EventArgs e)
        {
            if (DropDownMenu != null)
            {
                DropDownMenu.Show(this, new Point(DefaultMarginWidth, Height + 1));
            }
            EventHandler handler = Events[EventDropDownButtonClick] as EventHandler;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        protected override void OnFontChanged(EventArgs e)
        {
            base.OnFontChanged(e);
            SetTextBoxBounds();
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            SetTextBoxBounds();
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (ButtonRect.Contains(e.Location))
            {
                if (_state != ControlState.Down)
                {
                    State = ControlState.Highlight;
                }
            }
            else
            {
                State = ControlState.Normal;
            }
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            if (e.Button == MouseButtons.Left)
            {
                State = ControlState.Down;
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            if (e.Button == MouseButtons.Left)
            {
                if (ButtonRect.Contains(e.Location))
                {
                    State = ControlState.Highlight;
                }
                else
                {
                    State = ControlState.Normal;
                }

                if (e.Clicks == 1)
                {
                    OnDropDownButtonClick(EventArgs.Empty);
                }
            }
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            State = ControlState.Normal;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            if (_image != null)
            {
                e.Graphics.InterpolationMode = InterpolationMode.HighQualityBilinear;
                int imageWidth = DefaultMarginWidth - 8;
                Rectangle imageRect = new Rectangle(
                    6, (Height - imageWidth) / 2, imageWidth, imageWidth);
                e.Graphics.DrawImage(
                    _image,
                    imageRect,
                    new Rectangle(Point.Empty, _image.Size),
                    GraphicsUnit.Pixel);
            }
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            if (disposing)
            {
                if (_textBox != null && _textBox.IsDisposed)
                {
                    _textBox.Dispose();
                    _textBox = null;
                }
            }
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            base.OnPaintBackground(e);
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            using (GraphicsPath path = new GraphicsPath())
            {
                path.AddArc(0, 0, Height - 1, Height - 1, 90, 180);
                path.AddArc(Width - Height, 0, Height - 1, Height - 1, 270, 180);
                path.CloseFigure();

                e.Graphics.FillPath(Brushes.White, path);
                using (Pen pen = new Pen(_borderColor))
                {
                    e.Graphics.DrawPath(pen, path);
                }
            }

            RenderButton(e.Graphics);
        }

        internal void RenderArrowInternal(
            Graphics g,
            Rectangle dropDownRect,
            ArrowDirection direction,
            Brush brush)
        {
            Point point = new Point(
                dropDownRect.Left + (dropDownRect.Width / 2),
                dropDownRect.Top + (dropDownRect.Height / 2));
            Point[] points = null;
            switch (direction)
            {
                case ArrowDirection.Left:
                    points = new Point[] { 
                        new Point(point.X + 2, point.Y - 3), 
                        new Point(point.X + 2, point.Y + 3), 
                        new Point(point.X - 1, point.Y) };
                    break;

                case ArrowDirection.Up:
                    points = new Point[] { 
                        new Point(point.X - 3, point.Y + 2), 
                        new Point(point.X + 3, point.Y + 2), 
                        new Point(point.X, point.Y - 2) };
                    break;

                case ArrowDirection.Right:
                    points = new Point[] {
                        new Point(point.X - 2, point.Y - 3), 
                        new Point(point.X - 2, point.Y + 3), 
                        new Point(point.X + 1, point.Y) };
                    break;

                default:
                    points = new Point[] {
                        new Point(point.X - 3, point.Y - 1), 
                        new Point(point.X + 4, point.Y - 1), 
                        new Point(point.X, point.Y + 3) };
                    break;
            }
            g.FillPolygon(brush, points);
        }

        internal void RenderButton(Graphics g)
        {
            Color baseColor = _dropDownButtonBackColor;
            Color borderColor = _borderColor;
            Color innerBorderColor = Color.FromArgb(200, 250, 250, 250);

            switch (_state)
            {
                case ControlState.Highlight:
                    baseColor = GetColor(_dropDownButtonBackColor, 0, 35, 24, 9);
                    break;
                case ControlState.Down:
                    baseColor = GetColor(_dropDownButtonBackColor, 0, -35, -24, -9);
                    break;
            }

            Rectangle buttonRect = ButtonRect;
            if (_state == ControlState.Normal)
            {
                g.FillRectangle(Brushes.White, buttonRect);
            }
            else
            {
                RenderBackgroundInternal(
                    g,
                    buttonRect,
                    baseColor,
                    borderColor,
                    innerBorderColor,
                    0.45f,
                    true,
                    LinearGradientMode.Vertical);
            }
            using (SolidBrush brush = new SolidBrush(borderColor))
            {
                RenderArrowInternal(
                    g,
                    buttonRect,
                    ArrowDirection.Down,
                    brush);
            }
        }

        internal void RenderBackgroundInternal(
          Graphics g,
          Rectangle rect,
          Color baseColor,
          Color borderColor,
          Color innerBorderColor,
          float basePosition,
          bool drawBorder,
          LinearGradientMode mode)
        {
            if (drawBorder)
            {
                rect.Width--;
                rect.Height--;
            }
            using (LinearGradientBrush brush = new LinearGradientBrush(
               rect, Color.Transparent, Color.Transparent, mode))
            {
                Color[] colors = new Color[4];
                colors[0] = GetColor(baseColor, 0, 35, 24, 9);
                colors[1] = GetColor(baseColor, 0, 13, 8, 3);
                colors[2] = baseColor;
                colors[3] = GetColor(baseColor, 0, 68, 69, 54);

                ColorBlend blend = new ColorBlend();
                blend.Positions = new float[] { 0.0f, basePosition, basePosition + 0.05f, 1.0f };
                blend.Colors = colors;
                brush.InterpolationColors = blend;
                g.FillRectangle(brush, rect);
            }
            if (baseColor.A > 80)
            {
                Rectangle rectTop = rect;
                if (mode == LinearGradientMode.Vertical)
                {
                    rectTop.Height = (int)(rectTop.Height * basePosition);
                }
                else
                {
                    rectTop.Width = (int)(rect.Width * basePosition);
                }
                using (SolidBrush brushAlpha =
                    new SolidBrush(Color.FromArgb(80, 255, 255, 255)))
                {
                    g.FillRectangle(brushAlpha, rectTop);
                }
            }

            if (drawBorder)
            {
                using (Pen pen = new Pen(borderColor))
                {
                    g.DrawRectangle(pen, rect);
                }

                rect.Inflate(-1, -1);
                using (Pen pen = new Pen(innerBorderColor))
                {
                    g.DrawRectangle(pen, rect);
                }
            }
        }

        private Color GetColor(Color colorBase, int a, int r, int g, int b)
        {
            int a0 = colorBase.A;
            int r0 = colorBase.R;
            int g0 = colorBase.G;
            int b0 = colorBase.B;

            if (a + a0 > 255) { a = 255; } else { a = a + a0; }
            if (r + r0 > 255) { r = 255; } else { r = r + r0; }
            if (g + g0 > 255) { g = 255; } else { g = g + g0; }
            if (b + b0 > 255) { b = 255; } else { b = b + b0; }

            return Color.FromArgb(a, r, g, b);
        }
    }
}
