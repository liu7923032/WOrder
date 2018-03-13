using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Abp.Domain.Entities.Auditing;

namespace WOrder.Domain.Entities
{

    public class WOrder_Dictionary : FullAuditedEntity
    {
        //字典类别
        [Required]
        [StringLength(100)]
        public string DictType { get; set; }

        [Required]
        [StringLength(50)]
        public string No { get; set; }

        //字典名称
        [Required]
        [StringLength(200)]
        public string Name { get; set; }

        [Required]
        public int SortNo { get; set; }

        [StringLength(2000)]
        public string Memo { get; set; }
    }
}
