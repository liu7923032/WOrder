using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Abp.Domain.Entities.Auditing;

namespace WOrder.Domain.Entities
{
    public class Sys_Message : AuditedEntity<long>
    {
        [Required]
        [MaxLength(200)]
        public string Title { get; set; }

        public string Content { get; set; }

        [Required]
        public bool IsRead { get; set; }

        //目标位置
        public string Target { get; set; }

        //app到哪个页面
        public string AppPage { get; set; }

        /// <summary>
        /// 消息人
        /// </summary>
        public long UserId { get; set; }

        [Required]
        public long SrcId { get; set; }

        [ForeignKey("CreatorUserId")]
        public virtual WOrder_Account Creator { get; set; }

        [ForeignKey("UserId")]
        public virtual WOrder_Account User { get; set; }



    }
}
