using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Abp.Domain.Entities.Auditing;

namespace WOrder.Domain.Entities
{
    public class WOrder_Location : CreationAuditedEntity<long>
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
}
