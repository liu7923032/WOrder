using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Abp.Domain.Entities.Auditing;

namespace WOrder.Domain.Entities
{
    public class WOrder_Integral : CreationAuditedEntity
    {
        /// <summary>
        /// 花费
        /// </summary>
        [Required]
        public CostType CostType { get; set; }

        /// <summary>
        /// 当前积分
        /// </summary>
        [Required]
        public decimal Current { get; set; }

        [Required]
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
        public DateTime ActDate { get; set; }

        [ForeignKey("UserId")]
        public virtual WOrder_Account Account { get; set; }
    }


    public enum CostType
    {
        /// <summary>
        /// 获得
        /// </summary>
        Earn,
        /// <summary>
        /// 花费
        /// </summary>
        Cost
    }
}
