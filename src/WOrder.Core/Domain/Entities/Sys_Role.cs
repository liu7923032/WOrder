using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Abp.Domain.Entities.Auditing;

namespace WOrder.Domain.Entities
{
    public class Sys_Role:CreationAuditedEntity
    {
        [MaxLength(500)]
        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        /// <summary>
        /// 每个角色拥有多个用户
        /// </summary>
        [ForeignKey("RoleId")]
        public virtual ICollection<Sys_UserRole> Users { get; set; }

    }
}
