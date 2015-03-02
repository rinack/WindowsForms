using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Threading;


namespace Windows.Forms.Controls.Forms.MyList
{

    public partial class MyList : Control
    {
        public MyList()
        {
            InitializeComponent();
            this.Size = new Size(250,200);
            this.iconSizeMode = MyListItemIcon.SuperSmall1;
            this.items = new MyListItemCollection(this);
            this.BackColor = Color.White;
            this.itemColor = Color.FromArgb(40,Color.White);
            this.subItemColor = Color.FromArgb(40, Color.White);
            this.itemMouseOnColor = Color.LightSkyBlue;
            this.subItemMouseOnColor = Color.LightBlue;
            this.subItemSelectColor = Color.DeepSkyBlue;
            this.arrowColor = Color.DarkGray;
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            chatVScroll = new MyListVScroll(this);
        }

        #region Properties

        private MyListItemIcon iconSizeMode;
        /// <summary>
        /// 获取或者设置列表的图标模式
        /// </summary>
        [DefaultValue(MyListItemIcon.SuperSmall6)]
        public MyListItemIcon IconSizeMode {
            get { return iconSizeMode; }
            set {
                if (iconSizeMode == value) return;
                iconSizeMode = value;
                this.Invalidate();
            }
        }

        private bool isShowImage;
        public bool IsShowImage
        {
            get { return isShowImage; }
            set
            {
                isShowImage = value;
            }
        }


        private MyListItemCollection items;
        /// <summary>
        /// 获取列表中所有列表项的集合
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public MyListItemCollection Items {
            get {
                if (items == null)
                    items = new MyListItemCollection(this);
                return items;
            }
        }

        private MyListSubItem selectSubItem;
        /// <summary>
        /// 当前选中的子项
        /// </summary>
        [Browsable(false)]
        public MyListSubItem SelectSubItem {
            get { return selectSubItem; }
        }
        /// <summary>
        /// 获取或者设置滚动条背景色
        /// </summary>
        [DefaultValue(typeof(Color), "LightYellow"), Category("ControlColor")]
        [Description("滚动条的背景颜色")]
        public Color ScrollBackColor {
            get { return chatVScroll.BackColor; }
            set { chatVScroll.BackColor = value; }
        }
        /// <summary>
        /// 获取或者设置滚动条滑块默认颜色
        /// </summary>
        [DefaultValue(typeof(Color), "Gray"), Category("ControlColor")]
        [Description("滚动条滑块默认情况下的颜色")]
        public Color ScrollSliderDefaultColor {
            get { return chatVScroll.SliderDefaultColor; }
            set { chatVScroll.SliderDefaultColor = value; }
        }
        /// <summary>
        /// 获取或者设置滚动条点下的颜色
        /// </summary>
        [DefaultValue(typeof(Color), "DarkGray"), Category("ControlColor")]
        [Description("滚动条滑块被点击或者鼠标移动到上面时候的颜色")]
        public Color ScrollSliderDownColor {
            get { return chatVScroll.SliderDownColor; }
            set { chatVScroll.SliderDownColor = value; }
        }
        /// <summary>
        /// 获取或者设置滚动条箭头的背景色
        /// </summary>
        [DefaultValue(typeof(Color), "Gray"), Category("ControlColor")]
        [Description("滚动条箭头的背景颜色")]
        public Color ScrollArrowBackColor {
            get { return chatVScroll.ArrowBackColor; }
            set { chatVScroll.ArrowBackColor = value; }
        }
        /// <summary>
        /// 获取或者设置滚动条的箭头颜色
        /// </summary>
        [DefaultValue(typeof(Color), "White"), Category("ControlColor")]
        [Description("滚动条箭头的颜色")]
        public Color ScrollArrowColor {
            get { return chatVScroll.ArrowColor; }
            set { chatVScroll.ArrowColor = value; }
        }

        private Color arrowColor;
        /// <summary>
        /// 获取或者设置列表项箭头的颜色
        /// </summary>
        [DefaultValue(typeof(Color), "DarkGray"), Category("ControlColor")]
        [Description("列表项上面的箭头的颜色")]
        public Color ArrowColor {
            get { return arrowColor; }
            set {
                if (arrowColor == value) return;
                arrowColor = value;
                this.Invalidate();
            }
        }

