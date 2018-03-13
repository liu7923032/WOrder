using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Configuration;
using WOrder.Configuration;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace WOrder.Web.Startup
{
    public class WOrderSettingProvider : SettingProvider
    {
        private IConfigurationRoot _appConfiguration;

        public WOrderSettingProvider(IHostingEnvironment env)
        {
            _appConfiguration = AppConfigurations.Get(env.ContentRootPath, env.EnvironmentName);
        }

        public override IEnumerable<SettingDefinition> GetSettingDefinitions(SettingDefinitionProviderContext context)
        {
            var section = _appConfiguration.GetSection("Mail");
            var dictCollection = section.GetChildren();

            List<SettingDefinition> setting = new List<SettingDefinition>();
            foreach (var item in section.GetChildren())
            {
                setting.Add(new SettingDefinition("Abp.Net.Mail." + item.Key, item.Value, scopes: SettingScopes.Application));
            }
            return setting;

            // Abp.Net.Mail.Smtp.Host

        }
    }
}
