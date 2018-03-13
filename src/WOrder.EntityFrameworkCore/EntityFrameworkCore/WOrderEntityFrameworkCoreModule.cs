using Abp.EntityFrameworkCore;
using Abp.Modules;
using Abp.Reflection.Extensions;

namespace WOrder.EntityFrameworkCore
{
    [DependsOn(
        typeof(WOrderCoreModule), 
        typeof(AbpEntityFrameworkCoreModule))]
    public class WOrderEntityFrameworkCoreModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(WOrderEntityFrameworkCoreModule).GetAssembly());

        }
    }
}