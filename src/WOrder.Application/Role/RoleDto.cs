using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using WOrder.Domain.Entities;

namespace WOrder.Role
{

    [AutoMapTo(typeof(Sys_Role))]
    public class CreateRoleInput
    {
        [StringLength(50)]
        [Required]
        public string Name { get; set; }

        [StringLength(500)]
        public string Description { get; set; }
    }


    public class UpdateRoleInput : CreateRoleInput, IEntityDto<int>
    {
        public int Id { get; set; }
    }


    public class RoleDto : UpdateRoleInput
    {

    }


    public class UserRoleDto : IEntityDto<int>
    {
        /// <summary>
        /// userRoleId
        /// </summary>
        public int Id { get; set; }

        public long UserId { get; set; }

        public string UserName { get; set; }

        public string Phone { get; set; }

        public string Account { get; set; }

        public string Position { get; set; }

        public string Sex { get; set; }

        public string DeptName { get; set; }
    }


    /// <summary>
    /// 不进行分页查询
    /// </summary>
    public class GetAllRoleInput
    {
        public string Name { get; set; }


    }

    public class GetUsersByRole
    {
        public int RoleId { get; set; }

        public string Key { get; set; }

        public int? SkipCount { get; set; }

        public int? MaxResultCount { get; set; }
    }

}
