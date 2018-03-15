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

                ).AddItem(new MenuItemDefinition(
                            PageNames.SysManage,
                            L(nameof(PageNames.SysManage)),
                            url: "",
                            icon: ""
                         ).AddItem(new MenuItemDefinition(
                             PageNames.WorkOrder,
                            L(nameof(PageNames.WorkOrder)),
                            url: "Order/Index",
                            icon: ""))
                         .AddItem(new MenuItemDefinition(
                            PageNames.Schedule,
                            L(nameof(PageNames.Schedule)),
                            url: "Schedule/Index",
                            icon: ""
                        )));

        }

        private static ILocalizableString L(string name)
        {
            return new LocalizableString(name, WOrderConsts.LocalizationSourceName);
        }
    }


    public class WOrderPermissionDependency : IPermissionDependency, ITransientDependency
    {
        private IConfiguration appConfiguration;
        private IUserAppService _loginManager;
        public WOrderPermissionDependency(IHostingEnvironment env, IUserAppService loginManager)
        {
            _loginManager = loginManager;
            appConfiguration = AppConfigurations.Get(env.ContentRootPath, env.EnvironmentName);
        }

        public async Task<bool> IsSatisfiedAsync(IPermissionDependencyContext context)
        {
            //找到有管理员权限的用户
            var administrators = appConfiguration.GetSection("Authorzation:Administators").Value;

            if (string.IsNullOrEmpty(administrators))
            {
                return await Task.FromResult(false);
            }
            //检查当前人员是否有在里面
            var userId = Convert.ToInt32(context.User.UserId);
            //var user = await _loginManager.GetUserById(userId);
            if (administrators == "M0679")
            {
                return await Task.FromResult(true);
            }
            else
            {
                return await Task.FromResult(false);
            }
        }
    }
}
