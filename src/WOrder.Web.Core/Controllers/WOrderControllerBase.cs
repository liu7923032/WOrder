using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Abp.AspNetCore.Mvc.Controllers;
using Abp.Timing;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WOrder.File;

namespace WOrder.Web.Core.Controllers
{
    public abstract class WOrderControllerBase : AbpController
    {

        protected WOrderControllerBase()
        {
            LocalizationSourceName = WOrderConsts.LocalizationSourceName;
        }


        protected string GetExcelDir()
        {
            return string.Empty;
        }

        protected FileResult ToExcel(Stream fs, string fileName = "")
        {
            if (string.IsNullOrEmpty(fileName))
            {
                fileName = $"{Clock.Now.ToString("yyyy-MM-dd")}.xlsx";

            }
            return File(fs, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
        }


        /// <summary>
        /// 获取文件的大小
        /// </summary>
        /// <param name="fileSize"></param>
        /// <returns></returns>
        protected string GetFileSize(long fileSize)
        {
            string size = "";

            if (fileSize > 1024 * 1024 * 1024)
            {
                size = (fileSize / 1024 * 1024 * 1024).ToString() + "GB";
            }
            else if (fileSize > 1024 * 1024)
            {
                size = (fileSize / 1024 * 1024).ToString() + "MB";
            }
            else
            {
                size = (fileSize / 1024).ToString() + "KB";
            }
            return size;
        }

        /// <summary>
        /// 生成时间戳
        /// </summary>
        /// <returns></returns>
        protected string GetUniqValue()
        {
            byte[] buffer = Guid.NewGuid().ToByteArray();
            return BitConverter.ToInt64(buffer, 0).ToString();
        }


       
    }
}
