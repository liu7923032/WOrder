using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.Timing;
using Abp.Web.Models;
using Dark.Common.Utils;
using WOrder.Domain.Entities;
using WOrder.File;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WOrder.Web.Controllers
{
    public class FileController : WOrderControllerBase
    {
        private IHostingEnvironment hostingEnv;
        private IFileAppService _fileAppService;

        public FileController(IHostingEnvironment hostingEnv, IFileAppService fileAppService)
        {
            this.hostingEnv = hostingEnv;
            this._fileAppService = fileAppService;
        }

        /// <summary>
        /// 图片上传
        /// </summary>
        /// <param name="file"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public async Task<AjaxResponse> Upload(IFormFile file, string param = "")
        {
            if (file == null)
            {
                return await Task.FromResult(new AjaxResponse(false) { Result = "文件不存在" });
            }

            #region 1.0 生成文件dto对象
            CreateFileInput attachFile = new CreateFileInput
            {
                FileName = ContentDispositionHeaderValue
                               .Parse(file.ContentDisposition)
                              .FileName
                              .Trim('"'),
                Describe = param,
                ContentType = file.ContentType,
                FileSize = GetFileSize(file.Length),
            };
            attachFile.FileType = Path.GetExtension(attachFile.FileName);
            #endregion

            #region 2.0 创建文件的存放路径
            string relativeFilePath = $"\\upload\\product\\{Clock.Now.ToString("yyyy_MM")}\\";
            string fileDir = hostingEnv.WebRootPath + relativeFilePath;
            if (!Directory.Exists(fileDir))
            {
                Directory.CreateDirectory(fileDir);
            }
            #endregion

            #region 3.0 设置文件的项目路径
            string fileName = $"{ Clock.Now.ToString("yyyyMMdd") }_{GetUniqValue()}{attachFile.FileType}";
            attachFile.FilePath = relativeFilePath.Replace("\\", "/") + $"{fileName}";
            #endregion

            #region 4.0 保存文件到数据和具体的位置中
            var newFile = await _fileAppService.Create(attachFile);
            //3:将文件拷贝到对应的位置
            string newFilePath = $"{ fileDir }{ fileName }";
            using (FileStream fs = System.IO.File.Create(newFilePath))
            {
                // 复制文件新路径
                await file.CopyToAsync(fs);
                // 清空缓冲区数据
                fs.Flush();
            }
            #endregion

            //返回结果集
            return await Task.FromResult(new AjaxResponse(true) { Result = ObjectMapper.Map<FileDto>(newFile) });

        }

        /// <summary>
        /// 获取文件的大小
        /// </summary>
        /// <param name="fileSize"></param>
        /// <returns></returns>
        private string GetFileSize(long fileSize)
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
        public string GetUniqValue()
        {
            byte[] buffer = Guid.NewGuid().ToByteArray();
            return BitConverter.ToInt64(buffer, 0).ToString();
        }

        /// <summary>
        /// 下载图片
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<FileStreamResult> DownByURL()
        {
            string url = Request.Query["url"];
            var imgStream = await HttpTools.GetStreamAsync(url);
            return File(imgStream, "image/jpeg","商品图片.jpg");
        }

    }
}