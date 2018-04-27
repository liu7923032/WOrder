using System;
using System.Collections.Generic;
using System.Text;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Newtonsoft.Json;
using WOrder.Domain.Entities;

namespace WOrder.Message
{
    [AutoMapTo(typeof(Sys_Message))]
    public class CreateMessageInput
    {
        public string Title { get; set; }

        public string Content { get; set; }

        public string Target { get; set; }

        public string AppPage { get; set; }

        public long UserId { get; set; }

        public long SrcId { get; set; }
    }

    public class UpdateMessageInput : CreateMessageInput, IEntityDto<long>
    {
        public long Id { get; set; }
    }

    public class MessageDto : UpdateMessageInput
    {
        [JsonConverter(typeof(WOrderDateFormat))]
        public DateTime CreationTime { get; set; }

        public string SendUser { get; set; }
    }


    public class GetAllMessageInput : PagedAndSortedResultRequestDto
    {
        public long? UserId { get; set; }

        public bool? IsRead { get; set; }

        public DateTime? SDate { get; set; }

        public DateTime? EDate { get; set; }
    }

}
