using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Abp.Domain.Entities.Auditing;

namespace WOrder.Domain.Entities
{
    /// <summary>
    /// 投诉
    /// </summary>
    public class WOrder_Complaint : CreationAuditedEntity
    {
        public long ComplaintUId { get; set; }

        [ForeignKey("ComplaintUId")]
        public virtual WOrder_Account CAmount { get; set; }


    }

    /// <summary>
    /// 投诉状态
    /// </summary>
    public enum ComplaintStatus
    {
        Init,

        End
    }
}
