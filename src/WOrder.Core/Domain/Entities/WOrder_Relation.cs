using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Abp.Domain.Entities.Auditing;

namespace WOrder.Domain.Entities
{
    /// <summary>
    /// 关联表
    /// </summary>
    public class WOrder_Relation : CreationAuditedEntity
    {
        /// <summary>
        /// 对谁稽核 ,对谁巡检
        /// </summary>
        public long OrderId { get; set; }

        /// <summary>
        /// 新稽核产生的表
        /// </summary>
        public long AuditedId { get; set; }

        /// <summary>
        /// 具体对做的操作
        /// </summary>
        public string Category { get; set; }

        
    }
}
