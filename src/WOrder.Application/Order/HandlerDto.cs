using System;
using System.Collections.Generic;
using System.Text;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using WOrder.Domain.Entities;

namespace WOrder.Order
{
    [AutoMapTo(typeof(WOrder_Handler))]
    public class CreateHandlerDto
    {
        /// <summary>
        /// 处理人
        /// </summary>
        public long HandleId { get; set; }

        public long OrderId { get; set; }

        public OStatus OStatus { get; set; }
    }

    public class UpdateHandlerDto : CreateHandlerDto, IEntityDto<long>
    {
        public long Id { get; set; }
    }

    public class HandlerDto : UpdateHandlerDto
    {
        public string UserName { get; set; }

        /// 记录实时的状态
        /// </summary>
        public string StatusName { get; set; }

        
    }
}
