using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;

using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Drawing.Drawing2D;

namespace Windows.Forms.Controls.CUDataGridView
{
    /// <summary>
    /// 扩展DataGrid控件
    /// </summary>
    [ToolboxBitmap(typeof(DataGrid))]
    public partial class DataGridViewEx : System.Windows.Forms.DataGridView
    {
        private Color headersColor;

        [Description("获取或设置DataGridView 表头的颜色的颜色")]
        [DefaultValue(typeof(Color))]
        public Color HeadersColor
        {
            get { return headersColor; }
            set { headersColor = value; }
        }

        protected override void OnCellPainting(DataGridViewCellPaintingEventArgs e)
        {
            base.OnCellPainting(e);
            if (e.ColumnIndex == -1 && e.RowIndex == -1)
            {
                using (LinearGradientBrush brush = new LinearGradientBrush(e.CellBounds, Color.LightGray,
                    Color.White, LinearGradientMode.ForwardDiagonal))
                {
                    e.Graphics.FillRectangle(brush, e.CellBounds);
                    Rectangle border = e.CellBounds;
                    border.Offset(new Point(-1, -1));
                    e.Graphics.DrawRectangle(Pens.Gray, border);
                }
                e.PaintContent(e.CellBounds);
                e.Handled = true;
            }
            else if (e.RowIndex == -1)
            {
                //标题行
                using (LinearGradientBrush brush = new LinearGradientBrush(e.CellBounds, Color.LightGray,
                    Color.White, LinearGradientMode.Vertical))
                {
                    e.Graphics.FillRectangle(brush, e.CellBounds);
                    Rectangle border = e.CellBounds;
                    border.Offset(new Point(-1, -1));
                    e.Graphics.DrawRectangle(Pens.Gray, border);
                }
                e.PaintContent(e.CellBounds);
                e.Handled = true;
            }
            else if (e.ColumnIndex == -1)
            {
                //标题列
                using (LinearGradientBrush brush = new LinearGradientBrush(e.CellBounds, Color.LightGray,
                    Color.White, LinearGradientMode.Horizontal))
                {
                    e.Graphics.FillRectangle(brush, e.CellBounds);
                    Rectangle border = e.CellBounds;
                    border.Offset(new Point(-1, -1));
                    e.Graphics.DrawRectangle(Pens.Gray, border);
                }
                e.PaintContent(e.CellBounds);
                e.Handled = true;
            }

        }
    }
}
