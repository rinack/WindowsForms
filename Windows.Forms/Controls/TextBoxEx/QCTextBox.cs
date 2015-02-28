using System;
using System.Collections.Generic;

using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.ComponentModel;
using Windows.Forms.Controls.Enums;
using Windows.Forms.Controls.WinAPI;
using Windows.Forms.Controls.Common;
using Windows.Forms.Controls.Methods;

namespace ControlsEx.Controls.TextBoxEx
{
    public class QCTextBox : TextBox
    {
        #region Field

        private ControlState _state = ControlState.Normal;
        private Font _defaultFont = new Font("微软雅黑", 9);

        //当Text属性为空时编辑框内出现的提示文本
        private string _emptyTextTip;
        private Color _emptyTextTipColor = Color.DarkGray;

        #endregion

        #region Constructor

        public QCTextBox()
        {
            SetStyles();
            this.Font = _defaultFont;
            //this.AutoSize = true;
            this.BorderStyle = BorderStyle.None;
        }

        #endregion

        #region Properites

        [Description("当Text属性为空时编辑框内出现的提示文本")]
        public String EmptyTextTip
        {
            get { return _emptyTextTip; }
            set
            {
                if (_emptyTextTip != value)
                {
                    _emptyTextTip = value;
                    base.Invalidate();
                }
            }
        }

        [Description("获取或设置EmptyTextTip的颜色")]
        public Color EmptyTextTipColor
        {
            get { return _emptyTextTipColor; }
            set
            {
                if (_emptyTextTipColor != value)
                {
                    _emptyTextTipColor = value;
                    base.Invalidate();
                }
            }
        }

        private int _radius = 12;
        [Description("获取或设置圆角弧度")]
        public int Radius
        {
            get { return _radius; }
            set
            {
                _radius = value;
                this.Invalidate();
            }
        }

        [Description("获取或设置是否可自定义改变大小")]
        public bool CustomAutoSize
        {
            get { return this.AutoSize; }
            set { this.AutoSize = value; }
        }

        private Size inflateSize = new Size(0,0);
        [Description("将此 Rectangle 矩形放大指定量")]
        public Size InflateSize
        {
            get { return inflateSize; }
            set { inflateSize = value; }
        }

        #endregion

        #region Override

        protected override void OnMouseEnter(EventArgs e)
        {
            _state = ControlState.Highlight;
            base.OnMouseEnter(e);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            if (_state == ControlState.Highlight && Focused)
            {
                _state = ControlState.Focus;
            }
            else if (_state == ControlState.Focus)
            {
                _state = ControlState.Focus;
            }
            else
            {
                _state = ControlState.Normal;
            }
            base.OnMouseLeave(e);
        }

        protected override void OnMouseDown(MouseEventArgs mevent)
        {
            if (mevent.Button == MouseButtons.Left)
            {
                _state = ControlState.Highlight;
            }
            base.OnMouseDown(mevent);
        }

        protected override void OnMouseUp(MouseEventArgs mevent)
        {
            if (mevent.Button == MouseButtons.Left)
            {
                if (ClientRectangle.Contains(mevent.Location))
                {
                    _state = ControlState.Highlight;
                }
                else
                {
                    _state = ControlState.Focus;
                }
            }
            base.OnMouseUp(mevent);
        }

        protected override void OnLostFocus(EventArgs e)
        {
            _state = ControlState.Normal;
            base.OnLostFocus(e);
        }

        protected override void OnEnabledChanged(EventArgs e)
        {
            if (Enabled)
            {
                _state = ControlState.Normal;
            }
            else
            {
                _state = ControlState.Disabled;
            }
            base.OnEnabledChanged(e);
        }

        protected override void WndProc(ref Message m)
        {//TextBox是由系统进程绘制，重载OnPaint方法将不起作用

            base.WndProc(ref m);
            if (m.Msg == Win32.WM_PAINT || m.Msg == Win32.WM_CTLCOLOREDIT)
            {
                WmPaint(ref m);
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_defaultFont != null)
                {
                    _defaultFont.Dispose();
                }
            }

            _defaultFont = null;
            base.Dispose(disposing);
        }

        #endregion

        #region Private

        private void SetStyles()
        {
            // TextBox由系统绘制，不能设置 ControlStyles.UserPaint样式
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            SetStyle(ControlStyles.ResizeRedraw, true);
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            UpdateStyles();
        }

        private void WmPaint(ref Message m)
        {
            Graphics g = Graphics.FromHwnd(base.Handle);

            //g.SmoothingMode = SmoothingMode.AntiAlias;
            //去掉 TextBox 四个角
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            SetWindowRegion(this.Width, this.Height);

            if (!Enabled)
            {
                _state = ControlState.Disabled;
            }

            switch (_state)
            {
                case ControlState.Normal:
                    DrawNormalTextBox(g);
                    break;
                case ControlState.Highlight:
                    DrawHighLightTextBox(g);
                    break;
                case ControlState.Focus:
                    DrawFocusTextBox(g);
                    break;
                case ControlState.Disabled:
                    DrawDisabledTextBox(g);
                    break;
                default:
                    break;
            }

            if (Text.Length == 0 && !string.IsNullOrEmpty(EmptyTextTip) && !Focused)
            {
                TextRenderer.DrawText(g, EmptyTextTip, Font, ClientRectangle, EmptyTextTipColor, GetTextFormatFlags(TextAlign, RightToLeft == RightToLeft.Yes));
            }
        }

