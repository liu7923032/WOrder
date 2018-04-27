using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Abp.Domain.Entities.Auditing;

namespace WOrder.Domain.Entities
{
    public class WOrder_Account : FullAuditedEntity<long>
    {
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

        /// <summary>
        /// 调整未可空状态
        /// </summary>
        public int? DeptId { get; set; }

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

        /// <summary>
        /// 是否被审核通过
        /// </summary>
        public bool IsActive { get; set; }

        [StringLength(10)]
        public string Sex { get; set; }

        /// <summary>
        /// 人员照片
        /// </summary>
        [StringLength(100)]
        public string Photos { get; set; }

        /// <summary>
        /// 工作方式
        /// </summary>
        public string WorkMode { get; set; }

        /// <summary>
        /// 负责片区
        /// </summary>
        [StringLength(1000)]
        public string AreaName { get; set; }

        /// <summary>
        /// 身份证
        /// </summary>
        [StringLength(20)]
        public string IdCard { get; set; }

        /// <summary>
        /// 一个用户拥有多个角色
        /// </summary>
        [ForeignKey("UserId")]
        public virtual ICollection<Sys_UserRole> Roles { get; set; }

        public WOrder_Account()
        {
            IsLock = false;
        }

    }
}
