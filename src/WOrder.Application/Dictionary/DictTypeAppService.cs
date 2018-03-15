using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using Abp.UI;
using WOrder.Domain.Entities;

namespace WOrder.Dictionary
{
    public interface IDictTypeAppService : IAsyncCrudAppService<DictTypeDto, int, GetAllDictTypeDto, CreateDictTypeDto, UpdateDictTypeDto>
    {
    }

    [AbpAuthorize]
    public class DictTypeAppService : AsyncCrudAppService<WOrder_DictType, DictTypeDto, int, GetAllDictTypeDto, CreateDictTypeDto, UpdateDictTypeDto>, IDictTypeAppService
    {

        private readonly IRepository<WOrder_DictType> _dictTypeRepository;
        private readonly IRepository<WOrder_Dictionary> _dictRespository;
        public DictTypeAppService(IRepository<WOrder_DictType> dictTypeRepository, IRepository<WOrder_Dictionary> dictRespository) : base(dictTypeRepository)
        {
            _dictTypeRepository = dictTypeRepository;
            _dictRespository = dictRespository;
        }

        protected override IQueryable<WOrder_DictType> CreateFilteredQuery(GetAllDictTypeDto input)
        {
            return base.CreateFilteredQuery(input)
                .WhereIf(!string.IsNullOrEmpty(input.Name), u => u.Name.Contains(input.Name));
        }


        private async Task CheckExists(string name)
        {
            //检查数据库中是否存在同名的字典或标号
            var exist = await _dictTypeRepository.FirstOrDefaultAsync(u => u.Name.Equals(name));
            if (exist != null)
            {
                throw new UserFriendlyException("系统中已经存在该编号或者字典名称");
            }
        }


        public async override Task<DictTypeDto> Create(CreateDictTypeDto input)
        {
            await CheckExists(input.Name);
            return await base.Create(input);
        }


        public async override Task Delete(EntityDto<int> input)
        {
            var entity = _dictTypeRepository.Get(input.Id);
            await _dictRespository.DeleteAsync(u => u.DictType.Equals(entity.Name));
            await base.Delete(input);

        }
    }
}
