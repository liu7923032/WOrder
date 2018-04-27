using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using WOrder.Domain.Entities;

namespace WOrder.Location
{
    public class LocationProfile : Profile
    {
        public LocationProfile()
        {
            CreateMap<WOrder_Location, LocationDto>()
                .ForMember(u => u.UserName, opts => opts.MapFrom(p => p.User.UserName));
        }

    }
}
