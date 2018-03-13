using System;
using System.Collections.Generic;
using System.Text;

namespace Dark.Common.Crawler
{
    /// <summary>
    /// 爬虫爬去数
    /// </summary>
    public class CrawlerOptions
    {
        /// <summary>
        /// 请求地址
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// document
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 爬去的内容
        /// </summary>
        public List<SelectorOptions> CssSelectors { get; set; }

    }



}
