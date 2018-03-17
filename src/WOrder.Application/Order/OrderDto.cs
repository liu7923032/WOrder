using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using WOrder.Domain.Entities;
using Newtonsoft.Json;

namespace WOrder.Order
{

    [AutoMapTo(typeof(WOrder_Order))]
    public class CreateOrderDto
    {
        /// <summary>
        /// 订单编号
        /// </summary>
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
        /// 单据的类别 ,默认是0是派单，1是抢单 
        /// </summary>
        [Required]
        public OrderType OrderType { get; set; }
        /// <summary>
        /// 当前订单状态
        /// </summary>
        [Required]
        public TStatus TStatus { get; set; }
    }

    /// <summary>
    /// 更新
    /// </summary>
    public class UpdateOrderDto : CreateOrderDto, IEntityDto<int>
    {
        public int Id { get; set; }
    }
    /// <summary>
    /// 返回订单列表
    /// </summary>
    /// 

    public class OrderDto : UpdateOrderDto
    {
        /// <summary>
        /// 订单状态说明
        /// </summary>
        public string TStatusName { get; set; }
        /// <summary>
        /// 订单创建人
        /// </summary>
        public string CreatorName { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [JsonConverter(typeof(WOrderDateFormat))]
        [Required]
        public DateTime CreationTime { get; set; }

        ///// <summary>
        ///// 处理人
        ///// </summary>
        public List<HandlerDto> Handlers { get; set; }

    }



    public class GetAllOrderInput : PagedAndSortedResultRequestDto
    {
        /// <summary>
        /// 订单状态
        /// </summary>
        public TStatus? TStatus { get; set; }

        /// <summary>
        /// 订单类别
        /// </summary>
        public string Category { get; set; }

        /// <summary>
        /// 订单类别 是抢单还是派单
        /// </summary>
        public OrderType? OrderType { get; set; }

        public DateTime? SDate { get; set; }

        public DateTime? EDate { get; set; }
        /// <summary>
        /// 项目描述
        /// </summary>
        public string ItemName { get; set; }

    }

    
}
