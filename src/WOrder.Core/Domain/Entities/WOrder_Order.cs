using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Abp.Domain.Entities.Auditing;

namespace WOrder.Domain.Entities
{
    public class WOrder_Order : FullAuditedEntity<long>
    {
        /// <summary>
        /// 订单编号
        /// </summary>
        [Required]
        [StringLength(20)]
        public string OrderNo { get; set; }

        /// <summary>
        /// 分类 投诉 稽核  派单
        /// </summary>
        [Required]
        [StringLength(20)]
        public string Category { get; set; }

        /// <summary>
        /// 项目名称
        /// </summary>
        [Required]
        [StringLength(200)]
        public string ItemName { get; set; }

        /// <summary>
        /// 具体位置
        /// </summary>
        [Required]
        [StringLength(100)]
        public string OAddress { get; set; }

        /// <summary>
        /// 描述信息
        /// </summary>
        [StringLength(1000)]
        public string Description { get; set; }

        /// <summary>
        /// 到达时间
        /// </summary>
        public DateTime? ArriveDate { get; set; }

        /// <summary>
        /// 单据的类别 ,默认是0是派单，1是抢单 
        /// </summary>
        [Required]
        public OrderType OrderType { get; set; }

        /// <summary>
        /// 当前订单状态
        /// </summary>
        [Required]
        public TStatus TStatus { get; set; }

        /// <summary>
        /// 通过串联创建人和修改人
        /// </summary>
        [ForeignKey("CreatorUserId")]
        public virtual WOrder_Account CreatorUser { get; set; }


        [ForeignKey("LastModifierUserId")]
        public virtual WOrder_Account LastModifierUser { get; set; }

        /// <summary>
        /// 工单的处理人
        /// </summary>
        public virtual ICollection<WOrder_Handler> Handlers { get; set; }
    }


    /// <summary>
    /// 订单类别
    /// </summary>
    public enum OrderType
    {
        /// <summary>
        /// 派单
        /// </summary>
        Dispatch,
        /// <summary>
        /// 抢单
        /// </summary>
        Rob
    }

    /// <summary>
    /// 任务状态
    /// </summary>
    public enum TStatus
    {
        /// <summary>
        /// 待接收
        /// </summary>
        [Description("待派单")]
        Init = 0,
        /// <summary>
        /// 待接单
        /// </summary>
        [Description("待接单")]
        Wait,
        /// <summary>
        /// 处理中
        /// </summary>
        [Description("处理中")]
        Running,
        /// <summary>
        /// 完成
        /// </summary>
        [Description("已完成")]
        Finish,
    }

}
