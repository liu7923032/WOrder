using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

        [StringLength(50)]
        public string Email { get; set; }

        [StringLength(40)]
        public string Phone { get; set; }

        /// <summary>
        /// 个人积分
        /// </summary>
        public decimal? Integral { get; set; }

        [StringLength(100)]
        public string Password { get; set; }

        public bool IsActive { get; set; }

        public bool IsLock { get; set; }

        [StringLength(10)]
        public string Sex { get; set; }

        [StringLength(100)]
        public string Photos { get; set; }

        public WOrder_Account()
        {
            IsActive = true;
            IsLock = false;
        }
    }
}