        private Color itemColor;
        /// <summary>
        /// 获取或者设置列表项背景色
        /// </summary>
        [DefaultValue(typeof(Color), "White"), Category("ControlColor")]
        [Description("列表项的背景色")]
        public Color ItemColor {
            get { return itemColor; }
            set {
                if (itemColor == value) return;
                itemColor = value;
            }
        }

        private Color subItemColor;
        /// <summary>
        /// 获取或者设置子项的背景色
        /// </summary>
        [DefaultValue(typeof(Color), "White"), Category("ControlColor")]
        [Description("列表子项的背景色")]
        public Color SubItemColor {
            get { return subItemColor; }
            set {
                if (subItemColor == value) return;
                subItemColor = value;
            }
        }

        private Color itemMouseOnColor;
        /// <summary>
        /// 获取或者设置当鼠标移动到列表项的颜色
        /// </summary>
        [DefaultValue(typeof(Color), "LightYellow"), Category("ControlColor")]
        [Description("当鼠标移动到列表项上面的颜色")]
        public Color ItemMouseOnColor {
            get { return itemMouseOnColor; }
            set { itemMouseOnColor = value; }
        }

        private Color subItemMouseOnColor;
        /// <summary>
        /// 获取或者设置当鼠标移动到子项的颜色
        /// </summary>
        [DefaultValue(typeof(Color), "LightBlue"), Category("ControlColor")]
        [Description("当鼠标移动到子项上面的颜色")]
        public Color SubItemMouseOnColor {
            get { return subItemMouseOnColor; }
            set { subItemMouseOnColor = value; }
        }

        private Color subItemSelectColor;
        /// <summary>
        /// 获取或者设置选中的子项的颜色
        /// </summary>
        [DefaultValue(typeof(Color), "Wheat"), Category("ControlColor")]
        [Description("当列表子项被选中时候的颜色")]
        public Color SubItemSelectColor {
            get { return subItemSelectColor; }
            set { subItemSelectColor = value; }
        }


        //private bool _VScroll;
        ///// <summary>
        ///// 获取或者设置选中的子项的颜色
        ///// </summary>
        //[DefaultValue(typeof(bool), "true"), Category("VScroll")]
        //[Description("显示滚动条")]
        //public bool VScroll
        //{
        //    get { return _VScroll;  }
        //    set { _VScroll = value; }
        //}


        #endregion

        #region Events

        public delegate void ChatListEventHandler(object sender, MyListEventArgs e);
        public event ChatListEventHandler DoubleClickSubItem;
        public event ChatListEventHandler MouseEnterHead;
        public event ChatListEventHandler MouseLeaveHead;

        protected virtual void OnDoubleClickSubItem(MyListEventArgs e) {
            if (this.DoubleClickSubItem != null)
                DoubleClickSubItem(this, e);
        }

        protected virtual void OnMouseEnterHead(MyListEventArgs e) {
            if (this.MouseEnterHead != null)
                MouseEnterHead(this, e);
        }

        protected virtual void OnMouseLeaveHead(MyListEventArgs e) {
            if (this.MouseLeaveHead != null)
                MouseLeaveHead(this, e);
        }

        #endregion

        private Point m_ptMousePos;             //鼠标的位置
        private MyListVScroll chatVScroll;    //滚动条

        private MyListItem m_mouseOnItem;
        private bool m_bOnMouseEnterHeaded;     //确定用户绑定事件是否被触发
        private MyListSubItem m_mouseOnSubItem;

        protected override void OnCreateControl() {
            Thread threadInvalidate = new Thread(new ThreadStart(() => {
                Rectangle rectReDraw = new Rectangle(0, 0, this.Width, this.Height);
                
                while (true) {          //后台检测要闪动的图标然后重绘
                    for (int i = 0, lenI = this.items.Count; i < lenI; i++) {
                        
                            for (int j = 0, lenJ = items[i].SubItems.Count; j < lenJ; j++) {
                                if (items[i].SubItems[j].IsTwinkle) {
                                    items[i].SubItems[j].IsTwinkleHide = !items[i].SubItems[j].IsTwinkleHide;
                                    rectReDraw.Y = items[i].SubItems[j].Bounds.Y - chatVScroll.Value;
                                    rectReDraw.Height = items[i].SubItems[j].Bounds.Height;
                                    this.Invalidate(rectReDraw);
                                }
                            }
                    }
                    Thread.Sleep(500);
                }
            }));
            threadInvalidate.IsBackground = true;
            threadInvalidate.Start();
            base.OnCreateControl();
        }

