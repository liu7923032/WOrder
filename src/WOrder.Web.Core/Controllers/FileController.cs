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
using Abp.AspNetCore.Mvc.Authorization;
using WOrder.Web.Core.Controllers;
using WOrder.UserApp;
using Abp.UI;

namespace WOrder.Web.Controllers
{
    [Route("api/[controller]/[action]")]

    public class FileController : WOrderControllerBase
    {
        private IHostingEnvironment hostingEnv;
        private IFileAppService _fileAppService;
        private IUserAppService _userService;

        public FileController(IHostingEnvironment hostingEnv, IFileAppService fileAppService, IUserAppService userAppService)
        {
            this.hostingEnv = hostingEnv;
            this._fileAppService = fileAppService;
            this._userService = userAppService;
        }

        /// <summary>
        /// 图片上传
        /// </summary>
        /// <param name="file"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost]
        [WrapResult(WrapOnSuccess = false, WrapOnError = true)]
        public async Task<JsonResult> Upload(IFormFile file, string module = "")
        {
            var newFile = await SaveFile(file, module);
            return await Task.FromResult(Json(ObjectMapper.Map<FileDto>(newFile)));

        }
        /// <summary>
        /// 上传图片的方法
        /// </summary>
        /// <param name="file"></param>
        /// <param name="module"></param>
        /// <returns></returns>
        private async Task<FileDto> SaveFile(IFormFile file, string module)
        {
            if (file == null)
            {
                throw new UserFriendlyException("文件不存在");
            }

            #region 1.0 生成文件dto对象
            CreateFileInput attachFile = new CreateFileInput
            {
                FileName = ContentDispositionHeaderValue
                               .Parse(file.ContentDisposition)
                              .FileName
                              .Trim('"'),
                Describe = "",
                Module = module,
                ContentType = file.ContentType,
                FileSize = GetFileSize(file.Length),
            };
            attachFile.FileType = Path.GetExtension(attachFile.FileName);
            #endregion

            #region 2.0 创建文件的存放路径
            string relativeFilePath = $"\\upload\\{module}\\{Clock.Now.ToString("yyyy_MM")}\\";
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

            return await Task.FromResult(newFile);
        }

     


    }
}