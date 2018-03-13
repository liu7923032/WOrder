using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using Abp.Timing;
using Abp.UI;
using WOrder.Domain.Entities;

namespace WOrder.Integral
{
    #region 1.0 接口抽象
    public interface IIntegralAppService : IApplicationService
    {
        PagedResultDto<IntegralDto> GetAll(GetIntegralsInput input);
        Task<IntegralDto> Create(CreateIntegralInput input);

        Task GiveIntegral(GiveIntegralInput input);
    }
    #endregion


    #region 2.0 具体实现
    public class IntegralAppService : WOrderAppServiceBase, IIntegralAppService
    {

        private IRepository<WOrder_Account, long> _accountRepository;
        private IRepository<WOrder_Integral> _integralRepository;

        public IntegralAppService(IRepository<WOrder_Integral> integralRepository, IRepository<WOrder_Account, long> accountRepository)
        {
            this._accountRepository = accountRepository;
            this._integralRepository = integralRepository;
        }

        /// <summary>
        /// 对userId进行加密
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        /// 
        [AbpAllowAnonymous]
        public async Task<IntegralDto> Create(CreateIntegralInput input)
        {
            if (input.ValidationCode != WOrderConsts.SecurityCode)
            {
                throw new UserFriendlyException("请不要捣乱了");
            }

            input.Describe = CheckJsAndProcess(input.Describe);

            //找到用户的Id
            var user = await _accountRepository.GetAsync(input.UserId);
            //设置积分
            input.Current = user.Integral.Value;
            //更新该人员的主要积分信息
            if (input.CostType == CostType.Earn)
            {
                user.Integral += input.Integral;
            }
            else
            {
                user.Integral -= input.Integral;
            }
            var result = await _integralRepository.InsertAsync(input.MapTo<WOrder_Integral>());
            return result.MapTo<IntegralDto>();
        }

        public PagedResultDto<IntegralDto> GetAll(GetIntegralsInput input)
        {
            var query = _integralRepository.GetAll().Where(u => u.UserId.Equals(UserId));

            var pageData = query.OrderByDescending(u => u.CreationTime).Skip(input.SkipCount).Take(input.MaxResultCount);


            return new PagedResultDto<IntegralDto>() { Items = pageData.ToList().MapTo<List<IntegralDto>>(), TotalCount = query.Count() };
        }

        /// <summary>
        /// 赠送积分给人员
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task GiveIntegral(GiveIntegralInput input)
        {
            //1.找到朋友
            var friend = await _accountRepository.GetAsync(input.UserId);


            var owner = await _accountRepository.GetAsync(UserId);

            //3.积分不可为负
            if (input.Integral < 0)
            {
                throw new UserFriendlyException("积分不可小于0");
            }

            //4.不可将积分转给自己
            if (input.UserId == UserId)
            {
                throw new UserFriendlyException("不用太无聊");
            }

            //5.检查一下自己的积分是否足够
            if (owner.Integral < input.Integral)
            {
                throw new UserFriendlyException("可用积分不够");
            }

            //6.去掉自己的积分
            string desc = $"积分转移 {owner.UserName}->{friend.UserName} ,备注:{input.Describe}";
            await this.Create(new CreateIntegralInput()
            {
                ValidationCode = WOrderConsts.SecurityCode,
                Current = owner.Integral.Value,
                Integral = input.Integral,
                UserId = UserId,
                CostType = CostType.Cost,
                DeptId = 0,
                ActDate = Clock.Now,
                Describe = desc,
                TypeName = "人员"
            });

            //7.添加人员积分
            await this.Create(new CreateIntegralInput()
            {
                ValidationCode = WOrderConsts.SecurityCode,
                Current = friend.Integral.Value,
                Integral = input.Integral,
                UserId = input.UserId,
                CostType = CostType.Earn,
                DeptId = 0,
                ActDate = Clock.Now,
                Describe = desc,
                TypeName = "人员"
            });
        }
    }
    #endregion

}