        protected override void OnPaint(PaintEventArgs e) {
            Graphics g = e.Graphics;
            g.TranslateTransform(0, -chatVScroll.Value);        //根据滚动条的值设置坐标偏移
            Rectangle rectItem = new Rectangle(0, 1, this.Width, 0);                       //列表项区域
            Rectangle rectSubItem = new Rectangle(0, 26, this.Width, (int)iconSizeMode);    //子项区域
            SolidBrush sb = new SolidBrush(this.itemColor);
            if (items.Count<1)
            {

            }
            else
            {
                try
                {
                    for (int i = 0, lenItem = items.Count; i < 1; i++)
                    {
                        DrawItem(g, items[i], rectItem, sb);        //绘制列表项
                                            //如果列表项展开绘制子项
                            rectSubItem.Y = rectItem.Bottom + 1;
                            for (int j = 0, lenSubItem = items[i].SubItems.Count; j < lenSubItem; j++)
                            {
                                DrawSubItem(g, items[i].SubItems[j], ref rectSubItem, sb);  //绘制子项
                                rectSubItem.Y = rectSubItem.Bottom + 1;             //计算下一个子项的区域
                                rectSubItem.Height = (int)iconSizeMode;
                            }
                            rectItem.Height = rectSubItem.Bottom - rectItem.Top - (int)iconSizeMode - 1;
                        
                        items[i].Bounds = new Rectangle(rectItem.Location, rectItem.Size);
                        rectItem.Y = rectItem.Bottom + 1;           //计算下一个列表项区域
                        rectItem.Height = 26;
                    }
                    g.ResetTransform();             //重置坐标系
                    chatVScroll.VirtualHeight = rectItem.Bottom - 26;   //绘制完成计算虚拟高度决定是否绘制滚动条
                    if (chatVScroll.ShouldBeDraw)   //是否绘制滚动条
                        chatVScroll.ReDrawScroll(g);
                }
                finally { sb.Dispose(); }
            }
            base.OnPaint(e);
        }

        protected override void OnMouseWheel(MouseEventArgs e) {
            if (e.Delta > 0) chatVScroll.Value -= 50;
            if (e.Delta < 0) chatVScroll.Value += 50;
            base.OnMouseWheel(e);
        }

        protected override void OnMouseDown(MouseEventArgs e) {
            if (e.Button == MouseButtons.Left) {        //如果左键在滚动条滑块上点击
                if (chatVScroll.SliderBounds.Contains(e.Location)) {
                    chatVScroll.IsMouseDown = true;
                    chatVScroll.MouseDownY = e.Y;
                }
            }
            this.Focus();
            base.OnMouseDown(e);
        }

        protected override void OnMouseUp(MouseEventArgs e) {
            if (e.Button == MouseButtons.Left)
                chatVScroll.IsMouseDown = false;
            base.OnMouseUp(e);
        }

