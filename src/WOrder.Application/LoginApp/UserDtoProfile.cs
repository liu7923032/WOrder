using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using WOrder.Domain.Entities;

namespace WOrder.UserApp
{
    public class UserDtoProfile : Profile
    {
        public UserDtoProfile()
        {
            CreateMap<WOrder_Account, UserDto>()
                .ForMember(u => u.DeptName, opts => opts.MapFrom(u => u.Department.Name))
                .ForMember(u => u.RoleName, opts => opts.MapFrom(u => GetRoles(u.Roles)));
        }

        private string GetRoles(ICollection<Sys_UserRole> roles)
        {
            string roleName = string.Empty;
            foreach (var role in roles)
            {
                roleName += role.RoleId + ",";
            }
            if (roleName.Length > 0)
            {
                roleName = roleName.Substring(0, roleName.Length - 1);
            }
            return roleName;
        }
    }
}
