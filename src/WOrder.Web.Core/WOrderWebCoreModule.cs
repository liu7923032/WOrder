using System;
using System.Collections.Generic;
using System.Text;
using Abp.AspNetCore;
using Abp.AspNetCore.Configuration;
using Abp.Modules;
using Abp.Reflection.Extensions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using WOrder.EntityFrameworkCore;
using WOrder.QuartzJob;
using WOrder.Web.Core.Configuration;

namespace WOrder.Web.Core
{
    [DependsOn(
        typeof(WOrderApplicationModule),
        typeof(WOrderEntityFrameworkCoreModule),
        typeof(AbpAspNetCoreModule),
        typeof(WOrderQuartzJobModule)
       )]
    public class WOrderWebCoreModule: AbpModule
    {
        private readonly IHostingEnvironment _env;
        private readonly IConfigurationRoot _appConfiguration;

        public WOrderWebCoreModule(IHostingEnvironment env)
        {
            _env = env;
            _appConfiguration = env.GetAppConfiguration();
        }

        public override void PreInitialize()
        {
           
            //1.0 配置连接字符串
            Configuration.DefaultNameOrConnectionString = _appConfiguration.GetConnectionString(WOrderConsts.ConnectionStringName);

            //2.0 注册服务
            Configuration.Modules.AbpAspNetCore()
                 .CreateControllersForAppServices(
                     typeof(WOrderApplicationModule).GetAssembly()
                 );

            ConfigureTokenAuth();
        }

        private void ConfigureTokenAuth()
        {
            IocManager.Register<TokenAuthConfiguration>();
            var tokenAuthConfig = IocManager.Resolve<TokenAuthConfiguration>();

            tokenAuthConfig.SecurityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_appConfiguration["Authentication:JwtBearer:SecurityKey"]));
            tokenAuthConfig.Issuer = _appConfiguration["Authentication:JwtBearer:Issuer"];
            tokenAuthConfig.Audience = _appConfiguration["Authentication:JwtBearer:Audience"];
            tokenAuthConfig.SigningCredentials = new SigningCredentials(tokenAuthConfig.SecurityKey, SecurityAlgorithms.HmacSha256);
            tokenAuthConfig.Expiration = TimeSpan.FromDays(1);
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(WOrderWebCoreModule).GetAssembly());
        }
    }
}
