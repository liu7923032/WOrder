using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Dark.Common.Attributes
{
    public class ExcelDataAttribute : Attribute
    {

        public ExcelDataAttribute(string name)
        {
            this.Name = name;
        }
        /// <summary>
        /// 标题
        /// </summary>
        public string Name { get; set; }

       
        /// <summary>
        /// 宽度,默认自动宽度,如果设置了,那么就
        /// </summary>
        public int Width { get; set; }

        public PropertyInfo Property { get; set; }
    }
}
