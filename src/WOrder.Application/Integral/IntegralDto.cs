using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using WOrder.Domain.Entities;
using Newtonsoft.Json;

namespace WOrder.Integral
{

    [AutoMapTo(typeof(WOrder_Integral))]
    public class CreateIntegralInput
    {
        /// <summary>
        /// 花费类别
        /// </summary>
        [Required]
        public CostType CostType { get; set; }

        /// <summary>
        /// 当前积分
        /// </summary>
        public decimal? Current { get; set; }

        /// <summary>
        /// 消费积分
        /// </summary>
        [Required]
        [Range(1, Int32.MaxValue, ErrorMessage = "你好抠!!!积分最小也要1积分吧")]
        public decimal Integral { get; set; }


        /// <summary>
        /// 消费人员或者部门
        /// </summary>
        [Required]
        public long UserId { get; set; }

        /// <summary>
        /// 部门
        /// </summary>
        [Required]
        public int DeptId { get; set; }

        [Required]
        public string TypeName { get; set; }
        /// <summary>
        /// 说明
        /// </summary>
        public string Describe { get; set; }

        /// <summary>
        /// 积分发生的时间
        /// </summary>
        [Required]
        [JsonConverter(typeof(WOrderDateFormat))]
        public DateTime ActDate { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        public int? CreatorUserId { get; set; }

        public string ValidationCode { get; set; }

    }


    public class UpdateIntegralInput : CreateIntegralInput, IEntityDto<int>
    {
        public int Id { get; set; }
    }


    public class IntegralDto : UpdateIntegralInput
    {

    }

    /// <summary>
    /// 通过人员来获取积分
    /// </summary>
    public class GetIntegralsInput : PagedAndSortedResultRequestDto
    {
        //通过人员来获取积分
        public int? UserId { get; set; }
    }


    public class GiveIntegralInput
    {
        /// <summary>
        /// 
        /// </summary>
        [Required]
        public long UserId { get; set; }
        /// <summary>
        /// 积分
        /// </summary>
        [Required]
        [Range(1, Int32.MaxValue, ErrorMessage = "你好抠!!!积分最小也要1积分吧")]
        public decimal Integral { get; set; }

        public string Describe { get; set; }


    }
}
