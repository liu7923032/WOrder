using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Newtonsoft.Json;
using WOrder.Domain.Entities;

namespace WOrder.Schedule
{
    [AutoMapTo(typeof(WOrder_Schedule))]
    public class CreateScheduleDto
    {
        /// <summary>
        /// 年
        /// </summary>
        public int YFlag { get; set; }

        /// <summary>
        /// 月
        /// </summary>
        public int MFlag { get; set; }

        /// <summary>
        /// 日
        /// </summary>
        public int? DFlag { get; set; }

        /// <summary>
        /// 排班人员
        /// </summary>
        [Required]
        public long UserId { get; set; }

       
        /// <summary>
        /// 班别
        /// </summary>
        [Required]
        [StringLength(20, ErrorMessage = "班别字符长度不可超过20")]
        public string ClassType { get; set; }

        /// <summary>
        /// 工作描述
        /// </summary>
        [StringLength(200)]
        public string Description { get; set; }
    }

    public class UpdateScheduleDto : CreateScheduleDto, IEntityDto<long>
    {
        public long Id { get; set; }
    }

    public class ScheduleDto : UpdateScheduleDto
    {
        public string UserName { get; set; }

        public string AreaName { get; set; }

        public string WorkMode { get; set; }

        public int Week { get; set; }

        /// <summary>
        /// 日期字符串
        /// </summary>
        [Required]
        [JsonConverter(typeof(WOrderDateFormat))]
        public DateTime ClassDate { get; set; }
    }


    public class GetAllScheduleDto : PagedAndSortedResultRequestDto
    {
        public int YFlag { get; set; }

        public int MFlag { get; set; }

        public string ClassType { get; set; }

        public long? UserId { get; set; }
    }

    /// <summary>
    /// 批量保存
    /// </summary>
    [AutoMapTo(typeof(WOrder_Schedule))]
    public class BatchSaveDto
    {
        [Required]
        public int YFlag { get; set; }

        [Required]
        [Range(1, 12)]
        public int MFlag { get; set; }

        [Required]
        public long UserId { get; set; }

        public DateTime SDate { get; set; }

        public DateTime EDate { get; set; }

        public string ClassType { get; set; }

        public string Description { get; set; }
          
    }

    public class UserDayDto
    {

        public long Id { get; set; }

        public string ClassType { get; set; }
        /// <summary>
        /// 天
        /// </summary>
        public int DFlag { get; set; }
        /// <summary>
        /// 工作事项
        /// </summary>
        public string Description { get; set; }
    }


    public class GetScheduleDto
    {
        public long UserId { get; set; }

        public string UserName { get; set; }

        public int YFlag { get; set; }

        public int MFlag { get; set; }

        public string AreaName { get; set; }

        public string WorkMode { get; set; }

        public string Position { get; set; }

        public List<UserDayDto> UserDays { get; set; }
    }
}
