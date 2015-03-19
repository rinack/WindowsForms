using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Runtime.InteropServices;

namespace Windows.Forms.Controls.Metros
{
   
        
    public partial class MyBook : UserControl
    {
        private const int SET_FEATURE_ON_THREAD = 0x00000001;
        private const int SET_FEATURE_ON_PROCESS = 0x00000002;
        private const int SET_FEATURE_IN_REGISTRY = 0x00000004;
        private const int SET_FEATURE_ON_THREAD_LOCALMACHINE = 0x00000008;
        private const int SET_FEATURE_ON_THREAD_INTRANET = 0x00000010;
        private const int SET_FEATURE_ON_THREAD_TRUSTED = 0x00000020;
        private const int SET_FEATURE_ON_THREAD_INTERNET = 0x00000040;
        private const int SET_FEATURE_ON_THREAD_RESTRICTED = 0x00000080;
        [DllImport("urlmon.dll")]
        [PreserveSig]
        [return: MarshalAs(UnmanagedType.Error)]
        static extern int CoInternetSetFeatureEnabled(
             INTERNETFEATURELIST FeatureEntry,
             [MarshalAs(UnmanagedType.U4)] int dwFlags,
             bool fEnable);
        public MyBook()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            mc = new MyBookScrollbar(pan);

            //注册事件
            this.MouseWheel += new MouseEventHandler(FormSample_MouseWheel);
            web.IsWebBrowserContextMenuEnabled = false;//是否启用快捷键
            web.WebBrowserShortcutsEnabled = false;//以防止 WebBrowser 控件响应快捷键。
           
            CoInternetSetFeatureEnabled(INTERNETFEATURELIST.FEATURE_DISABLE_NAVIGATION_SOUNDS, SET_FEATURE_ON_PROCESS, true); 
            
        }
        /// <summary>
        /// 获取或者设置滚动条背景色
        /// </summary>
        [DefaultValue(typeof(Color), "LightYellow"), Category("ControlColor")]
        [Description("滚动条的背景颜色")]
        public Color ScrollBackColor
        {
            get { return mc.BackColor; }
            set { mc.BackColor = value; }
        }
        /// <summary>
        /// 获取或者设置滚动条滑块默认颜色
        /// </summary>
        [DefaultValue(typeof(Color), "Gray"), Category("ControlColor")]
        [Description("滚动条滑块默认情况下的颜色")]
        public Color ScrollSliderDefaultColor
        {
            get { return mc.SliderDefaultColor; }
            set { mc.SliderDefaultColor = value; }
        }
        /// <summary>
        /// 获取或者设置滚动条点下的颜色
        /// </summary>
        [DefaultValue(typeof(Color), "DarkGray"), Category("ControlColor")]
        [Description("滚动条滑块被点击或者鼠标移动到上面时候的颜色")]
        public Color ScrollSliderDownColor
        {
            get { return mc.SliderDownColor; }
            set { mc.SliderDownColor = value; }
        }
        /// <summary>
        /// 获取或者设置滚动条箭头的背景色
        /// </summary>
        [DefaultValue(typeof(Color), "Gray"), Category("ControlColor")]
        [Description("滚动条箭头的背景颜色")]
        public Color ScrollArrowBackColor
        {
            get { return mc.ArrowBackColor; }
            set { mc.ArrowBackColor = value; }
        }
        /// <summary>
        /// 获取或者设置滚动条的箭头颜色
        /// </summary>
        [DefaultValue(typeof(Color), "White"), Category("ControlColor")]
        [Description("滚动条箭头的颜色")]
        public Color ScrollArrowColor
        {
            get { return mc.ArrowColor; }
            set { mc.ArrowColor = value; }
        }

        private Color arrowColor;
        /// <summary>
        /// 获取或者设置列表项箭头的颜色
        /// </summary>
        [DefaultValue(typeof(Color), "DarkGray"), Category("ControlColor")]
        [Description("列表项上面的箭头的颜色")]
        public Color ArrowColor
        {
            get { return arrowColor; }
            set
            {
                if (arrowColor == value) return;
                arrowColor = value;
                this.Invalidate();
            }
        }
        public Uri Url
        {
            get
            {
                return web.Url;
            }
        }
        public Uri Navigate
        {
            get
            {
                return web.Url;
            }
            set 
            {
                web.Url = value;
            }
        }
         void FormSample_MouseWheel(object sender, MouseEventArgs e)
        {
            //获取光标位置
            //Point mousePoint = new Point(e.X,e.Y);
            //if (e.X>this.Width-10&&e.X<this.Width)
            //{
                if (e.Delta > 0) mc.Value -= 50;
                if (e.Delta < 0) mc.Value += 50;
                if (ScrollNew != null)
                    ScrollNew(this, new EventArgs());
            //}
            
        }

         public MyBookScrollbar mc;

