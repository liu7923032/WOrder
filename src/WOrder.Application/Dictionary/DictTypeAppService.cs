using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Abp.Application.Services;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using WOrder.Domain.Entities;

namespace WOrder.Dictionary
{
    public interface IDictTypeAppService : IAsyncCrudAppService<DictTypeDto, int, GetAllDictTypeDto, CreateDictTypeDto, UpdateDictTypeDto>
    {
    }

    public class DictTypeAppService : AsyncCrudAppService<WOrder_DictType, DictTypeDto, int, GetAllDictTypeDto, CreateDictTypeDto, UpdateDictTypeDto>, IDictTypeAppService
    {

        private readonly IRepository<WOrder_DictType> _dictTypeRepository;
        public DictTypeAppService(IRepository<WOrder_DictType> dictTypeRepository) : base(dictTypeRepository)
        {
            _dictTypeRepository = dictTypeRepository;
        }

        protected override IQueryable<WOrder_DictType> CreateFilteredQuery(GetAllDictTypeDto input)
        {
            return base.CreateFilteredQuery(input)
                .WhereIf(string.IsNullOrEmpty(input.Name), u => u.Name.Contains(input.Name));
        }
       
    }
}
