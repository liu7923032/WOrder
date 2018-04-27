using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using WOrder.Domain.Entities;

namespace WOrder.Message
{
    public class MessageProfile : Profile
    {
        public MessageProfile()
        {
            CreateMap<Sys_Message, MessageDto>().
                ForMember(u => u.SendUser, opts => opts.MapFrom(p => p.Creator.UserName));
        }
    }
}
