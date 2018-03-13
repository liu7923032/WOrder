using System;
using System.Collections.Generic;
using System.Text;
using NPOI.HSSF.Util;

namespace Dark.Common.Utils.Excel
{
    public interface IHasFontColor
    {
        FontColor FontColor { get; set; }
    }


    public enum FontColor
    {
        Blue = HSSFColor.Blue.Index,
        Red = HSSFColor.Red.Index,
        Orange = HSSFColor.Orange.Index
    }
}
