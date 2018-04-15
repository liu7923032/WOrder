using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Abp.Domain.Entities.Auditing;

namespace WOrder.Domain.Entities
{
    /// <summary>
    /// 放置附档的位置
    /// </summary>
    public class WOrder_AttachFile: CreationAuditedEntity
    {
        /// <summary>
        /// 隶属
        /// </summary>
        public string ParentId { get; set; }

        [Required]
        public string FileName { get; set; }

        [Required]
        public string FilePath { get; set; }

        [Required]
        public string FileType { get; set; }

        [Required]
        public string ContentType { get; set; }


        public string FileSize { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Describe { get; set; }
        /// <summary>
        /// 隶属模块
        /// </summary>
        public string Module { get; set; }
    }
}
