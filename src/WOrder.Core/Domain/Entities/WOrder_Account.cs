using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Abp.Domain.Entities.Auditing;

namespace WOrder.Domain.Entities
{
    public class WOrder_Account : AuditedEntity<long>
    {
        [Required]
        [StringLength(20)]
        public string Account { get; set; }

        [Required]
        [StringLength(10)]
        public string UserName { get; set; }


        [StringLength(40)]
        public string Phone { get; set; }

        [StringLength(40)]
        public string Email { get; set; }

        /// <summary>
        /// 岗位
        /// </summary>
        [StringLength(50)]
        public string Position { get; set; }


        [Required]
        public int DeptId { get; set; }

        [ForeignKey("DeptId")]
        public virtual WOrder_Department Department { get; set; }
        /// <summary>
        /// 个人积分
        /// </summary>
        public decimal? Integral { get; set; }

        [StringLength(100)]
        public string Password { get; set; }

        //是否被锁
        public bool IsLock { get; set; }

        [StringLength(10)]
        public string Sex { get; set; }

        /// <summary>
        /// 人员照片
        /// </summary>
        [StringLength(100)]
        public string Photos { get; set; }

        public WOrder_Account()
        {
            IsLock = false;
        }
    }
}
