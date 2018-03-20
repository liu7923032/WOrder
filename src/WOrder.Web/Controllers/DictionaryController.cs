using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc;
using WOrder.Authorization;

namespace WOrder.Web.Controllers
{
    [AbpMvcAuthorize(PermissionNames.Page_Admin)]
    public class DictionaryController: WOrderControllerBase
    {
        public async Task<ActionResult> Index()
        {
            return await Task.FromResult(View());
        }
    }
}
