﻿using System;
using System.Collections.Generic;
using System.Text;
using Abp.Application.Services.Dto;
using WOrder.Domain.Entities;

namespace WOrder.Order
{
    public class CreateHandlerDto
    {
        /// <summary>
        /// 处理人
        /// </summary>
        public long HandleId { get; set; }

        public string UserName { get; set; }

        public int OrderId { get; set; }

        public OStatus OStatus { get; set; }
        /// 记录实时的状态
        /// </summary>
        public string StatusName { get; set; }
    }

    public class UpdateHandlerDto : CreateHandlerDto, IEntityDto<int>
    {
        public int Id { get; set; }
    }

    public class HandlerDto : UpdateHandlerDto
    {

    }
}
