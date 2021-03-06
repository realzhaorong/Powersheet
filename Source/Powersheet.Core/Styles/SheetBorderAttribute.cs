using System;
using System.Drawing;

namespace Nerosoft.Powersheet
{
    /// <summary>
    /// 单元格边框样式
    /// </summary>
    public abstract class SheetBorderAttribute : Attribute, ISheetStyleAttribute
    {
        /// <summary>
        /// 初始化<see cref="SheetBorderAttribute"/>实例
        /// </summary>
        /// <param name="style"></param>
        public SheetBorderAttribute(BorderStyle style)
        {
            Style = style;
        }

        /// <summary>
        /// 获取边框样式
        /// </summary>
        public BorderStyle Style { get; }

        /// <summary>
        /// 获取或设置边框颜色
        /// </summary>
        public Color Color { get; set; }

        public void SetStyle(CellStyle style)
        {
            style ??= new CellStyle();
            style.BorderColor = Color;
            style.BorderStyle = Style;
        }
    }

    /// <summary>
    /// 表头单元格边框样式
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class SheetHeaderBorderAttribute : SheetBorderAttribute
    {
        public SheetHeaderBorderAttribute(BorderStyle style)
            : base(style)
        {
        }
    }

    /// <summary>
    /// 数据单元格边框样式
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class SheetBodyBorderAttribute : SheetBorderAttribute
    {
        public SheetBodyBorderAttribute(BorderStyle style)
            : base(style)
        {
        }
    }
}