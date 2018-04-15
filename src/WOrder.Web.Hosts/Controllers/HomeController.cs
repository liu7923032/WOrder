using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using WOrder.Configuration;
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