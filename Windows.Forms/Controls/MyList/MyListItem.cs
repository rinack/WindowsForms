using System;
using System.Collections.Generic;
using System.Text;

using System.Collections;
using System.Drawing;
using System.ComponentModel;

namespace Windows.Forms.Controls.MyList
{
    //TypeConverter未解决
    //[DefaultProperty("Text"),TypeConverter(typeof(MyListItemConverter))]

    public class MyListItem
    {
        private string text = "Item";
        /// <summary>
        /// 获取或者设置列表项的显示文本
        /// </summary>
        //[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string Text {
            get { return text; }
            set {
                text = value;
                if (this.ownerChatListBox != null)
                    this.ownerChatListBox.Invalidate(this.bounds);
            }
        }



        private int twinkleSubItemNumber;
        /// <summary>
        /// 当前列表项下面闪烁图标的个数
        /// </summary>
        [Browsable(false)]
        public int TwinkleSubItemNumber {
            get { return twinkleSubItemNumber; }
            internal set { twinkleSubItemNumber = value; }
        }

        private bool isTwinkleHide;
        internal bool IsTwinkleHide {
            get { return isTwinkleHide; }
            set { isTwinkleHide = value; }
        }

        private Rectangle bounds;
        /// <summary>
        /// 获取列表项的显示区域
        /// </summary>
        [Browsable(false)]
        public Rectangle Bounds {
            get { return bounds; }
            internal set { bounds = value; }
        }

        private MyList ownerChatListBox;
        /// <summary>
        /// 获取列表项所在的控件
        /// </summary>
        [Browsable(false)]
        public MyList OwnerChatListBox
        {
            get { return ownerChatListBox; }
            internal set { ownerChatListBox = value; }
        }

        private MyListSubItemCollection subItems;
        /// <summary>
        /// 获取当前列表项所有子项的集合
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public MyListSubItemCollection SubItems {
            get {
                if (subItems == null)
                    subItems = new MyListSubItemCollection(this);
                return subItems;
            }
        }

        public MyListItem() { if (this.text == null) this.text = string.Empty; }
        public MyListItem(string text) { this.text = text; }
       
        public MyListItem(MyListSubItem[] subItems) {
            this.subItems.AddRange(subItems);
        }
        public MyListItem(string text, MyListSubItem[] subItems) {
            this.text = text;
            this.subItems.AddRange(subItems);
        }
        
        //自定义列表子项的集合 注释同 自定义列表项的集合
        public class MyListSubItemCollection : IList, ICollection, IEnumerable
        {
            private int count;
            public int Count { get { return count; } }
            private MyListSubItem[] m_arrSubItems;
            private MyListItem owner;

            public MyListSubItemCollection(MyListItem owner) { this.owner = owner; }
            /// <summary>
            /// 对列表进行排序
            /// </summary>
            public void Sort() {
                Array.Sort<MyListSubItem>(m_arrSubItems, 0, this.count, null);
                if (this.owner.ownerChatListBox != null)
                    this.owner.ownerChatListBox.Invalidate(this.owner.bounds);
            }
         
            //确认存储空间
            private void EnsureSpace(int elements) {
                if (m_arrSubItems == null)
                    m_arrSubItems = new MyListSubItem[Math.Max(elements, 4)];
                else if (elements + this.count > m_arrSubItems.Length) {
                    MyListSubItem[] arrTemp = new MyListSubItem[Math.Max(m_arrSubItems.Length * 2, elements + this.count)];
                    m_arrSubItems.CopyTo(arrTemp, 0);
                    m_arrSubItems = arrTemp;
                }
            }
            /// <summary>
            /// 获取索引位置
            /// </summary>
            /// <param name="subItem">要获取索引的子项</param>
            /// <returns>索引</returns>
            public int IndexOf(MyListSubItem subItem) {
                return Array.IndexOf<MyListSubItem>(m_arrSubItems, subItem);
            }
            /// <summary>
            /// 添加一个子项
            /// </summary>
            /// <param name="subItem">要添加的子项</param>
            public void Add(MyListSubItem subItem) {
                if (subItem == null)
                    throw new ArgumentNullException("SubItem cannot be null");
                this.EnsureSpace(1);
                if (-1 == IndexOf(subItem)) {
                    subItem.OwnerListItem = owner;
                    m_arrSubItems[this.count++] = subItem;
                    if (this.owner.OwnerChatListBox != null)
                        this.owner.OwnerChatListBox.Invalidate();
                }
            }
            /// <summary>
            /// 添加一组子项
            /// </summary>
            /// <param name="subItems">要添加子项的数组</param>
            public void AddRange(MyListSubItem[] subItems) {
                if (subItems == null)
                    throw new ArgumentNullException("SubItems cannot be null");
                this.EnsureSpace(subItems.Length);
                try {
                    foreach (MyListSubItem subItem in subItems) {
                        if (subItem == null)
                            throw new ArgumentNullException("SubItem cannot be null");
                        if (-1 == this.IndexOf(subItem)) {
                            subItem.OwnerListItem = this.owner;
                            m_arrSubItems[this.count++] = subItem;
                        }
                    }
                } finally {
                    if (this.owner.OwnerChatListBox != null)
                        this.owner.OwnerChatListBox.Invalidate();
                }
            }
          
