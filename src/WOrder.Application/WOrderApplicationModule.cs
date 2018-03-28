using Abp.AutoMapper;
using Abp.MailKit;
using Abp.Modules;
using Abp.Reflection.Extensions;
using WOrder.Authorization;
using WOrder.Email;
using Abp.Configuration.Startup;
using WOrder.Extension;

namespace WOrder
{
    [DependsOn(
        typeof(WOrderCoreModule),
        typeof(AbpAutoMapperModule),
        typeof(AbpMailKitModule))]
    public class WOrderApplicationModule : AbpModule
    {

        public override void PreInitialize()
        {
            //添加权限验证
            Configuration.Authorization.Providers.Add<WOrderAuthorizationProvider>();

            //替换默认的smtp服务器
            Configuration.ReplaceService<IMailKitSmtpBuilder, WOrderMailKitSmtpBuilder>();
        }

        public override void Initialize()
        {

            var thisAssembly = typeof(WOrderApplicationModule).GetAssembly();

            IocManager.RegisterAssemblyByConvention(thisAssembly);

            Configuration.Modules.AbpAutoMapper().Configurators.Add(cfg =>
            {
                // Scan the assembly for classes which inherit from AutoMapper.Profile
                cfg.AddProfiles(thisAssembly);
            });

            //注册jpush帮助类
            IocManager.Register(typeof(JPushHelper), Abp.Dependency.DependencyLifeStyle.Transient);
        }
    }
}