        protected override void OnMouseMove(MouseEventArgs e) {
            m_ptMousePos = e.Location;
            if (chatVScroll.IsMouseDown) {          //如果滚动条的滑块处于被点击 那么移动
                chatVScroll.MoveSliderFromLocation(e.Y);
                return;
            }
            if (chatVScroll.ShouldBeDraw) {         //如果控件上有滚动条 判断鼠标是否在滚动条区域移动
                if (chatVScroll.Bounds.Contains(m_ptMousePos)) {
                    ClearItemMouseOn();
                    ClearSubItemMouseOn();
                    if (chatVScroll.SliderBounds.Contains(m_ptMousePos))
                        chatVScroll.IsMouseOnSlider = true;
                    else
                        chatVScroll.IsMouseOnSlider = false;
                    if (chatVScroll.UpBounds.Contains(m_ptMousePos))
                        chatVScroll.IsMouseOnUp = true;
                    else
                        chatVScroll.IsMouseOnUp = false;
                    if (chatVScroll.DownBounds.Contains(m_ptMousePos))
                        chatVScroll.IsMouseOnDown = true;
                    else
                        chatVScroll.IsMouseOnDown = false;
                    return;
                } else
                    chatVScroll.ClearAllMouseOn();
            }
            m_ptMousePos.Y += chatVScroll.Value;        //如果不在滚动条范围类 那么根据滚动条当前值计算虚拟的一个坐标
            for (int i = 0, Len = items.Count; i < Len; i++) {      //然后判断鼠标是否移动到某一列表项或者子项
                if (items[i].Bounds.Contains(m_ptMousePos)) {
                                 //如果展开 判断鼠标是否在某一子项上面
                        for (int j = 0, lenSubItem = items[i].SubItems.Count; j < lenSubItem; j++) {
                            if (items[i].SubItems[j].Bounds.Contains(m_ptMousePos)) {
                                if (m_mouseOnSubItem != null) {             //如果当前鼠标下子项不为空
                                    if (items[i].SubItems[j].HeadRect.Contains(m_ptMousePos)) {     //判断鼠标是否在头像内
                                        if (!m_bOnMouseEnterHeaded) {       //如果没有触发进入事件 那么触发用户绑定的事件
                                            OnMouseEnterHead(new MyListEventArgs(this.m_mouseOnSubItem, this.selectSubItem));
                                            m_bOnMouseEnterHeaded = true;
                                        }
                                    } else {
                                        if (m_bOnMouseEnterHeaded) {        //如果已经执行过进入事件 那触发用户绑定的离开事件
                                            OnMouseLeaveHead(new MyListEventArgs(this.m_mouseOnSubItem, this.selectSubItem));
                                            m_bOnMouseEnterHeaded = false;
                                        }
                                    }
                                }
                                if (items[i].SubItems[j].Equals(m_mouseOnSubItem)) {
                                    return;
                                }
                                ClearSubItemMouseOn();
                                ClearItemMouseOn();
                                m_mouseOnSubItem = items[i].SubItems[j];
                                this.Invalidate(new Rectangle(
                                    m_mouseOnSubItem.Bounds.X, m_mouseOnSubItem.Bounds.Y - chatVScroll.Value,
                                    m_mouseOnSubItem.Bounds.Width, m_mouseOnSubItem.Bounds.Height));
                                //this.Invalidate();
                                return;
                            }
                        }
                        ClearSubItemMouseOn();      //循环做完没发现子项 那么判断是否在列表上面
                        if (new Rectangle(0, items[i].Bounds.Top - chatVScroll.Value, this.Width, 20).Contains(e.Location)) {
                            if (items[i].Equals(m_mouseOnItem))
                                return;
                            ClearItemMouseOn();
                            m_mouseOnItem = items[i];
                            this.Invalidate(new Rectangle(
                                m_mouseOnItem.Bounds.X, m_mouseOnItem.Bounds.Y - chatVScroll.Value,
                                m_mouseOnItem.Bounds.Width, m_mouseOnItem.Bounds.Height));
                            //this.Invalidate();
                            return;
                        }
                    
                }
            }//若循环结束 既不在列表上也不再子项上 清空上面的颜色
            ClearItemMouseOn();
            ClearSubItemMouseOn();
            base.OnMouseMove(e);
        }

        protected override void OnMouseLeave(EventArgs e) {
            ClearItemMouseOn();
            ClearSubItemMouseOn();
            chatVScroll.ClearAllMouseOn();
            if (m_bOnMouseEnterHeaded) {        //如果已经执行过进入事件 那触发用户绑定的离开事件
                OnMouseLeaveHead(new MyListEventArgs(this.m_mouseOnSubItem, this.selectSubItem));
                m_bOnMouseEnterHeaded = false;
            }
            base.OnMouseLeave(e);
        }

