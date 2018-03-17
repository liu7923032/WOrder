using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Authorization;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using Abp.Timing;
using Microsoft.AspNetCore.Http;
using WOrder.Domain.Entities;

namespace WOrder.File
{
    public interface IFileAppService : IAsyncCrudAppService<FileDto, int, GetFilesInput, CreateFileInput, UpdateFileInput>
    {
        /// <summary>
        /// 通过parentId来获取所有的文件信息
        /// </summary>
        /// <param name="pId"></param>
        /// <returns></returns>
        Task<List<FileDto>> GetFilesById(int pId);

        
    }

    [AbpAuthorize]
    public class FileAppService : AsyncCrudAppService<WOrder_AttachFile, FileDto, int, GetFilesInput, CreateFileInput, UpdateFileInput>, IFileAppService
    {
        private IRepository<WOrder_AttachFile> _fileRepository;
        public FileAppService(IRepository<WOrder_AttachFile> fileRepository) : base(fileRepository)
        {
            _fileRepository = fileRepository;
        }

        /// <summary>
        /// 通过parentId来获取所有的文件信息
        /// </summary>
        /// <param name="pId"></param>
        /// <returns></returns>
        public async Task<List<FileDto>> GetFilesById(int pId)
        {
            var data = this.CreateFilteredQuery(new GetFilesInput() { ParentId = pId.ToString() }).ToList().MapTo<List<FileDto>>();
            return await Task.FromResult(data);
        }

        protected override IQueryable<WOrder_AttachFile> CreateFilteredQuery(GetFilesInput input)
        {
            return base.CreateFilteredQuery(input)
                .WhereIf(!string.IsNullOrEmpty(input.ParentId), u => u.ParentId.Equals(input.ParentId));
        }
    }
}
