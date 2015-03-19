using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace Windows.Forms.Controls.Metros
{
    [DefaultEvent("DoubleClick")]
    public partial class MetroPictureBoxFrame : UserControl
    {
        public MetroPictureBoxFrame()
        {
            InitializeComponent();
            OnMyTextValueChanged += new MyTextValueChanged(afterMyTextValueChanged);
        }
        public delegate void MyTextValueChanged(object sender, EventArgs e);
        //与委托相关联的事件
        public event MyTextValueChanged OnMyTextValueChanged;
        private void afterMyTextValueChanged(object sender, EventArgs e)
        {
            pictureBox2.Image = Image;
        }
        private Image _Image = AssemblyHelper.GetImage("StanForm.Picture.hooForSand_bg.jpg");

        public Image Image
        {

            get { return _Image; }
            set
            {
                if (value != _Image)
                {
                    _Image = value;
                    WhenMyTextValueChange();
                }
                _Image = value;
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
        private void MetroPictureBox_Load(object sender, EventArgs e)
        {

        }

        private void MetroPictureBox_Resize(object sender, EventArgs e)
        {
            Width = 94;
            Height = 96;
        }

        // pbPassword.BackgroundImage = QihooResources.Properties.Resources.edit_frame_normal;
         //pbPassword.BackHover = QihooResources.Properties.Resources.edit_frame_hover;

        

    }
}
