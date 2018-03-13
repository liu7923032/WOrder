using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using WOrder.Domain.Entities;

namespace WOrder.Integral
{
    public class IntegralDtoProfile : Profile
    {
        public IntegralDtoProfile()
        {
            CreateMap<CreateIntegralInput, WOrder_Integral>();
            CreateMap<WOrder_Integral, IntegralDto>();
        }
    }
}
