using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Windows.Forms;
using Windows.Forms.Controls.AlphaForm;

namespace Windows.Test.AlphaForm
{
    public partial class Form5 : Form
    {
        public Form5()
        {
            InitializeComponent();
        }

        protected override void OnShown(EventArgs e)
        {
            alphaFormTransformer1.Fade(FadeType.FadeIn, false,
             false, 500);
            base.OnShown(e);
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            alphaFormTransformer1.Fade(FadeType.FadeOut, true,
              false, 500);

            base.OnClosing(e);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = new Bitmap(AssemblyHelper.GetImage("AlphaForm.tvpic1.jpg"));
        }

        private void button2_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = new Bitmap(AssemblyHelper.GetImage("AlphaForm.tvpic2.jpg"));
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Form5_Load(object sender, EventArgs e)
        {
            alphaFormTransformer1.TransformForm(0);
            pictureBox1.Image = new Bitmap(AssemblyHelper.GetImage("AlphaForm.tvpic1.jpg"));
        }
    }
}
