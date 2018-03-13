using System.Reflection;
using Abp.AspNetCore.TestBase;
using Abp.Modules;
using Abp.Reflection.Extensions;
using WOrder.Web.Startup;

namespace WOrder.Web.Tests
{
    [DependsOn(
        typeof(WOrderWebModule),
        typeof(AbpAspNetCoreTestBaseModule)
        )]
    public class WOrderWebTestModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(WOrderWebTestModule).GetAssembly());
        }
    }
}