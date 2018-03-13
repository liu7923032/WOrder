using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Dependency;
using Abp.Domain.Repositories;
using Abp.Quartz;
using Castle.Core.Logging;
using Dark.Common.Crawler;
using Dark.Common.Serializer;
using Dark.Common.Utils;
using WOrder.Domain.Entities;
using Quartz;

namespace WOrder.QuartzJob.Jobs
{
    public class SyncPriceJob : JobBase, ITransientDependency
    {
        public SyncPriceJob()
        {
        }

        public override async Task Execute(IJobExecutionContext context)
        {
             await Task.Run(() => { });
        }

       
    }

    public class ProductPrice
    {
        public decimal p { get; set; }
    }
}
