using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.AspNetCore.Mvc.ViewComponents;

namespace WOrder.Web.Views
{
    public abstract class WOrderViewComponent: AbpViewComponent
    {
        protected WOrderViewComponent()
        {
            LocalizationSourceName = WOrderConsts.LocalizationSourceName;
        }
    }
}
