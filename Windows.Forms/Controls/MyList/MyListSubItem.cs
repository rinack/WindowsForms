using System;
using System.Collections.Generic;
using System.Text;

using System.Drawing;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Drawing.Imaging;

namespace Windows.Forms.Controls.Forms.MyList
{

    //有待解决
    //[TypeConverter(typeof(ExpandableObjectConverter))]
    public class MyListSubItem : IComparable
    {
        private int id;
        /// <summary>
        /// 获取或者设置用户账号
        /// </summary>
        public int ID {
            get { return id; }
            set { id = value; }
        }


        private string displayName;
        /// <summary>
        /// 获取或者设置用户备注名称
        /// </summary>
        public string Name {
            get { return displayName; }
            set { displayName = value; RedrawSubItem(); }
        }

        private string personalMsg;
        /// <summary>
        /// 获取或者设置用户签名信息
        /// </summary>
        public string Info {
            get { return personalMsg; }
            set { personalMsg = value; RedrawSubItem(); }
        }

        private string url;
        /// <summary>
        /// 获取或者设置用户IP地址
        /// </summary>
        public string Url {
            get { return url; }
            set {
                url = value;
            }
        }

       

       
        private Image headImage;
        /// <summary>
        /// 获取或者设置用户头像
        /// </summary>
        public Image HeadImage {
            get { return headImage; }
            set { headImage = value; RedrawSubItem(); }
        }

      
        private bool isTwinkle;
        /// <summary>
        /// 获取或者设置是否闪动
        /// </summary>
        public bool IsTwinkle {
            get { return isTwinkle; }
            set {
                if (isTwinkle == value) return;
                if (this.ownerListItem == null) return;
                isTwinkle = value;
                if (isTwinkle)
                    this.ownerListItem.TwinkleSubItemNumber++;
                else
                    this.ownerListItem.TwinkleSubItemNumber--;
            }
        }

        private bool isTwinkleHide;
        internal bool IsTwinkleHide {
            get { return isTwinkleHide; }
            set { isTwinkleHide = value; }
        }

        private Rectangle bounds;
        /// <summary>
        /// 获取列表子项显示区域
        /// </summary>
        [Browsable(false)]
        public Rectangle Bounds {
            get { return bounds; }
            internal set { bounds = value; }
        }

        private Rectangle headRect;
        /// <summary>
        /// 获取头像显示区域
        /// </summary>
        [Browsable(false)]
        public Rectangle HeadRect {
            get { return headRect; }
            internal set { headRect = value; }
        }

        private MyListItem ownerListItem;
        /// <summary>
        /// 获取当前列表子项所在的列表项
        /// </summary>
        [Browsable(false)]
        public MyListItem OwnerListItem {
            get { return ownerListItem; }
            internal set { ownerListItem = value; }
        }

        private void RedrawSubItem() {
            if (this.ownerListItem != null)
                if (this.ownerListItem.OwnerChatListBox != null)
                    this.ownerListItem.OwnerChatListBox.Invalidate(this.bounds);
        }
        /// <summary>
        /// 获取当前用户的黑白头像
        /// </summary>
        /// <returns>黑白头像</returns>
        public Bitmap GetDarkImage() {
            Bitmap b = new Bitmap(headImage);
            Bitmap bmp = b.Clone(new Rectangle(0, 0, headImage.Width, headImage.Height), PixelFormat.Format24bppRgb);
            b.Dispose();
            BitmapData bmpData = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.ReadWrite, bmp.PixelFormat);
            byte[] byColorInfo = new byte[bmp.Height * bmpData.Stride];
            Marshal.Copy(bmpData.Scan0, byColorInfo, 0, byColorInfo.Length);
            for (int x = 0, xLen = bmp.Width; x < xLen; x++) {
                for (int y = 0, yLen = bmp.Height; y < yLen; y++) {
                    byColorInfo[y * bmpData.Stride + x * 3] =
                        byColorInfo[y * bmpData.Stride + x * 3 + 1] =
                        byColorInfo[y * bmpData.Stride + x * 3 + 2] =
                        GetAvg(
                        byColorInfo[y * bmpData.Stride + x * 3],
                        byColorInfo[y * bmpData.Stride + x * 3 + 1],
                        byColorInfo[y * bmpData.Stride + x * 3 + 2]);
                }
            }
            Marshal.Copy(byColorInfo, 0, bmpData.Scan0, byColorInfo.Length);
            bmp.UnlockBits(bmpData);
            return bmp;
        }

        private byte GetAvg(byte b, byte g, byte r) {
            return (byte)((r + g + b) / 3);
        }

    
        //实现排序接口
        int IComparable.CompareTo(object obj) {
            if (!(obj is MyListSubItem))
                throw new NotImplementedException("obj is not MyListSubItem");
            MyListSubItem subItem = obj as MyListSubItem;
            return (this.ID).CompareTo(subItem.ID);
        }

        public MyListSubItem() {

            this.displayName = "Item";
            this.personalMsg = "信息";
        }
        
        
        public MyListSubItem(string displayname, string personalmsg) {
            this.displayName = displayname;
            this.personalMsg = personalmsg;
        }
        public MyListSubItem( string displayname, string personalmsg, UserStatus status) {
            this.displayName = displayname;
            this.personalMsg = personalmsg;
           
        }
        public MyListSubItem(int id, string displayname, string personalmsg, UserStatus status, Bitmap head) {
            this.id = id;
            this.displayName = displayname;
            this.personalMsg = personalmsg;
         
            this.headImage = head;
        }
        //在线状态
        public enum UserStatus
        {
            QMe, Online, Away, Busy, DontDisturb, OffLine   //貌似对于列表而言 没有隐身状态
        }
    }
}
