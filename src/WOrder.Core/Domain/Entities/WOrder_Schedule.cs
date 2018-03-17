using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Abp.Domain.Entities.Auditing;

namespace WOrder.Domain.Entities
{
    /// <summary>
    /// 排班记录表
    /// </summary>
    public class WOrder_Schedule : CreationAuditedEntity<long>
    {
        /// <summary>
        /// 排班人员
        /// </summary>
        [Required]
        public long UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual WOrder_Account User { get; set; }
        /// <summary>
        /// 年
        /// </summary>
        [Required]
        public int YFlag { get; set; }

        /// <summary>
        /// 月
        /// </summary>
        [Required]
        public int MFlag { get; set; }

        /// <summary>
        /// 日
        /// </summary>
        [Required]
        public int DFlag { get; set; }

        /// <summary>
        /// 日期字符串
        /// </summary>
        [Required]
        public DateTime ClassDate { get; set; }

        /// <summary>
        /// 班别
        /// </summary>
        [Required]
        public string ClassType { get; set; }

    }
}
