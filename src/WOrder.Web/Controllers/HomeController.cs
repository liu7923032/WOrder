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
using WOrder.Authorization;
using WOrder.Web.Core.Controllers;
using WOrder.Order;
using Abp.Timing;
using WOrder.Domain.Entities;

namespace WOrder.Web.Controllers
{
    [AbpMvcAuthorize(PermissionNames.Page_Admin)]
    public class HomeController : WOrderControllerBase
    {



        private IFileAppService _fileAppService;
        private IOrderAppService _orderAppService;

        public HomeController(IFileAppService fileAppService, IOrderAppService orderAppService)
        {
            _fileAppService = fileAppService;
            _orderAppService = orderAppService;
        }

        public async Task<ActionResult> Index()
        {
            //1����������Ϣ
            var allocateNum = await _orderAppService.GetTStatusCount(TStatus.Init);
            //2�����ӵ�
            var acceptNum = await _orderAppService.GetTStatusCount(TStatus.Wait);
            //2��������
            var processNum = await _orderAppService.GetTStatusCount(TStatus.Running);

            ViewBag.AllocateNum = allocateNum;
            ViewBag.AcceptNum = acceptNum;
            ViewBag.ProcessNum = processNum;

            return await Task.FromResult(View());
        }


        /// <summary>
        /// ����ͳ��
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult> GetBoardData()
        {
            //4�������������ͳ��
            var categoryEnd = await _orderAppService.GetCategoryCount(new GetDateInput()
            {
                SDate = Clock.Now.AddDays(-7),
                EDate = Clock.Now,
                TStatus=TStatus.Finish
            });
            return Json(categoryEnd);
        }

        /// <summary>
        /// �鿴��ϸ������Ϣ
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult> OrderDetails()
        {
            return await Task.FromResult(View());
        }

        /// <summary>
        /// �鿴��Ϣ
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult> Message()
        {
            return await Task.FromResult(View());
        }
    }
}