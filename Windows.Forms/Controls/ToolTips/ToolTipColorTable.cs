using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Windows.Forms.Controls.ToolTips
{
    public class ToolTipColorTable
    {
        private static readonly Color _base = Color.FromArgb(105, 200, 254);
        private static readonly Color _border = Color.FromArgb(204, 153, 51);
        private static readonly Color _backNormal = Color.FromArgb(250, 250, 250);
        private static readonly Color _backHover = Color.FromArgb(255, 180, 105);
        private static readonly Color _backPressed = Color.FromArgb(226, 176, 0);
        private static readonly Color _titleFore = Color.Brown;
        private static readonly Color _tipFore = Color.Chocolate;

        public ToolTipColorTable() { }

        public virtual Color Base
        {
            get { return _base; }
        }

        public virtual Color Border
        {
            get { return _border; }
        }

        public virtual Color BackNormal
        {
            get { return _backNormal; }
        }

        public virtual Color BackHover
        {
            get { return _backHover; }
        }

        public virtual Color BackPressed
        {
            get { return _backPressed; }
        }

        public virtual Color TitleFore
        {
            get { return _titleFore; }
        }

        public virtual Color TipFore
        {
            get { return _tipFore; }
        }
    }
}
