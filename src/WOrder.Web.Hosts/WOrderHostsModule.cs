using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.AspNetCore.Configuration;
using Abp.Dependency;
using Abp.Modules;
using Abp.Reflection.Extensions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using WOrder.Web.Core;
using WOrder.Web.Core.Configuration;
using Abp.Configuration.Startup;

namespace WOrder.Web.Hosts
{
   
    [DependsOn(
       typeof(WOrderWebCoreModule))]
    public class WOrderHostsModule : AbpModule
    {
        private readonly IHostingEnvironment _env;
        private readonly IConfigurationRoot _appConfiguration;

        public WOrderHostsModule(IHostingEnvironment env)
        {
            _env = env;
            _appConfiguration = env.GetAppConfiguration();
        }


        public override void PreInitialize()
        {
            
        }

        public override void Initialize()
        {
            Configuration.Modules.AbpAspNetCore().DefaultWrapResultAttribute.WrapOnSuccess = false;

            IocManager.RegisterAssemblyByConvention(typeof(WOrderHostsModule).GetAssembly());
        }
    }
}
