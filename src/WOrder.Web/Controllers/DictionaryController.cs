using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace WOrder.Web.Controllers
{
    public class DictionaryController: WOrderControllerBase
    {
        public async Task<ActionResult> Index()
        {
            return await Task.FromResult(View());
        }
    }
}