        private void DrawNormalTextBox(Graphics g)
        {
            using (Pen borderPen = new Pen(Color.LightGray))
            {
                //g.DrawRectangle(borderPen, new Rectangle(ClientRectangle.X, ClientRectangle.Y, ClientRectangle.Width - 1, ClientRectangle.Height - 1));
                g.DrawPath(borderPen, DrawHelper.DrawRoundRect(ClientRectangle.X, ClientRectangle.Y, ClientRectangle.Width - 1, ClientRectangle.Height - 1, _radius));
            }
        }

        private void DrawHighLightTextBox(Graphics g)
        {
            using (Pen highLightPen = new Pen(ColorTable.QQHighLightColor))
            {
                //Rectangle drawRect = new Rectangle(ClientRectangle.X, ClientRectangle.Y, ClientRectangle.Width - 1, ClientRectangle.Height - 1);
                //g.DrawRectangle(highLightPen, drawRect);

                g.DrawPath(highLightPen, DrawHelper.DrawRoundRect(ClientRectangle.X, ClientRectangle.Y, ClientRectangle.Width - 1, ClientRectangle.Height - 1, _radius));

                //InnerRect
                //drawRect.Inflate(-1, -1);
                //highLightPen.Color = ColorTable.QQHighLightInnerColor;
                //g.DrawRectangle(highLightPen, drawRect);

                g.DrawPath(new Pen(ColorTable.QQHighLightInnerColor), DrawHelper.DrawRoundRect(ClientRectangle.X, ClientRectangle.Y, ClientRectangle.Width - 1, ClientRectangle.Height - 1, _radius));
            }
        }

        private void DrawFocusTextBox(Graphics g)
        {
            using (Pen focusedBorderPen = new Pen(ColorTable.QQHighLightInnerColor))
            {
                //g.DrawRectangle(focusedBorderPen,new Rectangle(ClientRectangle.X,ClientRectangle.Y,ClientRectangle.Width - 1, ClientRectangle.Height - 1));
                g.DrawPath(focusedBorderPen, DrawHelper.DrawRoundRect(ClientRectangle.X, ClientRectangle.Y, ClientRectangle.Width - 1, ClientRectangle.Height - 1, _radius));
            }
        }

        private void DrawDownTextBox(Graphics g)
        {
            using (Pen focusedBorderPen = new Pen(ColorTable.QQHighLightInnerColor))
            {
                //g.DrawRectangle(focusedBorderPen,new Rectangle(ClientRectangle.X,ClientRectangle.Y,ClientRectangle.Width - 1, ClientRectangle.Height - 1));
                g.DrawPath(focusedBorderPen, DrawHelper.DrawRoundRect(ClientRectangle.X, ClientRectangle.Y, ClientRectangle.Width - 1, ClientRectangle.Height - 1, _radius));
            }
        }

        private void DrawDisabledTextBox(Graphics g)
        {
            using (Pen disabledPen = new Pen(SystemColors.ControlDark))
            {
                //g.DrawRectangle(disabledPen,new Rectangle( ClientRectangle.X,ClientRectangle.Y, ClientRectangle.Width - 1,ClientRectangle.Height - 1));
                g.DrawPath(disabledPen, DrawHelper.DrawRoundRect(ClientRectangle.X, ClientRectangle.Y, ClientRectangle.Width - 1, ClientRectangle.Height - 1, _radius));
            }
        }

        private static TextFormatFlags GetTextFormatFlags(HorizontalAlignment alignment, bool rightToleft)
        {
            TextFormatFlags flags = TextFormatFlags.WordBreak |
                TextFormatFlags.SingleLine;
            if (rightToleft)
            {
                flags |= TextFormatFlags.RightToLeft | TextFormatFlags.Right;
            }

            switch (alignment)
            {
                case HorizontalAlignment.Center:
                    flags |= TextFormatFlags.VerticalCenter | TextFormatFlags.HorizontalCenter;
                    break;
                case HorizontalAlignment.Left:
                    flags |= TextFormatFlags.VerticalCenter | TextFormatFlags.Left;
                    break;
                case HorizontalAlignment.Right:
                    flags |= TextFormatFlags.VerticalCenter | TextFormatFlags.Right;
                    break;
            }
            return flags;
        }

        #endregion

        public void SetWindowRegion(int width, int height)
        {
            System.Drawing.Drawing2D.GraphicsPath FormPath = new System.Drawing.Drawing2D.GraphicsPath();
            Rectangle rect = new Rectangle(0, 0, width, height);
            //FormPath = GetRoundedRectPath(rect, _radius);
            rect.Inflate(InflateSize);
            FormPath = RenderHelper.CreateRoundPath(rect, Radius);
            this.Region = new Region(FormPath);
        }

        private GraphicsPath GetRoundedRectPath(Rectangle rect, int radius)
        {
            int diameter = radius;
            Rectangle arcRect = new Rectangle(rect.Location, new Size(diameter, diameter));
            GraphicsPath path = new GraphicsPath();
            //   左上角      
            path.AddArc(arcRect, 180, 90);
            //   右上角      
            arcRect.X = rect.Right - diameter;
            path.AddArc(arcRect, 270, 90);
            //   右下角      
            arcRect.Y = rect.Bottom - diameter;
            path.AddArc(arcRect, 0, 90);
            //   左下角      
            arcRect.X = rect.Left;
            path.AddArc(arcRect, 90, 90);
            path.CloseFigure();
            return path;
        }
    }
}
