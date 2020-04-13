using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace Tools
{
    /// <summary>
    /// DataGridView扩展添加行号
    /// </summary>
    public class DataGridViewUtil:DataGridView
    {
        /// <summary>
        ///  解析数据显示区域添加行号
        /// </summary>
        /// <param name="e"></param>
        protected override void  OnRowPostPaint(DataGridViewRowPostPaintEventArgs e)
        {
 	         base.OnRowPostPaint(e);
             Rectangle rectangle = new Rectangle(e.RowBounds.Location.X,
                 Convert.ToInt32(e.RowBounds.Location.Y + (e.RowBounds.Height - this.RowHeadersDefaultCellStyle.Font.Size) / 2),
                 this.RowHeadersWidth - 4,
                 e.RowBounds.Height);
             TextRenderer.DrawText(e.Graphics, (e.RowIndex + 1).ToString(),
                 this.RowHeadersDefaultCellStyle.Font,
                 rectangle,
                 this.RowHeadersDefaultCellStyle.ForeColor,
                 TextFormatFlags.Right);
        }
    }
}
