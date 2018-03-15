using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Events.Bus;
using Abp.Linq.Extensions;
using Abp.Net.Mail;
using Abp.Notifications;
using Abp.Runtime.Session;
using Abp.Timing;
using Abp.UI;
using WOrder.Domain.Entities;
using WOrder.Integral;
using Microsoft.EntityFrameworkCore;
using Dark.Common.Extension;
using WOrder.UserApp;
using WOrder.Domain.Events;
using WOrder.Domain.Service;
using Abp.Application.Services;

namespace WOrder.Order
{
    #region 1.0 接口 IOrderAppService
    public interface IOrderAppService : IAsyncCrudAppService<OrderDto, int, GetAllOrderInput, CreateOrderDto, UpdateOrderDto>
    {

    }
    #endregion

    #region 2.0 实现 OrderAppService
    /// <summary>
    /// 订单处理类
    /// </summary>
    public class OrderAppService : AsyncCrudAppService<WOrder_Order,OrderDto, int, GetAllOrderInput, CreateOrderDto, UpdateOrderDto>, IOrderAppService
    {

        private IRepository<WOrder_Order> _orderRepository;
        private IRepository<WOrder_ORecord> _recordRepository;
        private IRepository<WOrder_Handler> _handlerRepository;


        public OrderAppService(IRepository<WOrder_Order> orderRepository,
            IRepository<WOrder_Handler> handlerRepository,
            IRepository<WOrder_ORecord> recordRepository
          ):base(orderRepository)
        {
            _orderRepository = orderRepository;
            _handlerRepository = handlerRepository;
            _recordRepository = recordRepository;

        }


    }
    #endregion

}
