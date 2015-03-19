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
    public partial class MetroButtonSearch : UserControl
    {

         public MetroButtonSearch()
        {
            InitializeComponent();
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            pictureBox1.Image = AssemblyHelper.GetImage("StanForm.Find.find_hover.png");
        }

        private void pictureBox1_MouseEnter(object sender, EventArgs e)
        {
            pictureBox1.Image = AssemblyHelper.GetImage("StanForm.Find.find_hover.png");
        }

        private void pictureBox1_MouseLeave(object sender, EventArgs e)
        {
            pictureBox1.Image = AssemblyHelper.GetImage("StanForm.Find.Finger_Normal.png");
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            base.OnClick(e);
        }
    }
}
