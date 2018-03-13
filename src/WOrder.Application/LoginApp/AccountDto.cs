using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using WOrder.Domain.Entities;

namespace WOrder.UserApp
{
    [AutoMapFrom(typeof(WOrder_Account))]
    public class UserDto : UpdateUserInput
    {

    }



    [AutoMapTo(typeof(WOrder_Account))]
    public class CreateUserInput
    {

        [StringLength(20)]
        public string Account { get; set; }

        [StringLength(10)]
        public string UserName { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public string Photos { get; set; }

        public string Sex { get; set; }

        [StringLength(20)]
        public string Password { get; set; }

    }

    public class UpdateUserInput : CreateUserInput, IEntityDto<long>
    {
        public long Id { get; set; }
    }

    /// <summary>
    /// 查询
    /// </summary>
    public class GetUsersInput
    {
        public string Account { get; set; }

        public string UserName { get; set; }
    }
}
