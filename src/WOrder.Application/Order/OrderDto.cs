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

   
        [MaxLength(2000)]
        public string StartAddr { get; set; }

        /// <summary>
        /// 目的地
        /// </summary>
        [Required]
        [StringLength(100)]
        public string OAddress { get; set; }

        /// <summary>
        /// 运送开始
        /// </summary>
        public DateTime? SDate { get; set; }

        public DateTime? EDate { get; set; }

        //图片信息
        public string FileIds { get; set; }

        /// <summary>
        /// 来源Id
        /// </summary>
        public long? SrcId { get; set; }
    }

    /// <summary>
    /// 更新
    /// </summary>
    public class UpdateOrderDto : CreateOrderDto, IEntityDto<long>
    {
        public long Id { get; set; }
    }
    /// <summary>
    /// 返回订单列表
    /// </summary>
    /// 

    public class OrderDto : UpdateOrderDto
    {

        public CStatus CStatus { get; set; }
        /// <summary>
        /// 订单状态说明
        /// </summary>
        public string TStatusName { get; set; }
        /// <summary>
        /// 订单创建人
        /// </summary>
        public string CreatorName { get; set; }

        /// <summary>
        /// 创建人联系方式
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [JsonConverter(typeof(WOrderDateFormat))]
        [Required]
        public DateTime CreationTime { get; set; }

        ///// <summary>
        ///// 处理人
        ///// </summary>
        public string HandleName { get; set; }

        [JsonConverter(typeof(WOrderDateFormat))]
        public new DateTime? SDate { get; set; }
        [JsonConverter(typeof(WOrderDateFormat))]
        public new DateTime? EDate { get; set; }

    }

    public class GetMyOrderInput: PagedAndSortedResultRequestDto
    {

        //0是待处理的，包含待接单和待完成   1是已完成
        public int UserStatus { get; set; }


        public OStatus? OStatus { get; set; }

        /// <summary>
        /// 订单类别
        /// </summary>
        public string Category { get; set; }

        /// <summary>
        /// 是维修类的单据还是运输类的单据
        /// </summary>
        public OrderType? OrderType { get; set; }

        public string ItemName { get; set; }

        public DateTime? SDate { get; set; }

        public DateTime? EDate { get; set; }

        public CStatus? CStatus { get; set; }

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

        /// <summary>
        /// 通过创建人来查找订单
        /// </summary>
        public int? CreatorId { get; set; }

        public CStatus? CStatus { get; set; }
    }

    
}
