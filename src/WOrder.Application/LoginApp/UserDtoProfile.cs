using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using WOrder.Domain.Entities;

namespace WOrder.UserApp
{
    public class UserDtoProfile:Profile
    {
        public UserDtoProfile()
        {
            CreateMap<WOrder_Account, UserDto>();
            CreateMap<UserDto, WOrder_Account>();
        }
    }
}
