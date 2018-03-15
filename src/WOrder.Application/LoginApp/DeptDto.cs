using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using WOrder.Domain.Entities;

namespace WOrder.LoginApp
{
    [AutoMapTo(typeof(WOrder_Department))]
    public class CreateDeptDto
    {
        [Required]
        [StringLength(50,ErrorMessage ="长度最大50长度")]
        public string Name { get; set; }

        /// <summary>
        /// 部门编码
        /// </summary>
        [Required]
        [StringLength(50, ErrorMessage = "长度最大50长度")]
        public string DeptNo { get; set; }

        /// <summary>
        /// 拼音
        /// </summary>
        [StringLength(50)]
        public string InputCode { get; set; }


        [StringLength(300, ErrorMessage = "长度最大300长度")]
        public string Position { get; set; }

    }


    public class UpdateDeptDto : CreateDeptDto, IEntityDto<int>
    {
        public int Id { get; set; }
    }


    public class DeptDto : UpdateDeptDto
    {

    }

    public class GetAllDeptDto : PagedAndSortedResultRequestDto
    {
        public string Name { get; set; }
    }
}
