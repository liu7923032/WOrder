using System.Reflection;
using Abp.Configuration.Startup;
using Abp.Localization;
using Abp.Localization.Dictionaries;
using Abp.Localization.Dictionaries.Json;
using Abp.Localization.Dictionaries.Xml;
using Abp.Localization.Sources;
using Abp.Reflection.Extensions;

namespace WOrder.Localization
{
    public static class WOrderLocalizationConfigurer
    {
        public static void Configure(ILocalizationConfiguration localizationConfiguration)
        {
            localizationConfiguration.Languages.Add(new LanguageInfo("en", "English", "famfamfam-flags england", isDefault: true));
            localizationConfiguration.Languages.Add(new LanguageInfo("zh-CN", "中文", "famfamfam-flags england"));

            localizationConfiguration.Sources.Add(
                new DictionaryBasedLocalizationSource(WOrderConsts.LocalizationSourceName,
                    new JsonEmbeddedFileLocalizationDictionaryProvider(
                        typeof(WOrderLocalizationConfigurer).GetAssembly(),
                        "WOrder.Localization.SourceFiles"
                    )
                ));

            //替换系统内置的错误提示
            localizationConfiguration.Sources.Extensions.Add(
                new LocalizationSourceExtensionInfo("AbpWeb",
                    new XmlEmbeddedFileLocalizationDictionaryProvider(
                        typeof(WOrderLocalizationConfigurer).GetAssembly(),
                        "WOrder.Localization.AbpWeb"
                    )
               ));
        }
    }
}