        protected override void OnClick(EventArgs e) {
            if (chatVScroll.IsMouseDown) return;    //MouseUp事件触发在Click后 滚动条滑块为点下状态 单击无效
            if (chatVScroll.ShouldBeDraw) {         //如果有滚动条 判断是否在滚动条类点击
                if (chatVScroll.Bounds.Contains(m_ptMousePos)) {        //判断在滚动条那个位置点击
                    if (chatVScroll.UpBounds.Contains(m_ptMousePos))
                        chatVScroll.Value -= 50;
                    else if (chatVScroll.DownBounds.Contains(m_ptMousePos))
                        chatVScroll.Value += 50;
                    else if (!chatVScroll.SliderBounds.Contains(m_ptMousePos))
                        chatVScroll.MoveSliderToLocation(m_ptMousePos.Y);
                    return;
                }
            }//否则 如果在列表上点击 展开或者关闭 在子项上面点击则选中
            foreach (MyListItem item in items) {
                if (item.Bounds.Contains(m_ptMousePos)) {
                    
                        foreach (MyListSubItem subItem in item.SubItems) {
                            if (subItem.Bounds.Contains(m_ptMousePos)) {
                                if (subItem.Equals(selectSubItem))
                                    return;
                                selectSubItem = subItem;
                                this.Invalidate();
                                return;
                            }
                        }
                        if (new Rectangle(0, item.Bounds.Top, this.Width, 20).Contains(m_ptMousePos)) {
                            selectSubItem = null;
                           
                            this.Invalidate();
                            return;
                        }
                    
                   
                }
            }
            base.OnClick(e);
        }

        protected override void OnDoubleClick(EventArgs e) {
            this.OnClick(e);        //双击时 再次触发一下单击事件  不然双击列表项 相当于只点击了一下列表项
            if (chatVScroll.Bounds.Contains(PointToClient(MousePosition))) return;  //如果双击在滚动条上返回
            if (this.selectSubItem != null)     //如果选中项不为空 那么触发用户绑定的双击事件
                OnDoubleClickSubItem(new MyListEventArgs(this.m_mouseOnSubItem, this.selectSubItem));
            base.OnDoubleClick(e);
        }
        /// <summary>
        /// 绘制列表项
        /// </summary>
        /// <param name="g">绘图表面</param>
        /// <param name="item">要绘制的列表项</param>
        /// <param name="rectItem">该列表项的区域</param>
        /// <param name="sb">画刷</param>
        protected virtual void DrawItem(Graphics g, MyListItem item, Rectangle rectItem, SolidBrush sb) {
            StringFormat sf = new StringFormat();
            sf.LineAlignment = StringAlignment.Center;
            sf.SetTabStops(0.0F, new float[] { 20.0F });
            if (item.Equals(m_mouseOnItem))
            {        //根据列表项现在的状态绘制相应的背景色
                sb.Color = this.itemMouseOnColor;

            }
            else
            {
                sb.Color = this.itemColor;
            }
               
            g.FillRectangle(sb, rectItem);
            
            
        }
        /// <summary>
        /// 绘制列表子项
        /// </summary>
        /// <param name="g">绘图表面</param>
        /// <param name="subItem">要绘制的子项</param>
        /// <param name="rectSubItem">该子项的区域</param>
        /// <param name="sb">画刷</param>
        protected virtual void DrawSubItem(Graphics g, MyListSubItem subItem, ref Rectangle rectSubItem, SolidBrush sb) {

            //if (subItem.Equals(selectSubItem)) {        //判断改子项是否被选中
            //    rectSubItem.Height = (int)MyListItemIcon.Large;
               
            //    //如果选中则绘制成大图标
            //    sb.Color = this.subItemSelectColor;
            //    g.FillRectangle(sb, rectSubItem);
            //    if (IsShowImage)
            //    {
            //        DrawHeadImage(g, subItem, rectSubItem);         //绘制头像
            //    }
            //    DrawLargeSubItem(g, subItem, rectSubItem);      //绘制大图标 显示的个人信息
            //    subItem.Bounds = new Rectangle(rectSubItem.Location, rectSubItem.Size);
            //    return;
            //}
            //else 
            if (subItem.Equals(m_mouseOnSubItem))
            {
                sb.Color = this.subItemMouseOnColor;
                g.FillRectangle(sb, rectSubItem);
                if (IsShowImage)
                {
                    DrawHeadImage(g, subItem, rectSubItem);         //绘制头像
                }
            }
            else
            {
                sb.Color = this.subItemColor;
                g.FillRectangle(sb, rectSubItem);
                if (IsShowImage)
                {
                    DrawHeadImage(g, subItem, rectSubItem);         //绘制头像
                }
            }



            //if (iconSizeMode == MyListItemIcon.Large) //没有选中则根据 图标模式绘制
            DrawLargeSubItem(g, subItem, rectSubItem);
            
            subItem.Bounds = new Rectangle(rectSubItem.Location, rectSubItem.Size);
        }
        /// <summary>
        /// 绘制列表子项的头像
        /// </summary>
        /// <param name="g">绘图表面</param>
        /// <param name="subItem">要绘制头像的子项</param>
        /// <param name="rectSubItem">该子项的区域</param>
        protected virtual void DrawHeadImage(Graphics g, MyListSubItem subItem, Rectangle rectSubItem) {
            if (subItem.IsTwinkle) {        //判断改头像是否闪动
                if (subItem.IsTwinkleHide)  //同理该值 在线程中 取反赋值
                    return;
            }

            int imageHeight = rectSubItem.Height - 10;      //根据子项的大小计算头像的区域
            subItem.HeadRect = new Rectangle(5, rectSubItem.Top + 5, imageHeight, imageHeight);

            if (subItem.HeadImage == null)                 //如果头像位空 用默认资源给定的头像
                subItem.HeadImage = AssemblyHelper.GetImage("StanForm.ListBox.mountaintop50.png");
            
                g.DrawImage(subItem.HeadImage, subItem.HeadRect);       //如果在线根据在想状态绘制小图标
             

            if (subItem.Equals(selectSubItem))              //根据是否选中头像绘制头像的外边框
                g.DrawRectangle(Pens.Cyan, subItem.HeadRect);
            else
                g.DrawRectangle(Pens.Gray, subItem.HeadRect);
        }
        Font ft2 = new System.Drawing.Font("微软雅黑", 8F);
        Font ft = new System.Drawing.Font("微软雅黑", 12F);
        
