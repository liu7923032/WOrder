using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Entities;
using WOrder.Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace WOrder.File
{
    [AutoMap(typeof(WOrder_AttachFile))]
    public class CreateFileInput
    {
        [Required]
        public string FileName { get; set; }

        [Required]
        public string FilePath { get; set; }

        [Required]
        public string FileType { get; set; }

        [Required]
        public string ContentType { get; set; }

        public string FileSize { get; set; }

        public string Describe { get; set; }

        public string Module { get; set; }

    }

    public class UpdateFileInput : CreateFileInput, IEntityDto<int>
    {
        public int Id { get; set; }
    }

    public class FileDto : UpdateFileInput
    {

    }


    public class GetFilesInput
    {
        public string PId { get; set; }

        public string Module { get; set; }
    }
}
