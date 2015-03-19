using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Windows.Forms.Controls.StyleForm;

namespace Windows.Test
{
    public partial class Form4 : BurrsForm
    {
        public Form4()
        {
            InitializeComponent();
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            NotifyForm.AnimalShow("消息提示", "“末日”前晒出流逝的岁月，上传一组证明您岁月痕迹的新老对比照片，即可获得抽奖资格和微博积分");
        }
    }
}