        /// <summary>
        /// 绘制大图标模式的个人信息
        /// </summary>
        /// <param name="g">绘图表面</param>
        /// <param name="subItem">要绘制信息的子项</param>
        /// <param name="rectSubItem">该子项的区域</param>
        protected virtual void DrawLargeSubItem(Graphics g, MyListSubItem subItem, Rectangle rectSubItem) {
            rectSubItem.Height = (int)iconSizeMode;       //重新赋值一个高度
            rectSubItem.Width = (int)iconSizeMode;
            string strDraw = subItem.Name;
            Size szFont = TextRenderer.MeasureText(strDraw, this.Font);
            SolidBrush b = new SolidBrush(ForeColor);
            if (szFont.Width > 0) {             //判断是否有备注名称
                if (!IsShowImage)//没有头像
                {
                    g.DrawString(strDraw, ft, b,5, rectSubItem.Top+8);
                }
                else
                {
                    g.DrawString(strDraw, ft, b, rectSubItem.Height + 15, rectSubItem.Top+8);
                }
            }
            if (!IsShowImage)//绘制个人签名
            {
                g.DrawString(subItem.Info, ft2, b, 5, rectSubItem.Top + 35);
            }
            else
            {
                g.DrawString(subItem.Info, ft2, b, rectSubItem.Height + 15, rectSubItem.Top + 35);
            }
            //
           
        }
      

        private void ClearItemMouseOn() {
            if (m_mouseOnItem != null) {
                this.Invalidate(new Rectangle(
                    m_mouseOnItem.Bounds.X, m_mouseOnItem.Bounds.Y - chatVScroll.Value,
                    m_mouseOnItem.Bounds.Width, m_mouseOnItem.Bounds.Height));
                m_mouseOnItem = null;
            }
        }

