using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Abp.AspNetCore.Mvc.Authorization;
using Abp.Timing;
using Abp.Web.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WOrder.Authorization;
using WOrder.File;
using WOrder.UserApp;
using WOrder.Web.Core.Controllers;

namespace WOrder.Web.Controllers
{
    [AbpMvcAuthorize(PermissionNames.Page_Admin)]
    public class UserController : WOrderControllerBase
    {
        private readonly IUserAppService _userAppService;
        private IHostingEnvironment hostingEnv;

        public UserController(IUserAppService userAppService, IHostingEnvironment _hostingEnv)
        {
            _userAppService = userAppService;
            hostingEnv = _hostingEnv;
        }

        public async Task<IActionResult> Index()
        {
            return await Task.FromResult(View());
        }



        public async Task<ActionResult> Create(CreateUserInput input, IFormFile file)
        {
            string filePath = string.Empty;
            if (file != null)
            {
                filePath = await SaveFile(file, input.Account);
            }
            input.Photos = filePath;
            await _userAppService.Create(input);
            return Json(new AjaxResponse());
        }

        public async Task<ActionResult> Update(UpdateUserInput input, IFormFile file)
        {
            string filePath = string.Empty;
            if (file != null)
            {
                filePath = await SaveFile(file, input.Account);
            }
            input.Photos = filePath;
            await _userAppService.Update(input);
            return Json(new AjaxResponse());
        }

        private async Task<string> SaveFile(IFormFile file, string account)
        {

            if (file == null)
            {
                return await Task.FromResult("文件不存在");
            }

            #region 1.0 生成文件dto对象
            CreateFileInput attachFile = new CreateFileInput
            {
                FileName = ContentDispositionHeaderValue
                               .Parse(file.ContentDisposition)
                              .FileName
                              .Trim('"'),
                ContentType = file.ContentType,
            };
            attachFile.FileType = Path.GetExtension(attachFile.FileName);
            #endregion

            #region 2.0 创建文件的存放路径
            string relativeFilePath = $"\\upload\\user\\{account}\\";
            string fileDir = hostingEnv.WebRootPath + relativeFilePath;
            if (!Directory.Exists(fileDir))
            {
                Directory.CreateDirectory(fileDir);
            }
            #endregion

            #region 3.0 设置文件的项目路径
            string fileName = $"{ account }{attachFile.FileType}";
            attachFile.FilePath = relativeFilePath.Replace("\\", "/") + $"{fileName}";
            #endregion

            #region 4.0 保存文件到数据和具体的位置中
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

            return await Task.FromResult(attachFile.FilePath);
        }

        /// <summary>
        /// 审核页面
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult> Approve()
        {
            return await Task.FromResult(View());
        }
    }
}