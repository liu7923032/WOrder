using System;
using System.Collections.Generic;
using System.Text;

namespace Dark.Common.Crawler
{
    /// <summary>
    /// 返回的结果类
    /// </summary>
    public class CrawlerResult
    {
        /// <summary>
        /// 返回元素的文本文件
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// 返回元素的所有的属性
        /// </summary>
        public Dictionary<string, string> Attributes { get; set; }

        /// <summary>
        /// 返回元素的html
        /// </summary>
        public string OutHtml { get; set; }
    }
}
