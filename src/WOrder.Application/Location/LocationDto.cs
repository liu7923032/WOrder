using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Newtonsoft.Json;
using WOrder.Domain.Entities;

namespace WOrder.Location
{
    [AutoMapTo(typeof(WOrder_Location))]
    public class CreateLocationInput
    {
        /// <summary>
        /// 人员
        /// </summary>
        [Required]
        public long UserId { get; set; }

        /// <summary>
        /// 位置
        /// </summary>
        [Required]
        public string Position { get; set; }

        /// <summary>
        /// 经度
        /// </summary>
        public double? Longitude { get; set; }

        /// <summary>
        /// 纬度
        /// </summary>
        public double? Latitude { get; set; }
    }

    public class UpdateLocationInput : CreateLocationInput, IEntityDto<long>
    {
        public long Id { get; set; }
    }


    public class LocationDto : UpdateLocationInput
    {
        [JsonConverter(typeof(WOrderDateFormat))]
        public DateTime CreationTime { get; set; }

        public string UserName { get; set; }
    }

    public class GetAllLocatinDto : PagedAndSortedResultRequestDto
    {
        public long? UserId { get; set; }

    }
}
