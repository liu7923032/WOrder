using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using WOrder.Domain.Entities;
using Newtonsoft.Json;

namespace WOrder.Comment
{
    [AutoMapFrom(typeof(WOrder_Comment))]
    public class CommentDto : UpdateCommentInput
    {

    }

    [AutoMapTo(typeof(WOrder_Comment))]
    public class CreateCommentInput
    {
        /// <summary>
        /// 评论的状态
        /// </summary>
        [Required]
        public CommentStatus CStatus { get; set; }
        /// <summary>
        /// 评论内容
        /// </summary>
        [Required]
        [StringLength(500)]
        public string Comment { get; set; }

        [Required]
        public int OrderId { get; set; }


        [JsonConverter(typeof(WOrderDateFormat))]
        public DateTime CreationTime { get; set; }

        /// <summary>
        /// 发表人
        /// </summary>
        public string CreatorUser { get; set; }

        /// <summary>
        /// 发表人的图片
        /// </summary>
        public string Sex { get; set; }
    }


    public class UpdateCommentInput : CreateCommentInput, IEntityDto<int>
    {
        public int Id { get; set; }
    }


    public class GetCommentsInput : PagedAndSortedResultRequestDto
    {
        public int? OrderId { get; set; }
    }
}
