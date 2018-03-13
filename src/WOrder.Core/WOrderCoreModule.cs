using Abp.Localization.Dictionaries.Xml;
using Abp.Localization.Sources;
using Abp.Modules;
using Abp.Reflection.Extensions;
using WOrder.Localization;

namespace WOrder
{
    public class WOrderCoreModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Auditing.IsEnabledForAnonymousUsers = true;
            //禁用多语言包
            Configuration.Localization.IsEnabled = true;


            WOrderLocalizationConfigurer.Configure(Configuration.Localization);
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(WOrderCoreModule).GetAssembly());
        }
    }
}