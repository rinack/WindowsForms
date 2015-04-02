using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Windows.Forms.Controls.AlphaForm;

namespace Windows.Test.AlphaForm
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            // On XP, the speed at which the main form fades is 
            // distractingly slow because of its size (see my note in OnShown below),
            // so we just fade out the layered window frame. 
            // NOTE: If you have the main form with a background image,
            // that matches for AFT's background image (i.e., controls on top),
            // you'll always want to fade in/out both windows.

            alphaFormTransformer1.Fade(FadeType.FadeOut, true,
              System.Environment.OSVersion.Version.Major < 6, 500);

            base.OnClosing(e);
        }

        protected override void OnShown(EventArgs e)
        {
            // I'm not real pleased with the speed that XP fades in
            // the main form when the form itself is somewhat large like
            // this one. Apparently when you have a Region, changing
            // the layered window opacity attribute doesn't draw very fast
            // for large regions. So here we only fade in the layered window.
            // NOTE: If you have the main form with a background image,
            // that matches for AFT's background image (sans alpha channel),
            // you'll always want to fade in/out both, windows otherwise 
            // it will look ugly. Look at the ControlsOnTopOfSkin project
            // where we've added calls to Fade().

            alphaFormTransformer1.Fade(FadeType.FadeIn, false,
             System.Environment.OSVersion.Version.Major < 6, 400);
            base.OnShown(e);
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            alphaFormTransformer1.TransformForm(0); 
        }

        private void offButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void surfButton_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void webBrowser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {

        }

        private void rdLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }

        private void blogLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }

        private void gbLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void alphaFormTransformer1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void infoLabel_Click(object sender, EventArgs e)
        {

        }

        private void surfURL_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
