using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using Abp.UI;
using WOrder.Domain.Entities;

namespace WOrder.Dictionary
{

    public interface IDictionaryAppService : IAsyncCrudAppService<DictDto, int, GetAllDictDto, CreateDictDto, UpdateDictDto>
    {

    }

    [AbpAuthorize]
    public class DictionaryAppService : AsyncCrudAppService<WOrder_Dictionary, DictDto, int, GetAllDictDto, CreateDictDto, UpdateDictDto>, IDictionaryAppService
    {

        private readonly IRepository<WOrder_Dictionary> _dictRepository;

        public DictionaryAppService(IRepository<WOrder_Dictionary> dictRepository) : base(dictRepository)
        {
            _dictRepository = dictRepository;
        }

        protected override IQueryable<WOrder_Dictionary> CreateFilteredQuery(GetAllDictDto input)
        {
            return base.CreateFilteredQuery(input)
                .WhereIf(!string.IsNullOrEmpty(input.DictType), u => u.DictType.Contains(input.DictType))
                .WhereIf(!string.IsNullOrEmpty(input.No), u => u.No.Contains(input.No))
                .WhereIf(!string.IsNullOrEmpty(input.Name), u => u.Name.Contains(input.Name));
        }

        public async override Task<DictDto> Create(CreateDictDto input)
        {

            await CheckExists(null, input.Name, input.No);
            return await base.Create(input);
        }

        private async Task CheckExists(int? id, string name, string no)
        {
            //检查数据库中是否存在同名的字典或标号
            var exist = await _dictRepository.FirstOrDefaultAsync(u => (u.No.Equals(no) || u.Name.Equals(name)) && u.Id != id);
            if (exist != null)
            {
                throw new UserFriendlyException("系统中已经存在该编号或者字典名称");
            }
        }

        public async override Task<DictDto> Update(UpdateDictDto input)
        {
            await CheckExists(input.Id, input.Name, input.No);
            return await base.Update(input);
        }
    }
}