        private void ClearSubItemMouseOn() {
            if (m_mouseOnSubItem != null) {
                this.Invalidate(new Rectangle(
                    m_mouseOnSubItem.Bounds.X, m_mouseOnSubItem.Bounds.Y - chatVScroll.Value,
                    m_mouseOnSubItem.Bounds.Width, m_mouseOnSubItem.Bounds.Height));
                m_mouseOnSubItem = null;
            }
        }
        /// <summary>
        /// 根据id返回一组列表子项
        /// </summary>
        /// <param name="userId">要返回的id</param>
        /// <returns>列表子项的数组</returns>
        public MyListSubItem[] GetSubItemsById(int userId) {
            List<MyListSubItem> subItems = new List<MyListSubItem>();
            for (int i = 0, lenI = this.items.Count; i < lenI; i++) {
                for (int j = 0, lenJ = items[i].SubItems.Count; j < lenJ; j++) {
                    if (userId == items[i].SubItems[j].ID)
                        subItems.Add(items[i].SubItems[j]);
                }
            }
            return subItems.ToArray();
        }
        /// <summary>
        /// 根据昵称返回一组列表子项
        /// </summary>
        /// <param name="nicName">要返回的昵称</param>
        /// <returns>列表子项的数组</returns>
        public MyListSubItem[] GetSubItemsByNicName(string nicName) {
            List<MyListSubItem> subItems = new List<MyListSubItem>();
            for (int i = 0, lenI = this.items.Count; i < lenI; i++) {
                for (int j = 0, lenJ = items[i].SubItems.Count; j < lenJ; j++) {
                    if (nicName == items[i].SubItems[j].Name)
                        subItems.Add(items[i].SubItems[j]);
                }
            }
            return subItems.ToArray();
        }
        /// <summary>
        /// 根据备注名称返回一组列表子项
        /// </summary>
        /// <param name="displayName">要返回的备注名称</param>
        /// <returns>列表子项的数组</returns>
        public MyListSubItem[] GetSubItemsByDisplayName(string displayName) {
            List<MyListSubItem> subItems = new List<MyListSubItem>();
            for (int i = 0, lenI = this.items.Count; i < lenI; i++) {
                for (int j = 0, lenJ = items[i].SubItems.Count; j < lenJ; j++) {
                    if (displayName == items[i].SubItems[j].Name)
                        subItems.Add(items[i].SubItems[j]);
                }
            }
            return subItems.ToArray();
        }
        //自定义列表项集合
        public class MyListItemCollection : IList, ICollection, IEnumerable
        {
            private int count;      //元素个数
            public int Count { get { return count; } }
            private MyListItem[] m_arrItem;
            private MyList owner;  //所属的控件

            public MyListItemCollection(MyList owner) { this.owner = owner; }
            //确认存储空间
            private void EnsureSpace(int elements) {
                if (m_arrItem == null)
                    m_arrItem = new MyListItem[Math.Max(elements, 4)];
                else if (this.count + elements > m_arrItem.Length) {
                    MyListItem[] arrTemp = new MyListItem[Math.Max(this.count + elements, m_arrItem.Length * 2)];
                    m_arrItem.CopyTo(arrTemp, 0);
                    m_arrItem = arrTemp;
                }
            }
            /// <summary>
            /// 获取列表项所在的索引位置
            /// </summary>
            /// <param name="item">要获取的列表项</param>
            /// <returns>索引位置</returns>
            public int IndexOf(MyListItem item) {
                return Array.IndexOf<MyListItem>(m_arrItem, item);
            }
            /// <summary>
            /// 添加一个列表项
            /// </summary>
            /// <param name="item">要添加的列表项</param>
            public void Add(MyListItem item) {
                if (this.count==1)
                {
                    return;
                     //throw new ArgumentNullException("只允许存在一个项","错误");
                }
                if (item == null)       //空引用不添加
                    throw new ArgumentNullException("Item cannot be null");
                this.EnsureSpace(1);
                if (-1 == this.IndexOf(item)) {         //进制添加重复对象
                    item.OwnerChatListBox = this.owner;
                    m_arrItem[this.count++] = item;
                    this.owner.Invalidate();            //添加进去是 进行重绘
                }
            }
            /// <summary>
            /// 添加一个列表项的数组
            /// </summary>
            /// <param name="items">要添加的列表项的数组</param>
            public void AddRange(MyListItem[] items) {
                if (items == null)
                    throw new ArgumentNullException("Items cannot be null");
                this.EnsureSpace(items.Length);
                try {
                    foreach (MyListItem item in items) {
                        if (item == null)
                            throw new ArgumentNullException("Item cannot be null");
                        if (-1 == this.IndexOf(item)) {     //重复数据不添加
                            item.OwnerChatListBox = this.owner;
                            m_arrItem[this.count++] = item;
                        }
                    }
                } finally {
                    this.owner.Invalidate();
                }
            }
            /// <summary>
            /// 移除一个列表项
            /// </summary>
            /// <param name="item">要移除的列表项</param>
            public void Remove(MyListItem item) {
                int index = this.IndexOf(item);
                if (index==0)
                {
                    return;
                }
                if (-1 != index)        //如果存在元素 那么根据索引移除
                    this.RemoveAt(index);

            }
            /// <summary>
            /// 根据索引位置删除一个列表项
            /// </summary>
            /// <param name="index">索引位置</param>
            public void RemoveAt(int index) {
                if (index < 0 || index >= this.count)
                    throw new IndexOutOfRangeException("Index was outside the bounds of the array");
                this.count--;
                for (int i = index, Len = this.count; i < Len; i++)
                    m_arrItem[i] = m_arrItem[i + 1];
                this.owner.Invalidate();
            }
            /// <summary>
            /// 清空所有列表项
            /// </summary>
            public void Clear() {
                this.count = 0;
                m_arrItem = null;
                this.owner.Invalidate();
            }
            /// <summary>
            /// 根据索引位置插入一个列表项
            /// </summary>
            /// <param name="index">索引位置</param>
            /// <param name="item">要插入的列表项</param>
            public void Insert(int index, MyListItem item) {
                if (index < 0 || index >= this.count)
                    throw new IndexOutOfRangeException("Index was outside the bounds of the array");
                if (item == null)
                    throw new ArgumentNullException("Item cannot be null");
                this.EnsureSpace(1);
                for (int i = this.count; i > index; i--)
                    m_arrItem[i] = m_arrItem[i - 1];
                item.OwnerChatListBox = this.owner;
                m_arrItem[index] = item;
                this.count++;
                this.owner.Invalidate();
            }
            /// <summary>
            /// 判断一个列表项是否在集合内
            /// </summary>
            /// <param name="item">要判断的列表项</param>
            /// <returns>是否在列表项</returns>
            public bool Contains(MyListItem item) {
                return this.IndexOf(item) != -1;
            }
            /// <summary>
            /// 将列表项的集合拷贝至一个数组
            /// </summary>
            /// <param name="array">目标数组</param>
            /// <param name="index">拷贝的索引位置</param>
            public void CopyTo(Array array, int index) {
                if (array == null)
                    throw new ArgumentNullException("array cannot be null");
                m_arrItem.CopyTo(array, index);
            }
            /// <summary>
            /// 根据索引获取一个列表项
            /// </summary>
            /// <param name="index">索引位置</param>
            /// <returns>列表项</returns>
            public MyListItem this[int index] {
                get {
                    if (index < 0 || index >= this.count)
                        throw new IndexOutOfRangeException("Index was outside the bounds of the array");
                    return m_arrItem[index];
                }
                set {
                    if (index < 0 || index >= this.count)
                        throw new IndexOutOfRangeException("Index was outside the bounds of the array");
                    m_arrItem[index] = value;
                }
            }
            //实现接口
            int IList.Add(object value) {
                if (!(value is MyListItem))
                    throw new ArgumentException("Value cannot convert to ListItem");
                this.Add((MyListItem)value);
                return this.IndexOf((MyListItem)value);
            }

