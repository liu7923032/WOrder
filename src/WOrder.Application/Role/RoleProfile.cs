using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using WOrder.Domain.Entities;

namespace WOrder.Role
{
    public class RoleProfile : Profile
    {

        public RoleProfile()
        {
            CreateMap<Sys_Role, RoleDto>();
        }
    }
}
