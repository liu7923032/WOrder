using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Abp;
using Abp.Authorization;
using Abp.Dependency;
using Abp.Domain.Uow;
using Abp.Runtime.Session;
using Castle.Core.Logging;
using WOrder.Configuration;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace WOrder.Authorization
{
    /// <summary>
    /// 自定义权限检查
    /// </summary>
    public class PermissionChecker : IPermissionChecker, ITransientDependency
    {

        public ILogger Logger { get; set; }

        public IAbpSession AbpSession { get; set; }

        private IConfiguration appConfiguration;
        public ICurrentUnitOfWorkProvider CurrentUnitOfWorkProvider { get; set; }
        public PermissionChecker(IHostingEnvironment env)
        {
            AbpSession = NullAbpSession.Instance;
            Logger = NullLogger.Instance;
            appConfiguration = AppConfigurations.Get(env.ContentRootPath, env.EnvironmentName);
        }


        public async Task<bool> IsGrantedAsync(string permissionName)
        {
            return AbpSession.UserId.HasValue && await IsGrantedAsync(AbpSession.UserId.Value, permissionName);

        }

        private async Task<bool> IsGrantedAsync(long userId, string permissionName)
        {
            var administrators = appConfiguration.GetSection("Authorzation:Administators").Value;

            if (string.IsNullOrEmpty(administrators))
            {
                return await Task.FromResult(false);
            }
            //检查当前人员是否有在里面
            string strUserId = userId.ToString();
            //var user = await _loginManager.GetUserById(userId);
            if (administrators.Contains(strUserId))
            {
                return await Task.FromResult(true);
            }
            else
            {
                return await Task.FromResult(false);
            }

        }

        public async Task<bool> IsGrantedAsync(UserIdentifier user, string permissionName)
        {
            if (CurrentUnitOfWorkProvider?.Current == null)
            {
                return await IsGrantedAsync(user.UserId, permissionName);
            }

            using (CurrentUnitOfWorkProvider.Current.SetTenantId(user.TenantId))
            {
                return await IsGrantedAsync(user.UserId, permissionName);
            }
        }
    }
}
