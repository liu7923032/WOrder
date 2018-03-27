using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Abp.AspNetCore.Mvc.Controllers;
using Abp.Timing;
using Microsoft.AspNetCore.Mvc;

namespace WOrder.Web.Core.Controllers
{
    public abstract class WOrderControllerBase : AbpController
    {
        protected WOrderControllerBase()
        {
            LocalizationSourceName = WOrderConsts.LocalizationSourceName;
        }


        protected string GetExcelDir()
        {
            return string.Empty;
        }

        protected FileResult ToExcel(Stream fs, string fileName = "")
        {
            if (string.IsNullOrEmpty(fileName))
            {
                fileName = $"{Clock.Now.ToString("yyyy-MM-dd")}.xlsx";

            }
            return File(fs, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
        }
    }
}
