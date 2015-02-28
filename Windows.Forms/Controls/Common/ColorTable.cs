using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using Windows.Forms.Controls.Methods;

namespace Windows.Forms.Controls.Common
{
    internal class ColorTable
    {
        public static Color QQBorderColor = Color.LightBlue;  //LightBlue = Color.FromArgb(173, 216, 230)
        public static Color QQHighLightColor = RenderHelper.GetColor(QQBorderColor, 255, -63, -11, 23);   //Color.FromArgb(110, 205, 253)
        public static Color QQHighLightInnerColor = RenderHelper.GetColor(QQBorderColor, 255, -100, -44, 1);   //Color.FromArgb(73, 172, 231);
    }
}
