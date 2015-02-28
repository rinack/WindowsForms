using System;
using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using Windows.Forms.Controls.Methods;

namespace ControlsEx.Controls.ButtonEx
{
    public class KeyBoardButton:Button
    {
        private Color defaultStartColor = Color.FromArgb(214, 230, 251);
        [Category("Appearance")]
        [DefaultValue(typeof(Color), "214, 230, 251")]
        [Description("默认颜色起始色,配合CurrentStartColor属性使用")]
        public Color DefaultStartColor
        {
            get
            {
                return this.defaultStartColor;
            }
            set
            {
                this.defaultStartColor = value;
            }
        }

        private Color defautEndColor= Color.FromArgb(154, 187, 234);
        [Category("Appearance")]
        [DefaultValue(typeof(Color), "154, 187, 234")]
        [Description("默认颜色结束色,配合CurrentEndColor属性使用")]
        public Color DefautEndColor
        {
            get
            {
                return this.defautEndColor;
            }
            set
            {
                this.defautEndColor = value;
            }
        }

        private Color defaultBorderColor = Color.FromArgb(59, 97, 156);
        [Category("Appearance")]
        [DefaultValue(typeof(Color), "59, 97, 156")]
        [Description("默认颜色边框色,配合CurrentBorderColor属性使用")]
        public Color DefaultBorderColor
        {
            get
            {
                return this.defaultBorderColor;
            }
            set
            {
                this.defaultBorderColor = value;
            }
        }

        private Color mouseEnterStartColor = Color.FromArgb(255, 240, 197);
        [Category("Appearance")]
        [DefaultValue(typeof(Color), "255, 240, 197")]
        [Description("鼠标进入可见状态时的起始颜色")]
        public Color MouseEnterStartColor
        {
            get
            {
                return this.mouseEnterStartColor;
            }
            set
            {
                this.mouseEnterStartColor = value;
            }
        }

        private Color mouseEnterEndColor= Color.FromArgb(255, 213, 152);
        [Category("Appearance")]
        [DefaultValue(typeof(Color), "255, 213, 152")]
        [Description("鼠标进入可见状态时的结束颜色")]
        public Color MouseEnterEndColor
        {
            get
            {
                return this.mouseEnterEndColor;
            }
            set
            {
                this.mouseEnterEndColor = value;
            }
        }

        private Color mouseEnterBorderColor = Color.FromArgb(0, 0, 128);
        [Category("Appearance")]
        [DefaultValue(typeof(Color), "0, 0, 128")]
        [Description("鼠标进入可见状态时的边框颜色")]
        public Color MouseEnterBorderColor
        {
            get
            {
                return this.mouseEnterBorderColor;
            }
            set
            {
                this.mouseEnterBorderColor = value;
            }
        }

        private Color mouseDownStartColor = Color.FromArgb(254, 151, 84);
        [Category("Appearance")]
        [DefaultValue(typeof(Color), "254, 151, 84")]
        [Description("鼠标点击时触发的起始颜色")]
        public Color MouseDownStartColor
        {
            get
            {
                return this.mouseDownStartColor;
            }
            set
            {
                this.mouseDownStartColor = value;
            }
        }

        private Color mouseDownEndColor = Color.FromArgb(255, 199, 131);
        [Category("Appearance")]
        [DefaultValue(typeof(Color), "255, 199, 131")]
        [Description("鼠标点击时触发的结束颜色")]
        public Color MouseDownEndColor
        {
            get
            {
                return this.mouseDownEndColor;
            }
            set
            {
                this.mouseDownEndColor = value;
            }
        }

        private Color currentStartColor = Color.FromArgb(214, 230, 251);
        [DefaultValue(typeof(Color), "214, 230, 251")]
        [Description("默认颜色起始色,配合DefaultStartColor属性使用")]
        public Color CurrentStartColor
        {
            get { return currentStartColor; }
            set { currentStartColor = value; }
        }

        private Color currentEndColor = Color.FromArgb(154, 187, 234);
        [DefaultValue(typeof(Color), "154, 187, 234")]
        [Description("默认颜色结束色,配合DefautEndColor属性使用")]
        public Color CurrentEndColor
        {
            get { return currentEndColor; }
            set { currentEndColor = value; }
        }

        private Color currentBorderColor = Color.FromArgb(59, 97, 156);
        [Browsable(true)]
        [DefaultValue(typeof(Color), "59, 97, 156")]
        [Description("默认颜色边框色,配合DefaultBorderColor属性使用")]
        public Color CurrentBorderColor
        {
            get
            {
                return this.currentBorderColor;
            }
            set 
            {
                this.currentBorderColor = value;
            }
        }

        private bool antialias = true;
        [Category("Appearance")]
        public bool AntiAlias
        {
            get
            {
                return this.antialias;
            }
            set
            {
                this.antialias = value;
                this.Invalidate();
            }
        }

        private bool isChecked = false;
        [Category("Appearance")]
        public bool IsChecked
        {
            get
            {
                return this.isChecked;
            }
            set
            {
                this.isChecked = value;
                this.Invalidate();

                CheckChangedEventArgs args = new CheckChangedEventArgs(this.isChecked);
                OnCheckChanged(args);
            }
        }

        private bool showFocusRectangle = false;
        [Category("Appearance")]
        public bool ShowFocusRectangle
        {
            get
            {
                return this.showFocusRectangle;
            }
            set
            {
                this.showFocusRectangle = value;
            }
        }

