using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace Windows.Forms.Controls.Metros
{
    [DefaultEvent("Click")]
    public partial class MetroButton : UserControl
    {
        public MetroButton()
        {
            InitializeComponent();
            OnMyValueChanged += new MyValueChanged(afterMyValueChanged);
        }
        //事件处理函数，在这里添加变量改变之后的操作
        private void afterMyValueChanged(object sender, EventArgs e)
        {
            LabText.Text = Texts;
            //初始化文字 位置
            LabText.Location = new Point((Width - LabText.Width) / 2, (Height - LabText.Height) / 2);
        }
        //定义的委托
        public delegate void MyValueChanged(object sender, EventArgs e);
        //与委托相关联的事件
        public event MyValueChanged OnMyValueChanged;

        private string _Text = "登陆";

        public  string Texts
        {
            get { return _Text; }
            set
            {
                if (value != _Text)
                {
                    _Text = value;
                    WhenMyValueChange();
                }
                _Text = value;
            }
        }
        private void WhenMyValueChange()
        {
            if (OnMyValueChanged != null)
            {
                OnMyValueChanged(this, null);
            }
        }


        private void MetroButton_Load(object sender, EventArgs e)
        {
           
        }

        private void MetroButton_MouseEnter_1(object sender, EventArgs e)
        {
            BackgroundImage = AssemblyHelper.GetImage("StanForm.Button.button_login_hover.png");
        }

        private void MetroButton_MouseLeave_1(object sender, EventArgs e)
        {
            BackgroundImage = AssemblyHelper.GetImage("StanForm.Button.button_login_normal.png");
        }

        private void MetroButton_MouseDown_1(object sender, MouseEventArgs e)
        {
            BackgroundImage = AssemblyHelper.GetImage("StanForm.Button.button_login_down.png");
        }

        private void LabText_MouseEnter(object sender, EventArgs e)
        {
            BackgroundImage = AssemblyHelper.GetImage("StanForm.Button.button_login_hover.png");
        }

        private void LabText_MouseDown(object sender, MouseEventArgs e)
        {
            BackgroundImage = AssemblyHelper.GetImage("StanForm.Button.button_login_down.png");
        }

        private void LabText_Click(object sender, EventArgs e)
        {
            base.OnClick(e);
        }

        private void MetroButton_Click(object sender, EventArgs e)
        {
            //base.OnClick(e);
        }

        private void MetroButton_Resize(object sender, EventArgs e)
        {
            //初始化文字 位置
           
        }

        private void MetroButton_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawString(LabText.Text, this.Font, new SolidBrush(this.ForeColor), new Point((Width - LabText.Width) / 2, (Height - LabText.Height+8) / 2));
        }
    }
}
