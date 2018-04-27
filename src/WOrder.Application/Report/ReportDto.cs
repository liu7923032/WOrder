using System;
using System.Collections.Generic;
using System.Text;
using Abp.Application.Services.Dto;
using WOrder.Domain.Entities;

namespace WOrder.Report
{
    public class ReportDto
    {

    }


    public class UserWorkDto
    {
        /// <summary>
        /// 用户岗位
        /// </summary>
        public string Position { get; set; }
        /// <summary>
        /// 用户姓名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 用户完成的工作数量
        /// </summary>
        public int UserNum { get; set; }
        /// <summary>
        /// 用户耗时
        /// </summary>
        public int UserHour { get; set; }

        //总耗时
        public int AllHour { get; set; }
    }

    public class TypeCount
    {
        public string Name { get; set; }

        public int Value { get; set; }
    }

    public class GetUserWorkInput : PagedResultRequestDto
    {
        public DateTime? SDate { get; set; }

        public DateTime? EDate { get; set; }

        public string UserName { get; set; }

        public OrderType? OrderType { get; set; }

        public string OAdress { get; set; }
    }
}
