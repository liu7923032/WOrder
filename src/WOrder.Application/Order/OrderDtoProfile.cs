using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using WOrder.Domain.Entities;
using Dark.Common.Extension;

namespace WOrder.Order
{
    public class OrderDtoProfile : Profile
    {
        public OrderDtoProfile()
        {

            CreateMap<WOrder_Handler, HandlerDto>()
             .ForMember(u => u.StatusName, opts => opts.MapFrom(p => p.OStatus.GetDescription()))
             .ForMember(u => u.UserName, opts => opts.MapFrom(p => p.Handler.UserName));
            //订单的mapping
            CreateMap<WOrder_Order, OrderDto>()
                .ForMember(u => u.CreatorName, opts => opts.MapFrom(p => p.CreatorUser.UserName))
                .ForMember(u => u.TStatusName, opts => opts.MapFrom(p => p.TStatus.GetDescription()))
                .ForMember(u => u.Handlers, opts => opts.MapFrom(p => p.Handlers));
                

        }

    }
}
