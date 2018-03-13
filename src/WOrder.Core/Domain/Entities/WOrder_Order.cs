using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Abp.Domain.Entities.Auditing;

namespace WOrder.Domain.Entities
{
    public class WOrder_Order : FullAuditedEntity
    {
        /// <summary>
        /// 订单编号
        /// </summary>
        [Required]
        [StringLength(20)]
        public string OrderNo { get; set; }

        /// <summary>
        /// 分类
        /// </summary>
        [Required]
        [StringLength(20)]
        public string Category { get; set; }


        /// <summary>
        /// 货物名称
        /// </summary>
        [Required]
        [StringLength(20)]
        public string GoodName { get; set; }

        /// <summary>
        /// 楼层
        /// </summary>
        [Required]
        [StringLength(100)]
        public string Floor { get; set; }

        /// <summary>
        /// 订单编号
        /// </summary>
        [Required]
        [StringLength(100)]
        public string Address { get; set; }

        /// <summary>
        /// 处理人
        /// </summary>
        public long? HandleUId { get; set; }

        /// <summary>
        /// 订单状态
        /// </summary>
        [Required]
        public OrderStatus OStatus { get; set; }

        /// <summary>
        /// 通过串联创建人和修改人
        /// </summary>
        [ForeignKey("CreatorUserId")]
        public virtual WOrder_Account CreatorUser { get; set; }

        [ForeignKey("LastModifierUserId")]
        public virtual WOrder_Account LastModifierUser { get; set; }
        /// <summary>
        /// 处理人
        /// </summary>
        [ForeignKey("HandleUId")]
        public virtual WOrder_Account HandleUser { get; set; }
    }


    /// <summary>
    /// 货物状态
    /// </summary>
    public enum OrderStatus
    {
        /// <summary>
        /// 待接收
        /// </summary>
        [Description("初始")]
        Init = 0,
        /// <summary>
        /// 待接单
        /// </summary>
        [Description("已派")]
        Wait,
        /// <summary>
        /// 接单
        /// </summary>
        [Description("接单")]
        Accept,
        /// <summary>
        /// 完成
        /// </summary>
        [Description("完成")]
        Complete,

    }

}
