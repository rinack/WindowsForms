using System;
using System.Collections.Generic;
using System.Text;

namespace Windows.Forms.Controls.MyList
{

    //自定 义事件参数类
    public class MyListEventArgs
    {
        private MyListSubItem mouseOnSubItem;
        public MyListSubItem MouseOnSubItem {
            get { return mouseOnSubItem; }
        }

        private MyListSubItem selectSubItem;
        public MyListSubItem SelectSubItem {
            get { return selectSubItem; }
        }

        public MyListEventArgs(MyListSubItem mouseonsubitem, MyListSubItem selectsubitem)
        {
            this.mouseOnSubItem = mouseonsubitem;
            this.selectSubItem = selectsubitem;
        }
    }
}
