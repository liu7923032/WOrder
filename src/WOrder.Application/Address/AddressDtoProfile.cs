using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using WOrder.Domain.Entities;

namespace WOrder.Address
{
    public class AddressDtoProfile : Profile
    {
        public AddressDtoProfile()
        {
            CreateMap<WOrder_Address, AddressDto>();
        }
    }
}