            void IList.Clear() {
                this.Clear();
            }

            bool IList.Contains(object value) {
                if (!(value is MyListItem))
                    throw new ArgumentException("Value cannot convert to ListItem");
                return this.Contains((MyListItem)value);
            }

            int IList.IndexOf(object value) {
                if (!(value is MyListItem))
                    throw new ArgumentException("Value cannot convert to ListItem");
                return this.IndexOf((MyListItem)value);
            }

            void IList.Insert(int index, object value) {
                if (!(value is MyListItem))
                    throw new ArgumentException("Value cannot convert to ListItem");
                this.Insert(index, (MyListItem)value);
            }

            bool IList.IsFixedSize {
                get { return false; }
            }

            bool IList.IsReadOnly {
                get { return false; }
            }

            void IList.Remove(object value) {
                if (!(value is MyListItem))
                    throw new ArgumentException("Value cannot convert to ListItem");
                this.Remove((MyListItem)value);
            }

            void IList.RemoveAt(int index) {
                this.RemoveAt(index);
            }

            object IList.this[int index] {
                get { return this[index]; }
                set {
                    if (!(value is MyListItem))
                        throw new ArgumentException("Value cannot convert to ListItem");
                    this[index] = (MyListItem)value;
                }
            }

            void ICollection.CopyTo(Array array, int index) {
                this.CopyTo(array, index);
            }

            int ICollection.Count {
                get { return this.count; }
            }

            bool ICollection.IsSynchronized {
                get { return true; }
            }

            object ICollection.SyncRoot {
                get { return this; }
            }

            IEnumerator IEnumerable.GetEnumerator() {
                for (int i = 0, Len = this.count; i < Len; i++)
                    yield return m_arrItem[i];
            }
        }
    }
}
