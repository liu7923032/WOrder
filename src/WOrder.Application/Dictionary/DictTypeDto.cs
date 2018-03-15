using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using WOrder.Domain.Entities;

namespace WOrder.Dictionary
{
    [AutoMapTo(typeof(WOrder_DictType))]
    public class CreateDictTypeDto
    {
        //字典名称
        [Required]
        [StringLength(200)]
        public string Name { get; set; }

        [Required]
        public int SortNo { get; set; }

        [StringLength(2000)]
        public string Memo { get; set; }
    }

    public class UpdateDictTypeDto : CreateDictTypeDto, IEntityDto
    {

        public int Id { get; set; }
    }

    public class DictTypeDto : UpdateDictTypeDto
    {

    }

    public class GetAllDictTypeDto : PagedAndSortedResultRequestDto
    {
        public string No { get; set; }

        public string Name { get; set; }
    }
}
