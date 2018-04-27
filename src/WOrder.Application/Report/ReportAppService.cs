using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using WOrder.Domain.Entities;
using WOrder.Order;

namespace WOrder.Report
{
    public interface IReportAppService : IApplicationService
    {
        Task<PagedResultDto<UserWorkDto>> GetUserWork(GetUserWorkInput input);

        Task<PagedResultDto<TypeCount>> GetTypeCount(GetUserWorkInput input);
    }
    public class ReportAppService : WOrderAppServiceBase, IReportAppService
    {

        private IRepository<WOrder_Account, long> userRepository;
        private IRepository<WOrder_Order, long> orderRepository;
        private IRepository<WOrder_Handler> handerRepository;

        public ReportAppService(IRepository<WOrder_Account, long> userRepository,
            IRepository<WOrder_Order, long> orderRepository,
            IRepository<WOrder_Handler> handerRepository)
        {
            this.userRepository = userRepository;
            this.orderRepository = orderRepository;
            this.handerRepository = handerRepository;
        }

        private IQueryable<WOrder_Order> GetOrder(GetUserWorkInput input)
        {
            var orderData = orderRepository.GetAll().Where(u => u.TStatus == TStatus.Finish);
            if (input.SDate.HasValue)
            {
                orderData = orderData.Where(u => u.EndDate >= input.SDate.Value);
            }

            if (input.EDate.HasValue)
            {
                orderData = orderData.Where(u => u.EndDate >= input.EDate.Value.AddDays(1));
            }

            if (input.OrderType.HasValue)
            {
                orderData = orderData.Where(u => u.OrderType.Equals(input.OrderType.Value));
            }

            if (!string.IsNullOrEmpty(input.UserName))
            {
                orderData = orderData.Where(u => u.Handler.UserName.Contains(input.UserName));
            }


            if (!string.IsNullOrEmpty(input.OAdress))
            {
                orderData = orderData.Where(u => u.OAddress.Contains(input.OAdress));
            }

            return orderData;
        }

        public async Task<PagedResultDto<TypeCount>> GetTypeCount(GetUserWorkInput input)
        {
            var orderData = GetOrder(input);
            var data = from a in orderData
                       group a by new { a.OAddress } into g
                       select new TypeCount
                       {
                           Name = g.Key.OAddress,
                           Value = g.Count()
                       };
            return await Task.FromResult(new PagedResultDto<TypeCount>()
            {
                TotalCount = data.Count(),
                Items = data.ToList()
            });
        }

        public async Task<PagedResultDto<UserWorkDto>> GetUserWork(GetUserWorkInput input)
        {
            var orderData = GetOrder(input).
                                Include("Handler");
            var data = from a in orderData
                       group a by new { a.Handler.UserName, a.Handler.Position } into g
                       select new UserWorkDto
                       {
                           UserName = g.Key.UserName,
                           Position = g.Key.Position,
                           UserNum = g.Count()
                       };
            return await Task.FromResult(new PagedResultDto<UserWorkDto>()
            {
                TotalCount = data.Count(),
                Items = data.ToList()
            });
        }
    }
}
