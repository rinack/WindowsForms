﻿using System;
using System.Collections.Generic;

using System.Text;

namespace Windows.Forms.Controls.Enums
{
    public enum ControlState
    {
        /// <summary>
        /// 正常状态
        /// </summary>
        Normal = 0,
        /// <summary>
        ///  /鼠标进入
        /// </summary>
        Highlight = 1,
        /// <summary>
        /// 鼠标按下
        /// </summary>
        Down = 2,
        /// <summary>
        /// 获得焦点
        /// </summary>
        Focus = 3,
        /// <summary>
        /// 控件禁止
        /// </summary>
        Disabled = 4  
    }
}
