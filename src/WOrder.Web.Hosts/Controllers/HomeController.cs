using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WOrder.Web.Core.Controllers;

namespace WOrder.Web.Hosts.Controllers
{
    public class HomeController : WOrderControllerBase
    {
        public IActionResult Index()
        {
            return Redirect("/swagger");
        }
    }
}