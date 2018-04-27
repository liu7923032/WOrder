using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WOrder.Web.Core.Controllers;

namespace WOrder.Web.Controllers
{
    public class RoleController : WOrderControllerBase
    {

        public IActionResult Index()
        {
            return View();
        }
    }
}