            /// <summary>
            /// 移除一个子项
            /// </summary>
            /// <param name="subItem">要移除的子项</param>
            public void Remove(MyListSubItem subItem) {
                int index = this.IndexOf(subItem);
                if (-1 != index)
                    this.RemoveAt(index);
            }
            /// <summary>
            /// 根据索引移除一个子项
            /// </summary>
            /// <param name="index">要移除子项的索引</param>
            public void RemoveAt(int index) {
                if (index < 0 || index >= this.count)
                    throw new IndexOutOfRangeException("Index was outside the bounds of the array");
                this.count--;
                for (int i = index, Len = this.count; i < Len; i++)
                    m_arrSubItems[i] = m_arrSubItems[i + 1];
                if (this.owner.OwnerChatListBox != null)
                    this.owner.OwnerChatListBox.Invalidate();
            }
            /// <summary>
            /// 清空所有子项
            /// </summary>
            public void Clear() {
                this.count = 0;
                m_arrSubItems = null;
                if (this.owner.OwnerChatListBox != null)
                    this.owner.OwnerChatListBox.Invalidate();
            }
            /// <summary>
            /// 根据索引插入一个子项
            /// </summary>
            /// <param name="index">索引位置</param>
            /// <param name="subItem">要插入的子项</param>
            public void Insert(int index, MyListSubItem subItem) {
                if (index < 0 || index >= this.count)
                    throw new IndexOutOfRangeException("Index was outside the bounds of the array");
                if (subItem == null)
                    throw new ArgumentNullException("SubItem cannot be null");
                this.EnsureSpace(1);
                for (int i = this.count; i > index; i--)
                    m_arrSubItems[i] = m_arrSubItems[i - 1];
                subItem.OwnerListItem = this.owner;
                m_arrSubItems[index] = subItem;
                this.count++;
                if (this.owner.OwnerChatListBox != null)
                    this.owner.OwnerChatListBox.Invalidate();
            }
            /// <summary>
            /// 将集合类的子项拷贝至数组
            /// </summary>
            /// <param name="array">要拷贝的数组</param>
            /// <param name="index">拷贝的索引位置</param>
            public void CopyTo(Array array, int index) {
                if (array == null)
                    throw new ArgumentNullException("Array cannot be null");
                m_arrSubItems.CopyTo(array, index);
            }
            /// <summary>
            /// 判断子项是否在集合内
            /// </summary>
            /// <param name="subItem">要判断的子项</param>
            /// <returns>是否在集合内</returns>
            public bool Contains(MyListSubItem subItem) {
                return this.IndexOf(subItem) != -1;
            }
            /// <summary>
            /// 根据索引获取一个列表子项
            /// </summary>
            /// <param name="index">索引位置</param>
            /// <returns>列表子项</returns>
            public MyListSubItem this[int index] {
                get {
                    if (index < 0 || index >= this.count)
                        throw new IndexOutOfRangeException("Index was outside the bounds of the array");
                    return m_arrSubItems[index];
                }
                set {
                    if (index < 0 || index >= this.count)
                        throw new IndexOutOfRangeException("Index was outside the bounds of the array");
                    m_arrSubItems[index] = value;
                    if (this.owner.OwnerChatListBox != null)
                        this.owner.OwnerChatListBox.Invalidate(m_arrSubItems[index].Bounds);
                }
            }
            //接口实现
            int IList.Add(object value) {
                if (!(value is MyListSubItem))
                    throw new ArgumentException("Value cannot convert to ListSubItem");
                this.Add((MyListSubItem)value);
                return this.IndexOf((MyListSubItem)value);
            }

            void IList.Clear() {
                this.Clear();
            }

            bool IList.Contains(object value) {
                if (!(value is MyListSubItem))
                    throw new ArgumentException("Value cannot convert to ListSubItem");
                return this.Contains((MyListSubItem)value);
            }

            int IList.IndexOf(object value) {
                if (!(value is MyListSubItem))
                    throw new ArgumentException("Value cannot convert to ListSubItem");
                return this.IndexOf((MyListSubItem)value);
            }

            void IList.Insert(int index, object value) {
                if (!(value is MyListSubItem))
                    throw new ArgumentException("Value cannot convert to ListSubItem");
                this.Insert(index, (MyListSubItem)value);
            }

            bool IList.IsFixedSize {
                get { return false; }
            }

            bool IList.IsReadOnly {
                get { return false; }
            }

            void IList.Remove(object value) {
                if (!(value is MyListSubItem))
                    throw new ArgumentException("Value cannot convert to ListSubItem");
                this.Remove((MyListSubItem)value);
            }

            void IList.RemoveAt(int index) {
                this.RemoveAt(index);
            }

            object IList.this[int index] {
                get {
                    return this[index];
                }
                set {
                    if (!(value is MyListSubItem))
                        throw new ArgumentException("Value cannot convert to ListSubItem");
                    this[index] = (MyListSubItem)value;
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
                    yield return m_arrSubItems[i];
            }
        }
    }
}
