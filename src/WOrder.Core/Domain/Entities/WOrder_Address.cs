using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Abp.Domain.Entities.Auditing;

namespace WOrder.Domain.Entities
{
    public class WOrder_Address: CreationAuditedEntity
    {
        /// <summary>
        /// 收货人
        /// </summary>
        [Required]
        [StringLength(50)]
        public string RecUser { get; set; }
        /// <summary>
        /// 省份
        /// </summary>
        [Required]
        [StringLength(10)]
        public string Province { get; set; }

        /// <summary>
        /// 城市
        /// </summary>
        [StringLength(20)]
        public string City { get; set; }

        /// <summary>
        /// 县/区域
        /// </summary>
        [Required]
        [StringLength(20)]
        public string Area { get; set; }

        /// <summary>
        /// 详细地址
        /// </summary>
        [Required]
        [StringLength(100)]
        public string DetailAddress { get; set; }

        /// <summary>
        /// 联系电话
        /// </summary>
        [Required]
        [StringLength(20)]
        public string Phone { get; set; }

        /// <summary>
        /// 邮箱通知
        /// </summary>
        [StringLength(30)]
        public string Email { get; set; }

        /// <summary>
        /// 地址别名 家,公司
        /// </summary>
        [StringLength(10)]
        public string AliasName { get; set; }

        /// <summary>
        /// 是否默认地址
        /// </summary>
        public bool IsDefault { get; set; }

        /// <summary>
        /// 隶属人员
        /// </summary>
        [ForeignKey("CreatorUserId")]
        public virtual WOrder_Account User { get; set; }


        public override string ToString()
        {
            return $"地址:{this.Province} {this.City} {this.DetailAddress},收件人:{this.RecUser},联系电话:{this.Phone}";
        }
    }
}
