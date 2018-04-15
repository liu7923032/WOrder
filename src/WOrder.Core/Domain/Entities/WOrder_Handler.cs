﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Abp.Domain.Entities.Auditing;

namespace WOrder.Domain.Entities
{
    //记录创建时间和最后更新时间
    public class WOrder_Handler : AuditedEntity
    {
        /// <summary>
        /// 处理人
        /// </summary>
        public long HandleId { get; set; }

        [ForeignKey("HandleId")]
        public virtual WOrder_Account Handler { get; set; }

        public long OrderId { get; set; }

        [ForeignKey("OrderId")]
        public virtual WOrder_Order Order { get; set; }
        /// <summary>
        /// 记录实时的状态
        /// </summary>
        public OStatus OStatus { get; set; }
        
        //人员处理状态记录表
        public virtual ICollection<WOrder_ORecord> Records { get; set; }
    }
}
