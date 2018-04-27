using System;
using System.Collections.Generic;
using System.Text;
using Abp.Domain.Entities.Auditing;

namespace WOrder.Domain.Entities
{
    public class Sys_UserRole:CreationAuditedEntity
    {
        public long UserId { get; set; }

        public int RoleId { get; set; }


    }
}
