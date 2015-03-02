using System;
using System.Collections.Generic;
using System.Text;

namespace Windows.Forms.Controls.Forms.MyListBox
{

    //自定 义事件参数类
    public class MyListBoxtEventArgs
    {
        private MyListBoxSubItem mouseOnSubItem;
        public MyListBoxSubItem MouseOnSubItem {
            get { return mouseOnSubItem; }
        }

        private MyListBoxSubItem selectSubItem;
        public MyListBoxSubItem SelectSubItem {
            get { return selectSubItem; }
        }

        public MyListBoxtEventArgs(MyListBoxSubItem mouseonsubitem, MyListBoxSubItem selectsubitem)
        {
            this.mouseOnSubItem = mouseonsubitem;
            this.selectSubItem = selectsubitem;
        }
    }
}
