using System;
using System.Collections.Generic;
using System.Text;

namespace Dark.Common.Crawler
{
    /// <summary>
    /// 爬虫爬的内容
    /// </summary>
    public class SelectorOptions
    {
        public SelectorOptions(string name,string cssSelector)
        {
            this.Name = name;
            this.Selector = cssSelector;
            this.IsArray = false;
        }

        public SelectorOptions(string name, string cssSelector, bool isArray = false)
        {
            this.Name = name;
            this.Selector = cssSelector;
            this.IsArray = isArray;
        }
        /// <summary>
        /// 要获取的名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 选择器
        /// </summary>
        public string Selector { get; set; }

        /// <summary>
        /// 是否是数组
        /// </summary>
        public bool IsArray { get; set; }
    }
}
