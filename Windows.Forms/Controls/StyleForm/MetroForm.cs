using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Windows.Forms.Controls.Enums;
using Windows.Forms.Controls.Methods;
using Windows.Forms.Controls.WinAPI;
using Windows.Forms.Controls.WinForm;

namespace Windows.Forms.Controls.StyleForm
{
    public partial class MetroForm : BaseForm
    {
        private Windows.Forms.Controls.MetroForm.MainForm skin;

        public MetroForm()
            : base()
        {
            InitializeComponent();
        }


        #region 变量
        /// <summary>
        /// 边框图片
        /// </summary>
        private Image _borderImage = AssemblyHelper.GetImage("StanForm.FormFrame.Metro_fringe_bkg.png");
        #endregion

        private bool isRadius = false;
        /// <summary>
        /// 是否设置圆角
        /// </summary>
        public bool IsRadius
        {
            get { return isRadius; }
            set { isRadius = value; }
        }

        #region 属性
        /// <summary>
        /// 
        /// </summary>
        protected override Rectangle MaxRect
        {
            get { return new Rectangle(this.Width - this.CloseRect.Width - 30, -1, 30, 27); }
        }
        /// <summary>
        /// 
        /// </summary>
        protected override Rectangle MiniRect
        {
            get
            {
                int x = this.Width - this.CloseRect.Width - this.MaxRect.Width - 27;
                Rectangle rect = new Rectangle(x, -1, 30, 27);
                return rect;
                //return new Rectangle(this.Width - this.CloseRect.Width - 28, -1, 28, 20);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        protected override Rectangle SysBtnRect
        {
            get
            {
                if (base._sysButton == ESysButton.Normal)
                    return new Rectangle(this.Width - 30 * 2 - 30, 0, 39 + 30 + 30, 27);
                else if (base._sysButton == ESysButton.CloseMini)
                    return new Rectangle(this.Width - 30 - 30, 0, 39 + 30, 27);
                else
                    return this.CloseRect;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        protected override Rectangle CloseRect
        {
            get { return new Rectangle(this.Width - 30, -1, 30, 27); }
        }

        #endregion

        #region 方法
        /// <summary>
        /// 绘画按钮
        /// </summary>
        /// <param name="g">画板</param>
        /// <param name="mouseState">鼠标状态</param>
        /// <param name="rect">按钮区域</param>
        /// <param name="str">图片字符串</param>
        private void DrawButton(Graphics g, EMouseState mouseState, Rectangle rect, string str)
        {
            switch (mouseState)
            {
                case EMouseState.Normal:
                    g.DrawImage(AssemblyHelper.GetImage("System.sysbtn_" + str + "_normal.png"), rect);
                    break;
                case EMouseState.Move:
                case EMouseState.Up:
                    g.DrawImage(AssemblyHelper.GetImage("System.sysbtn_" + str + "_hover.png"), rect);
                    break;
                case EMouseState.Down:
                    g.DrawImage(AssemblyHelper.GetImage("System.sysbtn_" + str + "_down.png"), rect);
                    break;
            }
        }
        #endregion

        #region Override Methods
        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            //绘画系统控制按钮
            switch (base.SysButton)
            {
                case ESysButton.Normal:
                    this.DrawButton(g, base.MinState, this.MiniRect, "min");
                    this.DrawButton(g, base.CloseState, this.CloseRect, "close");
                    if (this.WindowState == FormWindowState.Maximized)
                        this.DrawButton(g, base.MaxState, this.MaxRect, "restore");
                    else
                        this.DrawButton(g, base.MaxState, this.MaxRect, "max");
                    break;
                case ESysButton.Close:
                    this.DrawButton(g, base.CloseState, this.CloseRect, "close");
                    break;
                case ESysButton.CloseMini:
                    this.DrawButton(g, base.MinState, this.MiniRect, "min");
                    this.DrawButton(g, base.CloseState, this.CloseRect, "close");
                    break;
            }

            //绘画边框
            g.DrawImage(this._borderImage, new Rectangle(0, 0, 10, 10), new Rectangle(5, 5, 10, 10), GraphicsUnit.Pixel);//左上角
            g.DrawImage(this._borderImage, new Rectangle(0, -5, 10, this.Height + 10), new Rectangle(5, 5, 10, this._borderImage.Height - 10), GraphicsUnit.Pixel);//左边框
            g.DrawImage(this._borderImage, new Rectangle(-5, this.Height - 10, 10, 10), new Rectangle(0, this._borderImage.Height - 15, 10, 10), GraphicsUnit.Pixel);//左下角
            g.DrawImage(this._borderImage, new Rectangle(this.Width - 9, -5, 10, 10), new Rectangle(20, 0, 10, 10), GraphicsUnit.Pixel);//右上角
            g.DrawImage(this._borderImage, new Rectangle(this.Width - 9, -5, 10, this.Height + 10), new Rectangle(20, 5, 10, this._borderImage.Height - 10), GraphicsUnit.Pixel);//右边框
            g.DrawImage(this._borderImage, new Rectangle(this.Width - 9, this.Height - 10, 10, 10), new Rectangle(20, this._borderImage.Height - 15, 10, 10), GraphicsUnit.Pixel);//右下角

            g.DrawImage(this._borderImage, new Rectangle(5, -5, this.Width - 10, 18), new Rectangle(12, 0, 6, 18), GraphicsUnit.Pixel);
            g.DrawImage(this._borderImage, new Rectangle(5, this.Height - 6, this.Width - 10, 18), new Rectangle(12, 0, 6, 18), GraphicsUnit.Pixel);

            base.OnPaint(e);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            if (isRadius)
            {
                //调用API，将窗体剪成圆角
                int rgn = NativeMethods.CreateRoundRectRgn(0, 0, this.Width + 1, this.Height + 1, 4, 4);
                NativeMethods.SetWindowRgn(this.Handle, rgn, true);
            }
        }

        #endregion

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
                    skin = new Windows.Forms.Controls.MetroForm.MainForm(this);
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
            if (isRadius)
            {
                SetReion();
            }
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
            if (isRadius)
            {
                SetReion();
            }
        }

        #endregion
    }
}
