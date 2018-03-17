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
            CreateMap<WOrder_Account, UserDto>().ForMember(u => u.DeptName, opts => opts.MapFrom(u => u.Department.Name));
        }
    }
}
