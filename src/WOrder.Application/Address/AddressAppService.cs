using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.Linq.Extensions;
using WOrder.Domain.Entities;

namespace WOrder.Address
{
    #region 1.0 抽象接口
    public interface IAddressAppService : IAsyncCrudAppService<AddressDto, int, GetAllAddressInput, CreateAddressInput, UpdateAddressInput>
    {
        Task SetDefault(int id);
    }

    #endregion

    #region 2.0 具体实现
    [AbpAuthorize]
    public class AddressAppService : AsyncCrudAppService<WOrder_Address, AddressDto, int, GetAllAddressInput, CreateAddressInput, UpdateAddressInput>, IAddressAppService
    {
        private IRepository<WOrder_Address> _addressRepository;
        public AddressAppService(IRepository<WOrder_Address> addressRepository) : base(addressRepository)
        {
            _addressRepository = addressRepository;
        }

        /// <summary>
        /// 检查如果是默认地址,那么就处理下
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public override async Task<AddressDto> Create(CreateAddressInput input)
        {
            //如果存在默认地址了,那么就将其他状态变更为非默认地址
            if (input.IsDefault)
            {
                var userId = AbpSession.UserId.Value;
                //那么就检查其他是否有默认地址,如果有的话,那么就将状态变更为正确
                var existEntity = await _addressRepository.FirstOrDefaultAsync(u => u.CreatorUserId.Value.Equals(userId) && u.IsDefault);
                if (existEntity != null)
                {
                    existEntity.IsDefault = false;
                }
            }
            return await base.Create(input);
        }

        public async Task SetDefault(int id)
        {
            //1：找到地址
            var addr = await _addressRepository.GetAsync(id);
            //2: 设定
            addr.IsDefault = true;
            var userId = AbpSession.UserId.Value;
            //3: 找到其他的将他们都变成非默认的
            var otherAddress = await _addressRepository.GetAllListAsync(u => u.CreatorUserId.Value.Equals(userId) && u.IsDefault && u.Id != id);
            otherAddress.ForEach(u =>
            {
                u.IsDefault = false;
            });
        }

        protected override IQueryable<WOrder_Address> CreateFilteredQuery(GetAllAddressInput input)
        {
            var userId = input.CreatorUserId.HasValue ? AbpSession.UserId.Value : input.CreatorUserId.Value;
            //只查自己的地址
            return base.CreateFilteredQuery(input).Where(u => u.CreatorUserId.Value.Equals(userId));
        }

    }
    #endregion
}