        private short vkCode;
        [Category("Data")]
        public short VKCode
        {
            get
            {
                return this.vkCode;
            }
            set
            {
                this.vkCode = value;
            }
        }

        private static readonly object EventCheckChanged = new object();

        public KeyBoardButton()
            : base() {
            base.Size = new System.Drawing.Size(107, 31);
            base.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));

            base.Paint += new PaintEventHandler(LifeButton_Paint);
        }


        public event EventHandler<CheckChangedEventArgs> CheckChanged {
            add {
                base.Events.AddHandler(EventCheckChanged, value);
            }
            remove {
                base.Events.RemoveHandler(EventCheckChanged, value);
            }
        }

        protected virtual void OnCheckChanged(CheckChangedEventArgs args) {
            EventHandler<CheckChangedEventArgs> handler = base.Events[EventCheckChanged] as EventHandler<CheckChangedEventArgs>;
            if (handler != null) {
                handler(this, args);
            }
        }

        private void LifeButton_Paint(object sender, PaintEventArgs pevent) {
            if (this.ClientSize.Width > 3 && this.ClientSize.Height > 3) {
                Graphics g = pevent.Graphics;

                if (this.antialias) {
                    g.SmoothingMode = SmoothingMode.AntiAlias;
                    g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
                }

                Rectangle rect = new Rectangle(0, 0, this.ClientSize.Width - 1, this.ClientSize.Height - 1);

                Brush fillBrush = this.isChecked ?
                    new LinearGradientBrush(base.ClientRectangle, this.mouseDownStartColor, this.mouseDownEndColor, LinearGradientMode.Vertical) :
                    new LinearGradientBrush(base.ClientRectangle, this.currentStartColor, this.currentEndColor, LinearGradientMode.Vertical);

                g.FillRectangle(fillBrush, base.ClientRectangle);
                fillBrush.Dispose();

                if (this.BackgroundImage != null) {
                    if (base.Enabled) {
                        g.DrawImage(base.BackgroundImage, rect);
                    } else {
                        Image image = ImageProcessHelper.CreateDisabledImage(base.BackgroundImage);
                        g.DrawImage(image, rect);
                        image.Dispose();
                    }
                }

                using (Pen pen = new Pen(this.isChecked ? this.mouseEnterBorderColor : this.currentBorderColor, 1)) {
                    g.DrawRectangle(pen, rect);

                    if (base.Focused && this.showFocusRectangle) {
                        pen.Color = this.defaultBorderColor;
                        pen.DashStyle = DashStyle.Dot;
                        rect.Inflate(-2, -2);

                        g.DrawRectangle(pen, rect);
                    }
                }

                StringFormat format = new StringFormat();
                SetTextAlign(format);
                format.HotkeyPrefix = System.Drawing.Text.HotkeyPrefix.Show;

                using (Brush brush = new SolidBrush(base.ForeColor)) {
                    if (rect.Width > 4 && rect.Height > 2) {
                        rect.Inflate(-4, -2);
                        g.DrawString(base.Text, base.Font, (this.Enabled ? brush : Brushes.Gray), rect, format);
                    }
                }
            }
        }

        protected override void OnMouseEnter(EventArgs e) {
            this.currentStartColor = this.mouseEnterStartColor;
            this.currentEndColor = this.mouseEnterEndColor;
            this.currentBorderColor = this.mouseEnterBorderColor;

            this.Invalidate();

            base.OnMouseEnter(e);
        }

        protected override void OnMouseLeave(EventArgs e) {
            this.currentStartColor = this.defaultStartColor;
            this.currentEndColor = this.defautEndColor;
            this.currentBorderColor = this.defaultBorderColor;

            this.Invalidate();

            base.OnMouseLeave(e);
        }

        protected override void OnMouseDown(MouseEventArgs mevent) {
            this.currentStartColor = this.mouseDownStartColor;
            this.currentEndColor = this.mouseDownEndColor;

            this.Invalidate();

            base.OnMouseDown(mevent);
        }

        protected override void OnMouseUp(MouseEventArgs mevent) {
            this.currentStartColor = this.mouseEnterStartColor;
            this.currentEndColor = this.mouseEnterEndColor;

            base.OnMouseUp(mevent);
        }

        private void SetTextAlign(StringFormat format) {
            string textAlign = base.TextAlign.ToString("G");

            if (textAlign.IndexOf("Right") >= 0) {
                format.Alignment = StringAlignment.Far;
            } else if (textAlign.IndexOf("Center") >= 0) {
                format.Alignment = StringAlignment.Center;
            } else if (textAlign.IndexOf("Left") >= 0) {
                format.Alignment = StringAlignment.Near;
            }

            if (textAlign.IndexOf("Bottom") >= 0) {
                format.LineAlignment = StringAlignment.Far;
            } else if (textAlign.IndexOf("Middle") >= 0) {
                format.LineAlignment = StringAlignment.Center;
            } else if (textAlign.IndexOf("Top") >= 0) {
                format.LineAlignment = StringAlignment.Near;
            }
        }
    }

    public class CheckChangedEventArgs : EventArgs
    {
        private bool isChecked;

        public CheckChangedEventArgs(bool isChecked)
            : base()
        {
            this.isChecked = isChecked;
        }

        public bool Checked
        {
            get
            {
                return this.isChecked;
            }
            set
            {
                this.isChecked = value;
            }
        }
    }
}
