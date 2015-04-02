using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Windows.Forms;
using Windows.Forms.Controls.StyleForm;

namespace Windows.Test
{
    public partial class Form5 : MetroForm
    {
        public Form5()
        {
            InitializeComponent();

            image = AssemblyHelper.GetImage("Icons.start.png");
            Init();
        }

        private string _toolTipTitle = "ToolTipEx示例";
        private string _toolTip =
            "CS 程序员之窗-ToolTipEx示例-{0}。\r\n" +
            "                    ——Statts_2000\r\n" +
            "                         2010.01.09";
        private Image image;

        private void SetToolTip(Control control, string tip)
        {
            toolTipEx.SetToolTip(
                control,
                string.Format(_toolTip, tip));
        }

        private void ResetToolTip()
        {
            toolTipEx.Active = false;
            toolTipEx.Opacity = 1D;
            toolTipEx.ImageSize = new Size(16, 16);
            toolTipEx.Image = null;
            toolTipEx.ToolTipTitle = "";
        }

        private void Init()
        {
            toolTipEx.Active = false;
            SetToolTip(label1, "无标题");
            SetToolTip(label2, "系统图标");
            SetToolTip(label3, "自定义图标");
            SetToolTip(label4, "大图标");
            SetToolTip(label5, "透明");

            label1.MouseEnter += delegate(object sender, EventArgs e)
            {
                toolTipEx.Active = true;
            };

            label2.MouseEnter += delegate(object sender, EventArgs e)
            {
                toolTipEx.ToolTipIcon = ToolTipIcon.Info;
                toolTipEx.ToolTipTitle = _toolTipTitle;
                toolTipEx.Active = true;
            };

            label3.MouseEnter += delegate(object sender, EventArgs e)
            {
                toolTipEx.Image = image;
                toolTipEx.ToolTipTitle = _toolTipTitle;
                toolTipEx.Active = true;
            };

            label4.MouseEnter += delegate(object sender, EventArgs e)
            {
                toolTipEx.ImageSize = new Size(32, 32);
                toolTipEx.Image = image;
                toolTipEx.ToolTipTitle = _toolTipTitle;
                toolTipEx.Active = true;
            };

            label5.MouseEnter += delegate(object sender, EventArgs e)
            {
                toolTipEx.Opacity = 0.7D;
                toolTipEx.ImageSize = new Size(24, 24);
                toolTipEx.Image = image;
                toolTipEx.ToolTipTitle = _toolTipTitle;
                toolTipEx.Active = true;
            };

            label1.MouseLeave += delegate(object sender, EventArgs e)
            {
                ResetToolTip();
            };

            label2.MouseLeave += delegate(object sender, EventArgs e)
            {
                ResetToolTip();
            };

            label3.MouseLeave += delegate(object sender, EventArgs e)
            {
                ResetToolTip();
            };

            label4.MouseLeave += delegate(object sender, EventArgs e)
            {
                ResetToolTip();
            };

            label5.MouseLeave += delegate(object sender, EventArgs e)
            {
                ResetToolTip();
            };
        }
    }
}
