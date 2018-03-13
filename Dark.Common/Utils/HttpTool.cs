using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace Dark.Common.Utils
{
    //通用的http请求库
    public class HttpTools
    {
        /// <summary>
        /// 简单的通过网页请求,来获取页面的数据
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static async Task<string> GetStringAsync(string url)
        {
            return await HttpFunc<string>(async (client) =>
            {
                return await client.GetStringAsync(url);
            });
        }

        /// <summary>
        /// 下载一个流
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static async Task<Stream> GetStreamAsync(string url)
        {
            return await HttpFunc<Stream>(async (client) =>
            {
                return await client.GetStreamAsync(url);
            });
        }




        public static async Task DownFileAsync(List<string> urls)
        {
            await HttpAction(async (client) =>
            {
                foreach (var url in urls)
                {
                    //1.通过url获取远程流
                    var stream = await GetStreamAsync(url);
                    //2.将流下载下来并保存到临时文件中
                    //3.将文件压缩
                }
            });
        }

        /// <summary>
        /// 一般的
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="func"></param>
        /// <returns></returns>
        public static async Task<T> HttpFunc<T>(Func<HttpClient, Task<T>> func)
        {
            using (HttpClient client = new HttpClient())
            {
                return await func(client);
            }
        }

        public static async Task HttpAction(Func<HttpClient, Task> action)
        {
            using (HttpClient client = new HttpClient())
            {
                await action(client);
            }
        }
    }
}
