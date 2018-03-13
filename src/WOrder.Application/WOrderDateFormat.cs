using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json.Converters;

namespace WOrder
{
    /// <summary>
    /// 设置日期格式
    /// </summary>
    public class WOrderDateFormat : IsoDateTimeConverter
    {
        public WOrderDateFormat()
        {
            base.DateTimeFormat = "yyyy-MM-dd HH:mm";
        }

        public WOrderDateFormat(string dateFormat)
        {
            base.DateTimeFormat = dateFormat;
        }
    }
}
