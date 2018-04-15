using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using WOrder.Domain.Entities;

namespace WOrder.UserApp
{

    [AutoMapTo(typeof(WOrder_Account))]
    public class CreateUserInput
    {

        [StringLength(20)]
        public string Account { get; set; }

        [Required]
        [StringLength(10)]
        public string UserName { get; set; }

        [StringLength(20)]
        public string Email { get; set; }

        [StringLength(30)]
        public string Position { get; set; }

        [Required]
        [StringLength(13)]
        public string Phone { get; set; }
        /// <summary>
        /// 图片
        /// </summary>
        public string Photos { get; set; }

        [Required]
        [StringLength(4)]
        public string Sex { get; set; }

        /// <summary>
        /// 身份证信息
        /// </summary>
        [StringLength(20)]
        public string IdCard { get; set; }

        [Required]
        [StringLength(maximumLength: 20, ErrorMessage = "最长20,最短4", ErrorMessageResourceName = "", ErrorMessageResourceType = null, MinimumLength = 4)]
        public string Password { get; set; }

        /// <summary>
        /// 部门Id
        /// </summary>
        [Required]
        public int DeptId { get; set; }

        /// <summary>
        /// 工作方式
        /// </summary>
        public string WorkMode { get; set; }

        public string AreaName { get; set; }

        public bool IsActive { get; set; }

        public string FileIds { get; set; }
    }

    public class UpdateUserInput : CreateUserInput, IEntityDto<long>
    {
        public long Id { get; set; }
    }

    public class UserDto : UpdateUserInput
    {
        public string DeptName { get; set; }
    }
    /// <summary>
    /// 查询
    /// </summary>
    public class GetUsersInput
    {
        public string Account { get; set; }

        public string UserName { get; set; }
        /// <summary>
        /// 部门
        /// </summary>
        public int? DeptId { get; set; }

        /// <summary>
        /// 是否激活
        /// </summary>
        public bool? IsActive { get; set; }

        public string Key { get; set; }
    }

    /// <summary>
    /// 密码修改的dto
    /// </summary>
    public class ModifyPwdDto
    {
        [Required]
        public long UserId { get; set; }

        [Required]
        [StringLength(maximumLength: 4, ErrorMessage = "最长20,最短4", ErrorMessageResourceName = "", ErrorMessageResourceType = null)]
        public string OPwd { get; set; }


        [Required]
        public string CPwd { get; set; }

        [Required]
        [StringLength(maximumLength: 4, ErrorMessage = "最长20,最短4", ErrorMessageResourceName = "", ErrorMessageResourceType = null)]
        public string NPwd { get; set; }
    }
}
