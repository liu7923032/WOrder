using System;
using System.Threading.Tasks;
using Abp.Application.Navigation;
using Abp.Authorization;
using Abp.Dependency;
using Abp.Localization;
using WOrder.Authorization;
using WOrder.Configuration;
using WOrder.UserApp;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace WOrder.Web.Startup
{
    /// <summary>
    /// This class defines menus for the application.
    /// </summary>
    public class WOrderNavigationProvider : NavigationProvider
    {
        //private IPermissionDependency _permissionDependency;
        //public WOrderNavigationProvider(IPermissionDependency permissionDependency)
        //{
        //    _permissionDependency = permissionDependency;
        //}

        public override void SetNavigation(INavigationProviderContext context)
        {
            context.Manager.MainMenu
                .AddItem(
                    new MenuItemDefinition(
                            PageNames.BaseInfo,
                            L(nameof(PageNames.BaseInfo)),
                            url: "",
                            icon: "&#xe68e;"
                        ).AddItem(new MenuItemDefinition(
                                    PageNames.Dictionary,
                                    L(nameof(PageNames.Dictionary)),
                                    url: "Dictionary/Index",
                                    icon: "&#xe604;",
                                    requiredPermissionName: PermissionNames.Page_Admin
                                ))
                         .AddItem(new MenuItemDefinition(
                                    PageNames.User,
                                    L(nameof(PageNames.User)),
                                    url: "User/Index",
                                    icon: "&#xe604;",
                                    requiredPermissionName: PermissionNames.Page_Admin
                                ))
                        .AddItem(new MenuItemDefinition(
                                    PageNames.RoleSetting,
                                    L(nameof(PageNames.RoleSetting)),
                                    url: "Role/Index",
                                    icon: "&#xe604;",
                                    requiredPermissionName: PermissionNames.Page_Admin
                                ))
                         .AddItem(new MenuItemDefinition(
                                    PageNames.Approve,
                                    L(nameof(PageNames.Approve)),
                                    url: "User/Approve",
                                    icon: "&#xe604;",
                                    requiredPermissionName: PermissionNames.Page_Admin
                                ))
                         .AddItem(new MenuItemDefinition(
                                    PageNames.Location,
                                    L(nameof(PageNames.Location)),
                                    url: "Location/Index",
                                    icon: "&#xe604;",
                                    requiredPermissionName: PermissionNames.Page_Admin
                                ))

                    ).AddItem(new MenuItemDefinition(
                            PageNames.SysManage,
                            L(nameof(PageNames.SysManage)),
                            url: "",
                            icon: ""
                         ).AddItem(new MenuItemDefinition(
                             PageNames.WorkOrder,
                            L(nameof(PageNames.WorkOrder)),
                            url: "Order/Index",
                            requiredPermissionName: PermissionNames.Page_Admin,
                            icon: ""))
                         .AddItem(new MenuItemDefinition(
                             PageNames.Audit,
                            L(nameof(PageNames.Audit)),
                            url: "Order/Audit",
                            requiredPermissionName: PermissionNames.Page_Admin,
                            icon: ""))
                         .AddItem(new MenuItemDefinition(
                             PageNames.Feedback,
                            L(nameof(PageNames.Feedback)),
                            url: "Order/Feedback",
                            requiredPermissionName: PermissionNames.Page_Admin,
                            icon: ""))
                        .AddItem(new MenuItemDefinition(
                            PageNames.Inspect,
                            L(nameof(PageNames.Inspect)),
                            url: "Order/Inspect",
                            requiredPermissionName: PermissionNames.Page_Admin,
                            icon: ""
                        )).AddItem(new MenuItemDefinition(
                            PageNames.Transport,
                            L(nameof(PageNames.Transport)),
                            url: "Order/Transport",
                            requiredPermissionName: PermissionNames.Page_Admin,
                            icon: ""
                        ))
                         .AddItem(new MenuItemDefinition(
                            PageNames.Schedule,
                            L(nameof(PageNames.Schedule)),
                            url: "Schedule/Index",
                            requiredPermissionName: PermissionNames.Page_Admin,
                            icon: ""
                        )))
                 .AddItem(
                            new MenuItemDefinition(
                                PageNames.Report,
                                L(nameof(PageNames.Report)),
                                url: "")
                            .AddItem(
                                new MenuItemDefinition(
                                    PageNames.UserWorkCount,
                                    L(nameof(PageNames.UserWorkCount)),
                                    url: "Report/UserWorkCount",
                                    requiredPermissionName: PermissionNames.Page_Admin, icon: ""))
                            .AddItem(
                                new MenuItemDefinition(
                                    PageNames.UserWorkCount,
                                    L(nameof(PageNames.TransportCount)),
                                    url: "Report/TransportCount",
                                    requiredPermissionName: PermissionNames.Page_Admin, icon: ""))
                           );

        }

        private static ILocalizableString L(string name)
        {
            return new LocalizableString(name, WOrderConsts.LocalizationSourceName);
        }
    }


}
