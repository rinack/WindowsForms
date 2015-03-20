using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Windows.Forms.Controls.QQControls;
using Windows.Forms.Controls.StyleForm;
using System.Drawing.Drawing2D;
using Windows.Forms.Controls.Enums;
using Windows.Forms;

namespace Windows.Test
{
    public partial class DemoForm : StanForm
    {
        public DemoForm()
        {
            InitializeComponent();
        }

        #region Override

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                if (!DesignMode)
                {
                    cp.ExStyle |= (int)WindowStyle.WS_CLIPCHILDREN;
                }
                return cp;
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            DrawFromAlphaMainPart(this, e.Graphics);
        }

        #endregion

        #region Private

        /// <summary>
        /// 绘制窗体主体部分白色透明层
        /// </summary>
        /// <param name="form"></param>
        /// <param name="g"></param>
        public static void DrawFromAlphaMainPart(Form form, Graphics g)
        {
            Color[] colors = 
            {
                Color.FromArgb(5, Color.White),
                Color.FromArgb(30, Color.White),
                Color.FromArgb(145, Color.White),
                Color.FromArgb(150, Color.White),
                Color.FromArgb(30, Color.White),
                Color.FromArgb(5, Color.White)
            };

            float[] pos = 
            {
                0.0f,
                0.04f,
                0.10f,
                0.90f,
                0.97f,
                1.0f      
            };

            ColorBlend colorBlend = new ColorBlend(6);
            colorBlend.Colors = colors;
            colorBlend.Positions = pos;

            RectangleF destRect = new RectangleF(0, 0, form.Width, form.Height);
            using (LinearGradientBrush lBrush = new LinearGradientBrush(destRect, colors[0], colors[5], LinearGradientMode.Vertical))
            {
                lBrush.InterpolationColors = colorBlend;
                g.FillRectangle(lBrush, destRect);
            }
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

        #endregion

        private void FormDemo_Load(object sender, EventArgs e)
        {
            this.Left = (Screen.PrimaryScreen.WorkingArea.Width - Width) / 2;
            this.Top = (Screen.PrimaryScreen.WorkingArea.Height - Height) / 2;
        }

        private void qqButton3_Click(object sender, EventArgs e)
        {
            QQMessageBox.Show(
                this,
                "更改用户信息成功！",
                "提示",
                QQMessageBoxIcon.OK,
                QQMessageBoxButtons.OK);
        }

        #region ToolTip 控件浮动提示消息

        private string _toolTipTitle = "ToolTipEx示例";
        private string _toolTip =
            "ToolTipEx示例-{0}。\r\n" +
            "                    ——浮动提示\r\n" +
            "                         2015.03.20";

        private Image image= AssemblyHelper.GetImage("Icons.start.png");
       
        private void SetToolTip(Control control, string tip)
        {
            toolTipEx1.SetToolTip(
                control,
                string.Format(_toolTip, tip));
        }

        private void ResetToolTip()
        {
            toolTipEx1.Active = false;
            toolTipEx1.Opacity = 1D;
            toolTipEx1.ImageSize = new Size(16, 16);
            toolTipEx1.Image = null;
            toolTipEx1.ToolTipTitle = "";
        }

        private void Init()
        {
            toolTipEx1.Active = false;
            //SetToolTip(label1, "无标题");
            //SetToolTip(label2, "系统图标");
            //SetToolTip(label3, "自定义图标");
            //SetToolTip(label4, "大图标");
            //SetToolTip(label5, "透明");

            //label1.MouseEnter += delegate(object sender, EventArgs e)
            //{
            //    toolTipEx1.Active = true;
            //};

            //label2.MouseEnter += delegate(object sender, EventArgs e)
            //{
            //    toolTipEx1.ToolTipIcon = ToolTipIcon.Info;
            //    toolTipEx1.ToolTipTitle = _toolTipTitle;
            //    toolTipEx1.Active = true;
            //};

            //label3.MouseEnter += delegate(object sender, EventArgs e)
            //{
            //    toolTipEx1.Image = image;
            //    toolTipEx1.ToolTipTitle = _toolTipTitle;
            //    toolTipEx1.Active = true;
            //};

            //label4.MouseEnter += delegate(object sender, EventArgs e)
            //{
            //    toolTipEx1.ImageSize = new Size(32, 32);
            //    toolTipEx1.Image = image;
            //    toolTipEx1.ToolTipTitle = _toolTipTitle;
            //    toolTipEx1.Active = true;
            //};

            //label5.MouseEnter += delegate(object sender, EventArgs e)
            //{
            //    toolTipEx1.Opacity = 0.7D;
            //    toolTipEx1.ImageSize = new Size(24, 24);
            //    toolTipEx1.Image = image;
            //    toolTipEx1.ToolTipTitle = _toolTipTitle;
            //    toolTipEx1.Active = true;
            //};

            //label1.MouseLeave += delegate(object sender, EventArgs e)
            //{
            //    ResetToolTip();
            //};

            //label2.MouseLeave += delegate(object sender, EventArgs e)
            //{
            //    ResetToolTip();
            //};

            //label3.MouseLeave += delegate(object sender, EventArgs e)
            //{
            //    ResetToolTip();
            //};

            //label4.MouseLeave += delegate(object sender, EventArgs e)
            //{
            //    ResetToolTip();
            //};

            //label5.MouseLeave += delegate(object sender, EventArgs e)
            //{
            //    ResetToolTip();
            //};
        }

        #endregion
    }
}
