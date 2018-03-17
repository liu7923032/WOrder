using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace WOrder.Web.Controllers
{
    public class UserController : WOrderControllerBase
    {
        public async Task<IActionResult> Index()
        {
            return await Task.FromResult(View());
        }
    }
}