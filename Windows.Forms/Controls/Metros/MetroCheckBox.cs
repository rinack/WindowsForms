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
    public partial class MetroCheckBox : UserControl
    {
        public MetroCheckBox()
        {
            InitializeComponent();
            OnMyValueChanged += new MyValueChanged(afterMyValueChanged);
            OnMyTextValueChanged += new MyTextValueChanged(afterMyTextValueChanged);
           
        }

        public delegate void MyTextValueChanged(object sender, EventArgs e);
        //与委托相关联的事件
        public event MyTextValueChanged OnMyTextValueChanged;
        private void afterMyTextValueChanged(object sender, EventArgs e)
        {
            label1.Text = _Text;
        }
        private string _Text = "";

        public string Texts
        {

            get { return _Text; }
            set
            {
                if (value != _Text)
                {
                    _Text = value;
                    WhenMyTextValueChange();
                }
                _Text = value;
            }
        }
        //事件触发函数
        private void WhenMyTextValueChange()
        {
            if (OnMyTextValueChanged != null)
            {
                OnMyTextValueChanged(this, null);
            }
        }

        


        private bool _Checked = true;

        public bool Checked
        {

            get { return _Checked; }
            set
            {
                if (value != _Checked)
                {
                    _Checked = value;
                    WhenMyValueChange();
                }
                _Checked = value;
            }
        }

        //定义的委托
        public delegate void MyValueChanged(object sender, EventArgs e);
        //与委托相关联的事件
        public event MyValueChanged OnMyValueChanged;
        

        //事件处理函数，在这里添加变量改变之后的操作
        private void afterMyValueChanged(object sender, EventArgs e)
        {
            //do something
            if (Checked)
            {
                pictureBox1.Image = AssemblyHelper.GetImage("StanForm.CheckBox.checkbox_tick_normal1.png");
            }
            else
            {
                pictureBox1.Image = AssemblyHelper.GetImage("StanForm.CheckBox.checkbox_normal.png");
            }
        }
        //事件触发函数
        private void WhenMyValueChange()
        {
            if (OnMyValueChanged != null)
            {
                OnMyValueChanged(this, null);
            }
        }
     

        private void MetroCheckBox_MouseEnter(object sender, EventArgs e)
        {
            if (Checked)
            {
                pictureBox1.Image = AssemblyHelper.GetImage("StanForm.CheckBox.checkbox_tick_highlight1.png");
            }
            else
            {
                pictureBox1.Image = AssemblyHelper.GetImage("StanForm.CheckBox.checkbox_hightlight.png");
            }
        }

        private void MetroCheckBox_MouseDown(object sender, MouseEventArgs e)
        {
            if (Checked)
            {
                pictureBox1.Image = AssemblyHelper.GetImage("StanForm.CheckBox.checkbox_tick_pushed1.png");
            }
            else
	        {
                pictureBox1.Image = AssemblyHelper.GetImage("StanForm.CheckBox.checkbox_pushed.png");
	        }
        }

        private void MetroCheckBox_MouseLeave(object sender, EventArgs e)
        {
            if (Checked)
            {
                pictureBox1.Image = AssemblyHelper.GetImage("StanForm.CheckBox.checkbox_tick_normal1.png");
            }
            else
            {
                pictureBox1.Image = AssemblyHelper.GetImage("StanForm.CheckBox.checkbox_normal.png");
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {
            base.OnClick(e);
        }

        private void MetroCheckBox_Click(object sender, EventArgs e)
        {
            if (Checked==false)
            {
                Checked = true;
            }
            else
            {
                Checked = false;
            }
        }

        private void label1_MouseEnter(object sender, EventArgs e)
        {
            if (Checked)
            {
                pictureBox1.Image = AssemblyHelper.GetImage("StanForm.CheckBox.checkbox_tick_highlight1.png");
            }
            else
            {
                pictureBox1.Image = AssemblyHelper.GetImage("StanForm.CheckBox.checkbox_hightlight.png"); 
            }
        }

        private void pictureBox1_MouseLeave(object sender, EventArgs e)
        {
            if (Checked)
            {
                pictureBox1.Image = AssemblyHelper.GetImage("StanForm.CheckBox.checkbox_tick_normal1.png"); 
            }
            else
            {
                pictureBox1.Image = AssemblyHelper.GetImage("StanForm.CheckBox.checkbox_normal.png");
            }
        }

        private void label1_MouseLeave(object sender, EventArgs e)
        {
           
            if (Checked)
            {
                pictureBox1.Image = AssemblyHelper.GetImage("StanForm.CheckBox.checkbox_tick_normal1.png");
            }
            else
            {
                pictureBox1.Image = AssemblyHelper.GetImage("StanForm.CheckBox.checkbox_normal.png");
            }
        }
    }
}
