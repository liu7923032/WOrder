using System;
using Abp.Modules;
using Abp.Quartz;
using WOrder.QuartzJob.Jobs;
using Quartz;

namespace WOrder.QuartzJob
{
    [DependsOn(typeof(AbpQuartzModule), typeof(WOrderCoreModule))]
    public class WOrderQuartzJobModule : AbpModule
    {



        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(WOrderQuartzJobModule).Assembly);
        }

        public override void PostInitialize()
        {
            var jobManager = IocManager.Resolve<IQuartzScheduleJobManager>();
            //执行调度系统
            jobManager.ScheduleAsync<SyncPriceJob>(
                job =>
                {
                    job.WithIdentity(nameof(SyncPriceJob), "Group")
                        .WithDescription("A job to sync price from internet.");
                },
                trigger =>
                {
                    trigger.StartNow()
                        .WithCronSchedule("0 0 1 * * ?");
                });
        }
    }
}
