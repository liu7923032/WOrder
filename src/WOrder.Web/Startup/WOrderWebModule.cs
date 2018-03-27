using System;
using Abp.AspNetCore;
using Abp.AspNetCore.Configuration;
using Abp.Authorization;
using Abp.MailKit;
using Abp.Modules;
using Abp.Quartz;
using Abp.Reflection.Extensions;
using Abp.Threading.BackgroundWorkers;
using Abp.Timing;
using WOrder.Configuration;
using WOrder.EntityFrameworkCore;
using WOrder.QuartzJob;
using WOrder.QuartzJob.Jobs;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Quartz;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using WOrder.Web.Core;

namespace WOrder.Web.Startup
{
    [DependsOn(
      
        typeof(WOrderWebCoreModule)
        
       )]
    public class WOrderWebModule : AbpModule
    {
        private readonly IConfigurationRoot _appConfiguration;

        public WOrderWebModule(IHostingEnvironment env)
        {
            _appConfiguration = AppConfigurations.Get(env.ContentRootPath, env.EnvironmentName);
        }

        public override void PreInitialize()
        {

            //2.0 添加导航菜单
            Configuration.Navigation.Providers.Add<WOrderNavigationProvider>();

            //3.0 添加自定义的setting
            Configuration.Settings.Providers.Add<WOrderSettingProvider>();


            //5.0 配置默认的缓存过期时间是10个小时
            Configuration.Caching.ConfigureAll((cache) =>
            {
                cache.DefaultSlidingExpireTime = TimeSpan.FromHours(10);
            });

        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(WOrderWebModule).GetAssembly());
          
        }


        
    }
}