        private void web_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            if (web.ReadyState == WebBrowserReadyState.Complete)
            {
                mc.VirtualHeight = web.Document.Body.ScrollRectangle.Height;
                pan.Invalidate();
                //web.Document.Body.Style.Insert(0,"border:0px;");
                //web.Document.Body.Style.Insert(0, "overflow:hidden;");
                //web.Document.Body.Style.Insert(0, "margin:0px;"); 
                web.Document.Encoding = Encoding.Default.ToString();
            }
        }
        public  event EventHandler ScrollNew = null;

        private Point m_ptMousePos;             //鼠标的位置
  
        private void pan_Paint(object sender, PaintEventArgs e)
        {
            if (mc.ShouldBeDraw)   //是否绘制滚动条
                mc.ReDrawScroll(pan.CreateGraphics());
        }

        private void pan_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {        //如果左键在滚动条滑块上点击
                if (mc.SliderBounds.Contains(e.Location))
                {
                    mc.IsMouseDown = true;
                    mc.MouseDownY = e.Y;
                }
            }
            if (ScrollNew != null)
                ScrollNew(this, new EventArgs());
            this.Focus();
        }

        private void pan_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                mc.IsMouseDown = false;
        }

        private void pan_MouseMove(object sender, MouseEventArgs e)
        {
            m_ptMousePos = e.Location;
            if (mc.IsMouseDown)
            {          //如果滚动条的滑块处于被点击 那么移动
                mc.MoveSliderFromLocation(e.Y);
                if (ScrollNew != null)
                    ScrollNew(this, new EventArgs());
                return;
            }
            if (mc.ShouldBeDraw)
            {         //如果控件上有滚动条 判断鼠标是否在滚动条区域移动
                if (mc.Bounds.Contains(m_ptMousePos))
                {

                    if (mc.SliderBounds.Contains(m_ptMousePos))
                        mc.IsMouseOnSlider = true;
                    else
                        mc.IsMouseOnSlider = false;
                    if (mc.UpBounds.Contains(m_ptMousePos))
                        mc.IsMouseOnUp = true;
                    else
                        mc.IsMouseOnUp = false;
                    if (mc.DownBounds.Contains(m_ptMousePos))
                        mc.IsMouseOnDown = true;
                    else
                        mc.IsMouseOnDown = false;
                    return;
                }
                else
                    mc.ClearAllMouseOn();
            }
            m_ptMousePos.Y += mc.Value;        //如果不在滚动条范围类 那么根据滚动条当前值计算虚拟的一个坐标
   
            base.OnMouseMove(e);
        }

        private void pan_MouseLeave(object sender, EventArgs e)
        {
            mc.ClearAllMouseOn();
        }

        private void pan_MouseClick(object sender, MouseEventArgs e)
        {
            if (mc.IsMouseDown) return;    //MouseUp事件触发在Click后 滚动条滑块为点下状态 单击无效
            if (mc.ShouldBeDraw)
            {         //如果有滚动条 判断是否在滚动条类点击
                if (mc.Bounds.Contains(m_ptMousePos))
                {        //判断在滚动条那个位置点击
                    if (mc.UpBounds.Contains(m_ptMousePos))
                        mc.Value -= 50;
                    else if (mc.DownBounds.Contains(m_ptMousePos))
                        mc.Value += 50;
                    else if (!mc.SliderBounds.Contains(m_ptMousePos))
                        mc.MoveSliderToLocation(m_ptMousePos.Y);
                    return;
                }
                if (ScrollNew != null)
                    ScrollNew(this, new EventArgs());
            }//否则 如果在列表上点击 展开或者关闭 在子项上面点击则选中
           
        }

        private void pan_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (mc.Bounds.Contains(PointToClient(MousePosition))) return;  //如果双击在滚动条上返回
        }

        private void web_FileDownload(object sender, EventArgs e)
        {
            web.ScrollBarsEnabled = false;
        }

        private void MetroBook_SizeChanged(object sender, EventArgs e)
        {
            pan.Invalidate();
        }

        private void web_SizeChanged(object sender, EventArgs e)
        {
            if (web!=null)
            {
                if (web.Document!=null)
                {
                    if (web.Document.Body!=null)
                    {
                        if (web.Document.Body.ScrollRectangle!=null)
                        {
                            mc.VirtualHeight = web.Document.Body.ScrollRectangle.Height;
                        }
                    }
                }   
            }
        }
        public enum INTERNETFEATURELIST
        {
            FEATURE_OBJECT_CACHING = 0,
            FEATURE_ZONE_ELEVATION = 1,
            FEATURE_MIME_HANDLING = 2,
            FEATURE_MIME_SNIFFING = 3,
            FEATURE_WINDOW_RESTRICTIONS = 4,
            FEATURE_WEBOC_POPUPMANAGEMENT = 5,
            FEATURE_BEHAVIORS = 6,
            FEATURE_DISABLE_MK_PROTOCOL = 7,
            FEATURE_LOCALMACHINE_LOCKDOWN = 8,
            FEATURE_SECURITYBAND = 9,
            FEATURE_RESTRICT_ACTIVEXINSTALL = 10,
            FEATURE_VALIDATE_NAVIGATE_URL = 11,
            FEATURE_RESTRICT_FILEDOWNLOAD = 12,
            FEATURE_ADDON_MANAGEMENT = 13,
            FEATURE_PROTOCOL_LOCKDOWN = 14,
            FEATURE_HTTP_USERNAME_PASSWORD_DISABLE = 15,
            FEATURE_SAFE_BINDTOOBJECT = 16,
            FEATURE_UNC_SAVEDFILECHECK = 17,
            FEATURE_GET_URL_DOM_FILEPATH_UNENCODED = 18,
            FEATURE_TABBED_BROWSING = 19,
            FEATURE_SSLUX = 20,
            FEATURE_DISABLE_NAVIGATION_SOUNDS = 21,
            FEATURE_DISABLE_LEGACY_COMPRESSION = 22,
            FEATURE_FORCE_ADDR_AND_STATUS = 23,
            FEATURE_XMLHTTP = 24,
            FEATURE_DISABLE_TELNET_PROTOCOL = 25,
            FEATURE_FEEDS = 26,
            FEATURE_BLOCK_INPUT_PROMPTS = 27,
            FEATURE_ENTRY_COUNT = 28
        }  
    }
}
