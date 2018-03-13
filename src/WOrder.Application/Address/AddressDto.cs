using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Entities;
using WOrder.Domain.Entities;

namespace WOrder.Address
{
    [AutoMapTo(typeof(WOrder_Address))]
    public class CreateAddressInput
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
        [Required(ErrorMessage = "省份必须填写")]
        [StringLength(10)]
        public string Province { get; set; }

        /// <summary>
        /// 城市
        /// </summary>
        [StringLength(20)]
        public string City { get; set; }

        /// <summary>
        /// 区域
        /// </summary>
        [Required(ErrorMessage = "区域必须填写")]
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
        [Required]
        public bool IsDefault { get; set; }
    }


    public class UpdateAddressInput : CreateAddressInput, IEntityDto<int>
    {
        public int Id { get; set; }
    }


    public class AddressDto : UpdateAddressInput
    {
    }


    public class GetAllAddressInput
    {
        public long? CreatorUserId { get; set; }
    }
}
