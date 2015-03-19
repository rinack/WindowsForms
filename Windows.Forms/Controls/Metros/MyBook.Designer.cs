namespace Windows.Forms.Controls.Metros
{
    partial class MyBook
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.web = new System.Windows.Forms.WebBrowser();
            this.pan = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // web
            // 
            this.web.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.web.Location = new System.Drawing.Point(25, 0);
            this.web.MinimumSize = new System.Drawing.Size(20, 20);
            this.web.Name = "web";
            this.web.ScriptErrorsSuppressed = true;
            this.web.ScrollBarsEnabled = false;
            this.web.Size = new System.Drawing.Size(450, 383);
            this.web.TabIndex = 5;
            this.web.Url = new System.Uri("", System.UriKind.Relative);
            this.web.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.web_DocumentCompleted);
            this.web.FileDownload += new System.EventHandler(this.web_FileDownload);
            this.web.SizeChanged += new System.EventHandler(this.web_SizeChanged);
            // 
            // pan
            // 
            this.pan.Dock = System.Windows.Forms.DockStyle.Right;
            this.pan.Location = new System.Drawing.Point(490, 0);
            this.pan.Margin = new System.Windows.Forms.Padding(0);
            this.pan.MaximumSize = new System.Drawing.Size(10, 50000);
            this.pan.MinimumSize = new System.Drawing.Size(10, 0);
            this.pan.Name = "pan";
            this.pan.Size = new System.Drawing.Size(10, 383);
            this.pan.TabIndex = 3;
            this.pan.Paint += new System.Windows.Forms.PaintEventHandler(this.pan_Paint);
            this.pan.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pan_MouseClick);
            this.pan.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.pan_MouseDoubleClick);
            this.pan.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pan_MouseDown);
            this.pan.MouseLeave += new System.EventHandler(this.pan_MouseLeave);
            this.pan.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pan_MouseMove);
            this.pan.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pan_MouseUp);
            // 
            // MyBook
            // 
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.web);
            this.Controls.Add(this.pan);
            this.Name = "MyBook";
            this.Size = new System.Drawing.Size(500, 383);
            this.SizeChanged += new System.EventHandler(this.MetroBook_SizeChanged);
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.Panel pan;
        public System.Windows.Forms.WebBrowser web;


    }
}
