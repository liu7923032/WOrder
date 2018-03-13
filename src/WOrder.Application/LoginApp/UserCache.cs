using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Abp.Dependency;
using Abp.Domain.Entities.Caching;
using Abp.Domain.Repositories;
using Abp.Runtime.Caching;
using WOrder.Cache;
using WOrder.Domain.Entities;

namespace WOrder.UserApp
{
    public interface IUserCache : IEntityCache<UserDto, long>
    {
        //Task<List<AccountDto>> GetAllUsers();
    }

    public class UserCache : EntityCache<WOrder_Account, UserDto,long>, IUserCache, ISingletonDependency
    {
        public UserCache(ICacheManager cacheManager, IRepository<WOrder_Account, long> repository) : base(cacheManager, repository)
        {

        }

        //public async Task<List<AccountDto>> GetAllUsers()
        //{

        //    _cacheManager.get
        //    throw new NotImplementedException();
        //}
    }
}
