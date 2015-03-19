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
    public partial class MetroTextBoxSearch : UserControl
    {
        public MetroTextBoxSearch()
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
                myTextBox1.WaterText = _WaterText;
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
            myTextBox1.Text = _Text;
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
        public override string Text
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


        //定义的委托
        public delegate void MyValueChanged(object sender, EventArgs e);
        //与委托相关联的事件
        public event MyValueChanged OnMyValueChanged;



        //事件处理函数，在这里添加变量改变之后的操作
        private void afterMyValueChanged(object sender, EventArgs e)
        {
            main.BackgroundImage = AssemblyHelper.GetImage("StanForm.TextBox.edit_frame_hover_reversed.png");
                myTextBox1.UseSystemPasswordChar = false;
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
            Text = myTextBox1.Text;
        }

        private void Btn_MouseEnter(object sender, EventArgs e)
        {
            Btn.BackgroundImage = AssemblyHelper.GetImage("StanForm.TextBox.serbtnhover.png");
        }

        private void Btn_MouseLeave(object sender, EventArgs e)
        {
            Btn.BackgroundImage = AssemblyHelper.GetImage("StanForm.TextBox.serbtnnomar.png");
        }

        private void Btn_MouseClick(object sender, MouseEventArgs e)
        {
            Btn.BackgroundImage = AssemblyHelper.GetImage("StanForm.TextBox.serbtnpres.png");
        }

        private void label1_Click(object sender, EventArgs e)
        {
            base.OnClick(e);
        }

    }
}
