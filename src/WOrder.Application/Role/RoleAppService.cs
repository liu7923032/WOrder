using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.UI;
using Microsoft.EntityFrameworkCore;
using WOrder.Authorization;
using WOrder.Domain.Entities;
using WOrder.UserApp;

namespace WOrder.Role
{
    public interface IRoleAppService : IAsyncCrudAppService<RoleDto, int, GetAllRoleInput, CreateRoleInput, UpdateRoleInput>
    {
        /// <summary>
        /// 将用户添加到角色
        /// </summary>
        /// <returns></returns>
        Task<bool> AddUsersToRole(List<long> uIds, int roleId);

        /// <summary>
        /// 将用户从角色中删除
        /// </summary>
        /// <param name="uIds"></param>
        /// <param name="roleId"></param>
        /// <returns></returns>
        Task<bool> DelUserRoles(List<int> uIds);


        //Task<PagedResultDto<UserRoleDto>> GetUsersByRoleId(int roleId);

        Task<PagedResultDto<UserRoleDto>> GetUsersByRoleId(GetUsersByRole input);
    }

    /// <summary>
    /// 只有管理员才能对角色进行操作
    /// </summary>
    [AbpAuthorize(PermissionNames.Page_Admin)]
    public class RoleAppService : AsyncCrudAppService<Sys_Role, RoleDto, int, GetAllRoleInput, CreateRoleInput, UpdateRoleInput>, IRoleAppService
    {

        private readonly IRepository<Sys_Role> _roleRepository;
        private readonly IRepository<Sys_UserRole> _userRoleRepository;
        private readonly IRepository<WOrder_Account, long> _accountRepository;
        private readonly IRepository<WOrder_Department> _deptRepository;
        public RoleAppService(IRepository<Sys_Role> repository,
            IRepository<Sys_UserRole> userRoleRepository,
            IRepository<WOrder_Account, long> accountRepository,
            IRepository<WOrder_Department> deptRepository) : base(repository)
        {
            _roleRepository = repository;
            _userRoleRepository = userRoleRepository;
            _accountRepository = accountRepository;
            _deptRepository = deptRepository;
        }

        public async Task<bool> AddUsersToRole(List<long> uIds, int roleId)
        {
            //1:检查用户是否都正常
            var userIds = _accountRepository.GetAll().Where(u => uIds.Contains(u.Id)).Select(u => u.Id).ToList();

            //2:排除掉已经存在该角色的人员
            var userRoleIds = _userRoleRepository.GetAll().Where(u => u.RoleId.Equals(roleId)).Select(u => u.UserId).ToList();
            //3:移除匹配项
            userIds.RemoveAll(u => userRoleIds.Contains(u));
            //2:管理用户和角色权限
            userIds.ForEach(async u =>
            {
                Sys_UserRole entity = new Sys_UserRole()
                {
                    UserId = u,
                    RoleId = roleId
                };
                await _userRoleRepository.InsertAsync(entity);
            });
            return await Task.FromResult(true);
        }

        public async Task<bool> DelUserRoles(List<int> uIds)
        {
            await _userRoleRepository.DeleteAsync(u => uIds.Contains(u.Id));
            return await Task.FromResult(true);
        }

        //public async Task<PagedResultDto<UserRoleDto>> GetUsersByRoleId(int roleId)
        //{


        //    var users = (from a in _userRoleRepository
        //                     .GetAll()
        //                     .Where(u => u.RoleId.Equals(roleId))
        //                 join b in _accountRepository.GetAll()
        //                 on a.UserId equals b.Id
        //                 join c in _deptRepository.GetAll()
        //                 on b.DeptId equals c.Id into leftJoin
        //                 from c in leftJoin.DefaultIfEmpty()
        //                 orderby b.Id
        //                 select new UserRoleDto
        //                 {
        //                     Id = a.Id,
        //                     UserId = b.Id,
        //                     UserName = b.UserName,
        //                     Account = b.Account,
        //                     Phone = b.Phone,
        //                     Sex = b.Sex,
        //                     Position = b.Position,
        //                     DeptName = c == null ? "" : c.Name
        //                 }).ToList();
        //    return await Task.FromResult(new PagedResultDto<UserRoleDto>()
        //    {
        //        TotalCount = users.Count(),
        //        Items = users
        //    });
        //}

        public async Task<PagedResultDto<UserRoleDto>> GetUsersByRoleId(GetUsersByRole input)
        {
            var users = (from a in _userRoleRepository
                              .GetAll()
                              .Where(u => u.RoleId.Equals(input.RoleId))
                         join b in _accountRepository.GetAll()
                         on a.UserId equals b.Id
                         join c in _deptRepository.GetAll()
                         on b.DeptId equals c.Id into leftJoin
                         from c in leftJoin.DefaultIfEmpty()
                         orderby b.Id
                         select new UserRoleDto
                         {
                             Id = a.Id,
                             UserId = b.Id,
                             UserName = b.UserName,
                             Account = b.Account,
                             Phone = b.Phone,
                             Sex = b.Sex,
                             Position = b.Position,
                             DeptName = c == null ? "" : c.Name
                         });
            if (!string.IsNullOrEmpty(input.Key))
            {
                users = users.Where(u => u.UserName.Contains(input.Key)
                                            || u.Account.Contains(input.Key)
                                            || u.DeptName.Contains(input.Key));
            }

            if (input.SkipCount.HasValue)
            {
                users = users.Skip(input.SkipCount.Value);
            }
            if (input.MaxResultCount.HasValue)
            {
                users = users.Take(input.MaxResultCount.Value);
            }

            var items = users.ToList();
            return await Task.FromResult(new PagedResultDto<UserRoleDto>()
            {
                TotalCount = items.Count(),
                Items = items
            });
        }
    }
}
