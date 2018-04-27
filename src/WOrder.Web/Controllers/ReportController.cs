using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Microsoft.AspNetCore.Mvc;
using WOrder.Domain.Entities;
using WOrder.Web.Core.Controllers;

namespace WOrder.Web.Controllers
{
    public class ReportController : WOrderControllerBase
    {
       

        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 人员工作量统计
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult> UserWorkCount()
        {
            return await Task.FromResult(View());
        }

        /// <summary>
        /// 运送管理统计
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult> TransportCount()
        {
            return await Task.FromResult(View());
        }

    }

    
}