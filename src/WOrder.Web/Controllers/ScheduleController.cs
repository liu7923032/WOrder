﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc;
using WOrder.Authorization;
using WOrder.Web.Core.Controllers;

namespace WOrder.Web.Controllers
{
    [AbpMvcAuthorize(PermissionNames.Page_Admin)]
    public class ScheduleController : WOrderControllerBase
    {
        public async Task<IActionResult> Index()
        {
            return await Task.FromResult(View());
        }
    }
}