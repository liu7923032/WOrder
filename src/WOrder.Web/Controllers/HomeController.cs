using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.AspNetCore.Mvc.Authorization;
using Abp.AutoMapper;
using WOrder.Category;
using WOrder.Comment;
using WOrder.File;
using WOrder.Web.Startup;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WOrder.Web.Controllers
{
    public class HomeController : WOrderControllerBase
    {

        private IFileAppService _fileAppService;


        public HomeController(IFileAppService fileAppService)
        {
            _fileAppService = fileAppService;
        }

        public async Task<ActionResult> Index()
        {

            
            return View();
        }

      

    }
}