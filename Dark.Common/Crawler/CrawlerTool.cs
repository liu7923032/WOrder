using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AngleSharp.Dom;
using AngleSharp.Dom.Html;
using AngleSharp.Parser.Html;
using Dark.Common.Utils;

namespace Dark.Common.Crawler
{
    /// <summary>
    /// 通过
    /// </summary>
    public class CrawlerTool
    {
        public CrawlerTool()
        {
        }
        /// <summary>
        /// 创建HtmlDocument
        /// </summary>
        /// <param name="opts"></param>
        /// <returns></returns>
        private static async Task<IHtmlDocument> CreateDocumnet(CrawlerOptions opts)
        {
            var source = string.Empty;
            if (!string.IsNullOrEmpty(opts.Url))
            {
                source = await HttpTools.GetStringAsync(opts.Url);
            }
            if (!string.IsNullOrEmpty(opts.Content))
            {
                source = opts.Content;
            }

            //1.创建一个html解析器
            var parser = new HtmlParser();
            //2.解析数据源
            var document = parser.Parse(source);
            //3.返回document
            return document;
        }

        /// <summary>
        /// 返回结果
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        private static CrawlerResult CreateCResult(IElement element)
        {
            CrawlerResult crawlerResult = new CrawlerResult();
            crawlerResult.Text = element.TextContent;
            crawlerResult.OutHtml = element.OuterHtml;
            crawlerResult.Attributes = new Dictionary<string, string>();
            foreach (var attribute in element.Attributes)
            {
                crawlerResult.Attributes[attribute.Name] = attribute.Value;
            }
            return crawlerResult;
        }


        /// <summary>
        /// 获取爬虫字典
        /// </summary>
        /// <param name="opts"></param>
        /// <returns></returns>
        public static async Task<Dictionary<string, List<CrawlerResult>>> GetResultAsync(CrawlerOptions opts)
        {

            Dictionary<string, List<CrawlerResult>> dictResult = new Dictionary<string, List<CrawlerResult>>();
            var document = await CreateDocumnet(opts);
            //多线程爬虫
            List<Task> tasks = new List<Task>();
            opts.CssSelectors.ForEach(u =>
            {
                //启动多线程
                tasks.Add(Task.Factory.StartNew(() =>
                {
                    var elements = document.QuerySelectorAll(u.Selector);
                    List<CrawlerResult> cResults = new List<CrawlerResult>();
                   //是一个集合
                   foreach (var element in elements)
                    {
                        cResults.Add(CreateCResult(element));
                    }
                    dictResult[u.Name] = cResults;
                }));

            });

            Task.WaitAll(tasks.ToArray());
            // 获取html 元素
            return await Task.FromResult(dictResult);
        }

    }
}
