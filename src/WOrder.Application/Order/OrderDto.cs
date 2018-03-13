using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using WOrder.Domain.Entities;
using Newtonsoft.Json;

namespace WOrder.Order
{
    /// <summary>
    /// 返回订单列表
    /// </summary>
    /// 
    [AutoMapFrom(typeof(WOrder_Order))]
    public class OrderDto : IEntityDto<int>
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public int CartId { get; set; }

        [Required]
        public string OrderNo { get; set; }

        /// <summary>
        /// 订单状态
        /// </summary>
        public OrderStatus OrderStatus { get; set; }


        [JsonConverter(typeof(WOrderDateFormat))]
        [Required]
        public DateTime CreationTime { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        public string CreatorName { get; set; }

        /// <summary>
        /// 日期格式话
        /// </summary>
        [JsonConverter(typeof(WOrderDateFormat))]
        public DateTime? ApproveTime { get; set; }
        /// <summary>
        /// 审核人
        /// </summary>
        public string ApproveUName { get; set; }

        [Required]
        public decimal AllPrice { get; set; }


        public string AddressInfo { get; set; }
    }

    /// <summary>
    /// 对订单状态进行变更
    /// </summary>
    public class ChangeOrderInput
    {
        public List<int> OrderIds { get; set; }

        public OrderStatus OrderStatus { get; set; }
    }

    public class GetAllOrderInput : PagedAndSortedResultRequestDto
    {
        public OrderStatus OrderStatus { get; set; }

        public OrderType OrderType { get; set; }
    }

    /// <summary>
    /// 用于查询订单状态
    /// </summary>
    public enum OrderType
    {
        /// <summary>
        /// 查询我的订单
        /// </summary>
        Me = 0,
        /// <summary>
        /// 查询所有订单
        /// </summary>
        All
    }
}
