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
    public partial class MetroTextBox : UserControl
    {
        public MetroTextBox()
        {
            InitializeComponent();
            OnMyValueChanged += new MyValueChanged(afterMyValueChanged);
            OnMyTextValueChanged += new MyTextValueChanged(afterMyTextValueChanged);
            OnTextValueChanged += new MyTextValueChanged(afterTextValueChanged);
        }//定义的委托
        public delegate void MyTextValueChanged(object sender, EventArgs e);
        //与委托相关联的事件
        public event MyTextValueChanged OnMyTextValueChanged;
        public event MyTextValueChanged OnTextValueChanged;
        private void afterMyTextValueChanged(object sender, EventArgs e)
        {
                myTextBox2.WaterText = _WaterText;
        }
        //事件触发函数
        private void WhenTextValueChange()
        {
            if (OnTextValueChanged != null)
            {
                OnTextValueChanged(this, null);
            }
        }
        private void afterTextValueChanged(object sender, EventArgs e)
        {
            myTextBox2.Text = _Text;
        }
        private string _WaterText = "";

        public string WaterText
        {

            get { return _WaterText; }
            set
            {
                if (value != _WaterText)
                {
                    _WaterText = value;
                    WhenMyTextValueChange();
                }
                _WaterText = value;
            }
        }
        private string _Text;
        [Category("Skin")]
        [Description("文字")]
        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        public new string Text
        {
            get { return _Text; }
            set
            {
                if (value != _Text)
                {
                    _Text = value;
                    WhenTextValueChange();
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

        private void panel1_MouseEnter(object sender, EventArgs e)
        {
            if (IsPassWord)
            {
                panel1.BackgroundImage = AssemblyHelper.GetImage("StanForm.TextBox.edit_frame_hover.png");
            }
            else
            {
                panel1.BackgroundImage = AssemblyHelper.GetImage("StanForm.TextBox.edit_frame_hover_reversed.png"); 
            }
          
        }

        private void MetroTextBox_MouseLeave(object sender, EventArgs e)
        {
            if (IsPassWord)
            {
                panel1.BackgroundImage = AssemblyHelper.GetImage("StanForm.TextBox.edit_frame_normal.png");
            }
            else
            {
                panel1.BackgroundImage = AssemblyHelper.GetImage("StanForm.TextBox.edit_frame_normal_reversed.png");
            }
        }

        private void myTextBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (IsPassWord)
            {
                panel1.BackgroundImage = AssemblyHelper.GetImage("StanForm.TextBox.edit_frame_hover.png");
            }
            else
            {
                panel1.BackgroundImage = AssemblyHelper.GetImage("StanForm.TextBox.edit_frame_hover_reversed.png"); 
            }
        }

        private bool _IsPassWord = false;

        public bool IsPassWord
        {

            get { return _IsPassWord; }
            set
            {
                if (value != _IsPassWord)
                {
                    _IsPassWord = value;
                    WhenMyValueChange();
                }
                _IsPassWord = value;
            }
        }

        //定义的委托
        public delegate void MyValueChanged(object sender, EventArgs e);
        //与委托相关联的事件
        public event MyValueChanged OnMyValueChanged;



        //事件处理函数，在这里添加变量改变之后的操作
        private void afterMyValueChanged(object sender, EventArgs e)
        {
            if (_IsPassWord)//
            {
                panel1.BackgroundImage = AssemblyHelper.GetImage("StanForm.TextBox.edit_frame_normal.png");
                myTextBox2.UseSystemPasswordChar = true;
            }
            else
            {
                panel1.BackgroundImage = AssemblyHelper.GetImage("StanForm.TextBox.edit_frame_normal_reversed.png");
                myTextBox2.UseSystemPasswordChar = false;
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

        private void myTextBox1_TextChanged(object sender, EventArgs e)
        {
            _Text = myTextBox2.Text;
        }
    }
}
