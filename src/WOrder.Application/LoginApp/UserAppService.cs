﻿using System;
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
using Microsoft.EntityFrameworkCore;
using WOrder.File;
using Dark.Common.Extension;

namespace WOrder.UserApp
{
    public interface IUserAppService : IAsyncCrudAppService<UserDto, long, GetUsersInput, CreateUserInput, UpdateUserInput>
    {
        //通过账号来获取登陆对象
        Task<UserDto> GetUserByAccountAsync(string account);
        //登陆系统
        Task<UserDto> SignAsync(LoginModel login);
        //创建证件当事人
        Task<ClaimsPrincipal> GetPrincipalAsync(UserDto user, string authenticationType);
        //通过用户来获取账号人员信息
        Task<UserDto> GetUserById(long id);

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="modifyPwdDto"></param>
        /// <returns></returns>
        Task<bool> ModifyPassword(ModifyPwdDto modifyPwdDto);
        /// <summary>
        /// 初始化密码
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<bool> InitPassword(long userId);

        /// <summary>
        /// 人员注册
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<UserDto> Register(CreateUserInput input);
        /// <summary>
        /// 激活账号
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<bool> ActiveAccount(long userId);

        /// <summary>
        /// 返回新图片地址
        /// </summary>
        /// <param name="fileId"></param>
        /// <returns></returns>
        Task<string> ChangePhoto(int fileId);

    }

    [AbpAllowAnonymous]
    public class UserAppService : AsyncCrudAppService<WOrder_Account, UserDto, long, GetUsersInput, CreateUserInput, UpdateUserInput>, IUserAppService
    {
        private IRepository<WOrder_Account, long> _accountRepository;
        private IRepository<WOrder_AttachFile> _fileRepository;

        public UserAppService(IRepository<WOrder_Account, long> userRepository, IRepository<WOrder_AttachFile> fileRepository) : base(userRepository)
        {
            _accountRepository = userRepository;
            _fileRepository = fileRepository;
        }

        #region 1.0 登陆功能
        public async Task<UserDto> GetUserById(long id)
        {
            var userEntity = await _accountRepository.GetAsync(id);
            return await Task.FromResult(userEntity.MapTo<UserDto>());
        }

        public async Task<UserDto> GetUserByAccountAsync(string account)
        {
            WOrder_Account user = null;
            var isTel = int.TryParse(account, out int telphone);
            if (isTel)
            {
                user = _accountRepository.GetAll().Where(u => u.Phone.Equals(account)).Include("Department").FirstOrDefault();
            }
            else
            {
                string strAccount = account.ToUpper();
                user = _accountRepository.GetAll().Where(u => u.Account.Equals(account)).Include("Department").FirstOrDefault();
            }
            return await Task.FromResult(user.MapTo<UserDto>());
        }

        public async Task<UserDto> SignAsync(LoginModel login)
        {
            //1:检查账号是否存在
            var user = await GetUserByAccountAsync(login.Account.Trim());
            if (user == null)
            {
                throw new UserFriendlyException("账号不存在");
            }
            //激活检查
            if (!user.IsActive)
            {
                throw new UserFriendlyException("账号未激活,请等待..");
            }
            //2:检查密码是否匹配
            if (user.Password != login.Password)
            {
                throw new UserFriendlyException("密码错误,请重试");
            }
            //2:创建身份认证
            //ClaimsIdentity identity = new ClaimsIdentity(authenticationType);
            //identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));
            //identity.AddClaim(new Claim(ClaimTypes.Name, user.Account));
            //return await Task.FromResult< new ClaimsPrincipal(identity);
            return await Task.FromResult(user);
        }

