using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using WOrder.Domain.Entities;

namespace WOrder.Dictionary
{
    [AutoMapTo(typeof(WOrder_Dictionary))]
    public class CreateDictDto
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

    public class UpdateDictDto : CreateDictDto, IEntityDto
    {

        public int Id { get; set; }
    }

    public class DictDto : UpdateDictDto
    {

    }

    public class GetAllDictDto: PagedAndSortedResultRequestDto
    {

        public string DictType { get; set; }
        public string No { get; set; }

        public string Name { get; set; }
    }
}
