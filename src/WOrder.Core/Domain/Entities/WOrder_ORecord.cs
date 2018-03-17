using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Abp.Domain.Entities.Auditing;

namespace WOrder.Domain.Entities
{
    /// <summary>
    /// 订单状态变更记录表
    /// </summary>
    public class WOrder_ORecord : CreationAuditedEntity
    {
        /// <summary>
        /// 订单的状态
        /// </summary>
        public OStatus OStatus { get; set; }
        /// <summary>
        /// 处理人
        /// </summary>
        public int HandlerId { get; set; }

        [ForeignKey("HandlerId")]
        public virtual WOrder_Handler Handler { get; set; }

    }

    /// <summary>
    /// 订单处理的状态
    /// </summary>
    public enum OStatus
    {
        /// <summary>
        /// 待接单
        /// </summary>
        [Description("待处理")]
        Init,
        /// <summary>
        /// 接单
        /// </summary>
        [Description("已接单")]
        Accept,
        /// <summary>
        /// 完结
        /// </summary>
        [Description("处理完")]
        Finish
    }

}
