using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Abp.Domain.Entities.Auditing;

namespace WOrder.Domain.Entities
{
    public class WOrder_Department : FullAuditedEntity
    {

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        /// <summary>
        /// 部门编码
        /// </summary>
        [Required]
        [StringLength(200)]
        public string DeptNo { get; set; }

        /// <summary>
        /// 拼音
        /// </summary>
        [StringLength(50)]
        public string InputCode { get; set; }

        [StringLength(300)]
        public string Position { get; set; }

        public virtual ICollection<WOrder_Account> Accounts { get; set; }
    }
}