        public async Task<ClaimsPrincipal> GetPrincipalAsync(UserDto user, string authenticationType)
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
        public async override Task<UserDto> Create(CreateUserInput input)
        {
            input.Account = input.Account.ToUpper();
            input.IsActive = true;
            WOrder_Account account = input.MapTo<WOrder_Account>();

            await _accountRepository.InsertAsync(account);
            await CurrentUnitOfWork.SaveChangesAsync();
            if (!string.IsNullOrEmpty(input.FileIds))
            {
                List<int> fileIds = input.FileIds.ToListBySplit();
                var fileList = await _fileRepository.GetAllListAsync(u => fileIds.Contains(u.Id));
                fileList.ForEach(u =>
                {
                    u.ParentId = account.Id.ToString();
                    u.Module = "user";
                });
            }
            return await Task.FromResult(account.MapTo<UserDto>());
        }

        [AbpAuthorize(PermissionNames.Page_Admin)]
        public async override Task<UserDto> Update(UpdateUserInput input)
        {
            if (!string.IsNullOrEmpty(input.FileIds))
            {
                int fileId = input.FileIds.ToListBySplit().FirstOrDefault();
                var newFile = await UpdateUserPhoto(input.Id, fileId);
                input.Photos = newFile.FilePath;
            }

            return await base.Update(input);
        }

        [AbpAuthorize(PermissionNames.Page_Admin)]
        public override Task Delete(EntityDto<long> input)
        {
            return base.Delete(input);
        }


        protected override IQueryable<WOrder_Account> CreateFilteredQuery(GetUsersInput input)
        {
            return base.CreateFilteredQuery(input)
                 .WhereIf(!string.IsNullOrEmpty(input.Key),
                            u => (u.Account.Contains(input.Key) 
                            || u.Department.Name.Contains(input.Key)
                            || u.UserName.Contains(input.Key)))
                 //.WhereIf(!string.IsNullOrEmpty(input.UserName), u => u.UserName.Contains(input.UserName))
                 .WhereIf(input.DeptId.HasValue, u => u.DeptId.Equals(input.DeptId.Value))
                 .WhereIf(input.IsActive.HasValue, u => u.IsActive.Equals(input.IsActive.Value)).Include("Department");
        }


        #endregion

        #region 3.0 密码修改
        public async Task<bool> ModifyPassword(ModifyPwdDto modifyPwdDto)
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
            return await Task.FromResult(true);
        }

        /// <summary>
        /// 初始化密码
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<bool> InitPassword(long userId)
        {
            var userEntity = await _accountRepository.GetAsync(userId);
            userEntity.Password = "000000";
            await _accountRepository.UpdateAsync(userEntity);
            return await Task.FromResult(true);
        }

        /// <summary>
        /// 注册账号
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<UserDto> Register(CreateUserInput input)
        {
            //注册时候,默认是未激活状态
            WOrder_Account account = input.MapTo<WOrder_Account>();
            account.IsActive = false;
            await _accountRepository.InsertAsync(account);
            return await Task.FromResult(account.MapTo<UserDto>());
        }

        /// <summary>
        /// 激活账号
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<bool> ActiveAccount(long userId)
        {
            var account = _accountRepository.Get(userId);
            account.IsActive = true;
            await _accountRepository.UpdateAsync(account);
            return await Task.FromResult(true);
        }

        public async Task<string> ChangePhoto(int fileId)
        {
            var userEntity = await _accountRepository.GetAsync(AbpSession.GetUserId());
            var newFile = await UpdateUserPhoto(userEntity.Id, fileId);
            userEntity.Photos = newFile.FilePath;
            await _accountRepository.UpdateAsync(userEntity);
            return await Task.FromResult(userEntity.Photos);
        }

        private async Task<WOrder_AttachFile> UpdateUserPhoto(long userId, int newFileId)
        {
            var newFile = _fileRepository.Get(newFileId);
            //删除旧文件
            var oldFile = await _fileRepository.FirstOrDefaultAsync(u => u.ParentId == userId.ToString() && u.Module.Equals("user"));
            if (oldFile != null)
            {
                await _fileRepository.DeleteAsync(oldFile);
            }
            //设置新图片地址
            newFile.ParentId = userId.ToString();
            return await Task.FromResult(newFile);
        }

        #endregion
    }
}
