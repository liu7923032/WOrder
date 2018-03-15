using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using WOrder.Dictionary;
using WOrder.Domain.Entities;

namespace WOrder.Category
{
    public class DictionaryMapProfile : Profile
    {
        public DictionaryMapProfile()
        {
            CreateMap<WOrder_Dictionary, DictDto>();
            CreateMap<WOrder_DictType, DictTypeDto>();
        }
    }
}
