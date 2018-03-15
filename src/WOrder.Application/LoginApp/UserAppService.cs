using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Abp;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.AutoMapper;

using Abp.Dependency;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using Abp.Runtime.Caching;
using Abp.Runtime.Session;
using WOrder.Authorization;
using WOrder.Cache;
using WOrder.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Abp.UI;

namespace WOrder.UserApp
{
    public interface IUserAppService : IAsyncCrudAppService<UserDto, long, GetUsersInput, CreateUserInput, UpdateUserInput>
    {
        //通过账号来获取登陆对象
        Task<WOrder_Account> GetUserByAccountAsync(string account);
        //登陆系统
        Task<WOrder_Account> SignAsync(LoginModel login);
        //创建证件当事人
        Task<ClaimsPrincipal> GetPrincipalAsync(WOrder_Account user, string authenticationType);
        //通过用户来获取账号人员信息
        Task<UserDto> GetUserById(long id);

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="modifyPwdDto"></param>
        /// <returns></returns>
        Task ModifyPassword(ModifyPwdDto modifyPwdDto);

        Task InitPassword(long userId);
    }

    [AbpAllowAnonymous]
    public class UserAppService : AsyncCrudAppService<WOrder_Account, UserDto, long, GetUsersInput, CreateUserInput, UpdateUserInput>, IUserAppService
    {
        private IRepository<WOrder_Account, long> _accountRepository;
        private IUserCache _userCache;

        public UserAppService(IRepository<WOrder_Account, long> userRepository, IUserCache userCache) : base(userRepository)
        {
            _accountRepository = userRepository;
            _userCache = userCache;
        }

        #region 1.0 登陆功能
        public async Task<UserDto> GetUserById(long id)
        {
            return await _userCache.GetAsync(id);
        }

        public async Task<WOrder_Account> GetUserByAccountAsync(string account)
        {
            return await _accountRepository.FirstOrDefaultAsync(u => u.Account.Equals(account));
        }

        public async Task<WOrder_Account> SignAsync(LoginModel login)
        {
            //1:检查账号是否存在
            var user = await GetUserByAccountAsync(login.Account);
            if (user == null)
            {
                throw new AbpException("账号不存在");
            }
            //2:检查密码是否匹配

            if (user.IsLock)
            {
                throw new AbpException("账号被锁住");
            }
            //2:创建身份认证
            //ClaimsIdentity identity = new ClaimsIdentity(authenticationType);
            //identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));
            //identity.AddClaim(new Claim(ClaimTypes.Name, user.Account));
            //return await Task.FromResult< new ClaimsPrincipal(identity);
            return await Task.FromResult(user);
        }

        public async Task<ClaimsPrincipal> GetPrincipalAsync(WOrder_Account user, string authenticationType)
        {

            ClaimsIdentity identity = new ClaimsIdentity(authenticationType);
            identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));
            identity.AddClaim(new Claim(ClaimTypes.Name, user.Account));
            return await Task.FromResult(new ClaimsPrincipal(identity));
        }


        #endregion

        #region 2.0 人员的增删改查
        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>

        [AbpAuthorize(PermissionNames.Page_Admin)]
        public override Task<UserDto> Create(CreateUserInput input)
        {
            return base.Create(input);
        }

        [AbpAuthorize(PermissionNames.Page_Admin)]
        public override Task<UserDto> Update(UpdateUserInput input)
        {
            return base.Update(input);
        }

        [AbpAuthorize(PermissionNames.Page_Admin)]
        public override Task Delete(EntityDto<long> input)
        {
            return base.Delete(input);
        }


        protected override IQueryable<WOrder_Account> CreateFilteredQuery(GetUsersInput input)
        {
            return base.CreateFilteredQuery(input)
                 .WhereIf(!string.IsNullOrEmpty(input.Account), u => u.Account.Contains(input.Account))
                 .WhereIf(!string.IsNullOrEmpty(input.UserName), u => u.UserName.Contains(input.UserName));
        }


        #endregion

        #region 3.0 密码修改
        public async Task ModifyPassword(ModifyPwdDto modifyPwdDto)
        {
            if (modifyPwdDto.OPwd != modifyPwdDto.CPwd)
            {
                throw new UserFriendlyException("旧密码和确认密码不同,请确认");
            }
            var userEntity = await _accountRepository.GetAsync(modifyPwdDto.UserId);
            if (userEntity.Password != modifyPwdDto.OPwd)
            {
                throw new UserFriendlyException("旧密码错误,请确认");
            }
            userEntity.Password = modifyPwdDto.NPwd;
            await _accountRepository.UpdateAsync(userEntity);
        }

        /// <summary>
        /// 初始化密码
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task InitPassword(long userId)
        {
            var userEntity = await _accountRepository.GetAsync(userId);
            userEntity.Password = "0000";
            await _accountRepository.UpdateAsync(userEntity);
        }
        #endregion
    }
}
