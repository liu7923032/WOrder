using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Abp.Domain.Entities.Auditing;

namespace WOrder.Domain.Entities
{
    /// <summary>
    /// 基础分类
    /// </summary>
    public class WOrder_DictType : AuditedEntity
    {
        [Required]
        [StringLength(50)]
        public string Name { get; set; }



        [StringLength(1000)]
        public string Memo { get; set; }

        /// <summary>
        /// 用于显示排序
        /// </summary>
        public int SortNo { get; set; }

    }
}
