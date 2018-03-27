using System;
using System.Collections.Generic;
using System.Text;
using Abp.Authorization;
using Abp.Localization;

namespace WOrder.Authorization
{
    public class WOrderAuthorizationProvider : AuthorizationProvider
    {
        public override void SetPermissions(IPermissionDefinitionContext context)
        {
            context.CreatePermission(PermissionNames.Page_Admin, L("Page_Admin"));
        }

        private static ILocalizableString L(string name)
        {
            return new LocalizableString(name, WOrderConsts.LocalizationSourceName);
        }


    }
}
