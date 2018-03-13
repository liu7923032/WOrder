using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using WOrder.Domain.Entities;

namespace WOrder.Order
{
    public class OrderDtoProfile : Profile
    {
        public OrderDtoProfile()
        {
            CreateMap<WOrder_Order, OrderDto>();
            CreateMap<WOrder_Order, OrderDto>().ForMember(u => u.CreatorName, opts => opts.MapFrom(p => p.CreatorUser.UserName))
                .ForMember(u => u.ApproveUName, opts => opts.MapFrom(p => p.LastModifierUser.UserName))
                .ForMember(u => u.AddressInfo, opts => opts.MapFrom(p => p.Address.ToString()));
        }

        /// <summary>
        /// 处理购物状态
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>

    }
}
