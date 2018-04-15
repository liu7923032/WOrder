using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace WOrder.Web.Controllers
{
    public class ReportController : Controller
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
    }
}