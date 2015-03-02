﻿using System;
using System.Collections.Generic;
using System.Text;

using System.Drawing;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Drawing.Imaging;

namespace Windows.Forms.Controls.Forms.MyListBox
{

    //有待解决
    //[TypeConverter(typeof(ExpandableObjectConverter))]
    public class MyListBoxSubItem : IComparable
    {
        private int id;
        /// <summary>
        /// 获取或者设置用户账号
        /// </summary>
        public int ID {
            get { return id; }
            set { id = value; }
        }

        private string nicName;
        /// <summary>
        /// 获取或者设置用户昵称
        /// </summary>
        public string NicName {
            get { return nicName; }
            set { 
                nicName = value;
                RedrawSubItem(); 
            }
        }

        private string displayName;
        /// <summary>
        /// 获取或者设置用户备注名称
        /// </summary>
        public string DisplayName {
            get { return displayName; }
            set { displayName = value; RedrawSubItem(); }
        }

        private string personalMsg;
        /// <summary>
        /// 获取或者设置用户签名信息
        /// </summary>
        public string PersonalMsg {
            get { return personalMsg; }
            set { personalMsg = value; RedrawSubItem(); }
        }

        private string ipAddress;
        /// <summary>
        /// 获取或者设置用户IP地址
        /// </summary>
        public string IpAddress {
            get { return ipAddress; }
            set {
               ipAddress = value;
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

        private UserStatus status;
        /// <summary>
        /// 获取或者设置用户当前状态
        /// </summary>
        public UserStatus Status {
            get { return status; }
            set {
                if (status == value) return;
                status = value;
                if (this.ownerListItem != null)
                    this.ownerListItem.SubItems.Sort();
            }
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

        private MyListBoxItem ownerListItem;
        /// <summary>
        /// 获取当前列表子项所在的列表项
        /// </summary>
        [Browsable(false)]
        public MyListBoxItem OwnerListItem {
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
            if (!(obj is MyListBoxSubItem))
                throw new NotImplementedException("obj is not MyListBoxSubItem");
            MyListBoxSubItem subItem = obj as MyListBoxSubItem;
            return (this.status).CompareTo(subItem.status);
        }

        public MyListBoxSubItem() { 
            this.status = UserStatus.Online;
            this.displayName = "名称";
            this.nicName = "昵称";
            this.personalMsg = "心情&消息";
        }
        public MyListBoxSubItem(string nicname) {
            this.nicName = nicname;
        }
        public MyListBoxSubItem(string nicname, UserStatus status) {
            this.nicName = nicname;
            this.status = status;
        }
        public MyListBoxSubItem(string nicname, string displayname, string personalmsg) {
            this.nicName = nicname;
            this.displayName = displayname;
            this.personalMsg = personalmsg;
        }
        public MyListBoxSubItem(string nicname, string displayname, string personalmsg, UserStatus status) {
            this.nicName = nicname;
            this.displayName = displayname;
            this.personalMsg = personalmsg;
            this.status = status;
        }
        public MyListBoxSubItem(int id, string nicname, string displayname, string personalmsg, UserStatus status, Bitmap head) {
            this.id = id;
            this.nicName = nicname;
            this.displayName = displayname;
            this.personalMsg = personalmsg;
            this.status = status;
            this.headImage = head;
        }
        //在线状态
        public enum UserStatus
        {
            QMe, Online, Away, Busy, DontDisturb, OffLine   //貌似对于列表而言 没有隐身状态
        }
    }
}