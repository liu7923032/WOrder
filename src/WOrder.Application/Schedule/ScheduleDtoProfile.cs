using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Dark.Common.Utils;
using WOrder.Domain.Entities;

namespace WOrder.Schedule
{
    public class ScheduleDtoProfile : Profile
    {
        public ScheduleDtoProfile()
        {
            CreateMap<WOrder_Schedule, ScheduleDto>()
                .ForMember(u => u.UserName, opts => opts.MapFrom(p => p.User.UserName))
                .ForMember(u => u.AreaName, opts => opts.MapFrom(p => p.User.AreaName))
                .ForMember(u => u.WorkMode, opts => opts.MapFrom(p => p.User.WorkMode))
                .ForMember(u => u.Week, opts => opts.MapFrom(p => DateTool.GetWeekZN(p.ClassDate.DayOfWeek)));
        }
    }
}
