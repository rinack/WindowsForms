using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Windows.Forms.Controls.Forms.WinForm;
using Windows.Forms.Controls.Methods;
using Windows.Forms.Controls.WinAPI;

namespace Windows.Forms.Controls.StyleForm
{
    public partial class BurrsForm : BaseForm
    {
        private MainForm skin;

        public BurrsForm()
        {
            InitializeComponent();
        }

        #region 重载事件
        //Show或Hide被调用时
        protected override void OnVisibleChanged(EventArgs e)
        {
            if (Visible)
            {
                //启用窗口淡入淡出
                if (!DesignMode)
                {
                    //淡入特效
                    Win32.AnimateWindow(this.Handle, 150, Win32.AW_BLEND | Win32.AW_ACTIVATE);
                }
                //判断不是在设计器中
                if (!DesignMode && skin == null)
                {
                    skin = new MainForm(this);
                    skin.Show(this);
                }
                base.OnVisibleChanged(e);
            }
            else
            {
                base.OnVisibleChanged(e);
                Win32.AnimateWindow(this.Handle, 150, Win32.AW_BLEND | Win32.AW_HIDE);
            }
        }

        //窗体关闭时
        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
            //先关闭阴影窗体
            if (skin != null)
            {
                skin.Close();
            }
            //在Form_FormClosing中添加代码实现窗体的淡出
            Win32.AnimateWindow(this.Handle, 150, Win32.AW_BLEND | Win32.AW_HIDE);
        }

        //控件首次创建时被调用
        protected override void OnCreateControl()
        {
            base.OnCreateControl();
            SetReion();
        }

        //圆角
        private void SetReion()
        {
            using (GraphicsPath path =
                    GraphicsPathHelper.CreatePath(
                    new Rectangle(Point.Empty, base.Size), 6, RoundStyle.All, true))
            {
                Region region = new Region(path);
                path.Widen(Pens.White);
                region.Union(path);
                this.Region = region;
            }
        }

        //改变窗体大小时
        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            SetReion();
        }

        ////移动窗体
        //protected override void OnMouseDown(MouseEventArgs e)
        //{
        //    if (SkinMobile)
        //    {
        //        //释放鼠标焦点捕获
        //        Win32.ReleaseCapture();
        //        //向当前窗体发送拖动消息
        //        Win32.SendMessage(this.Handle, 0x0112, 0xF011, 0);
        //        base.OnMouseUp(e);
        //    }
        //    base.OnMouseDown(e);
        //}
        #endregion
    }
}
