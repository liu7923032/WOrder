using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.Domain.Services;
using Abp.Events.Bus;
using WOrder.Domain.Entities;
using WOrder.Domain.Events;

namespace WOrder.Domain.Service
{
    /// <summary>
    /// 
    /// </summary>
    public interface IOrderManager : IDomainService
    {
        Task UpdateOrder(WOrder_Order order, OrderStatus status);
    }


    /// <summary>
    /// 有新订单后通知对应的人
    /// </summary>
    public class OrderManager : DomainService, IOrderManager
    {
        private readonly IRepository<WOrder_ORecord> _recordRepository;
        private readonly IRepository<WOrder_Order> _cartItemRepository;
        public OrderManager(IRepository<WOrder_ORecord> recordRepository, IRepository<WOrder_Order> cartItemRepository)
        {
            _recordRepository = recordRepository;
            _cartItemRepository = cartItemRepository;
        }

        public async Task UpdateOrder(WOrder_Order order, OrderStatus status)
        {
            //1:添加订单记录
            await _recordRepository.InsertAsync(new WOrder_ORecord()
            {
                OrderId = order.Id,
                OrderStatus = status
            });
            //2:更新订单记录
            var initStatus = order.OStatus;
            order.OStatus = status;

            //3:只有在商品发货后,才进行通知和更新产品数量
            if (status == OrderStatus.Wait)
            {
                //更新订单产品的销售记录
                //var products = _cartItemRepository.GetAllIncluding((c) => c.Product).Where(u => u.CartId.Equals(order.CartId)).ToList();
                //products.ForEach(u =>
                //{
                //    u.Product.SaleNums += u.ItemNum;
                //});
                //通知提醒
                EventBus.Default.Trigger(new OrderEventData() { Order = order, OldStatus = initStatus });
            }
        }
    }

}
