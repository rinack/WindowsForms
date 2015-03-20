using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;

namespace Windows.Forms.Controls.ToolTips
{
    public class InterpolationModeGraphics : IDisposable
    {
        private InterpolationMode _oldMode;
        private Graphics _graphics;

        public InterpolationModeGraphics(Graphics graphics)
            : this(graphics, InterpolationMode.HighQualityBicubic)
        {
        }

        public InterpolationModeGraphics(
            Graphics graphics, InterpolationMode newMode)
        {
            _graphics = graphics;
            _oldMode = graphics.InterpolationMode;
            graphics.InterpolationMode = newMode;
        }

        #region IDisposable 成员

        public void Dispose()
        {
            _graphics.InterpolationMode = _oldMode;
        }

        #endregion
    }
}
