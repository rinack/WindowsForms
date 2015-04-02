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

namespace Windows.Test.AlphaForm
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        private void skinButton1_Click(object sender, EventArgs e)
        {
            Bitmap bmap = new Bitmap(AssemblyHelper.GetImage("AlphaForm.skin1.tif"));
            alphaFormTransformer1.UpdateSkin(bmap, null, 255);
        }

        private void skinButton2_Click(object sender, EventArgs e)
        {
            Bitmap bmap = new Bitmap(AssemblyHelper.GetImage("AlphaForm.skin2.tif"));
            alphaFormTransformer1.UpdateSkin(bmap, null, 255);
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
