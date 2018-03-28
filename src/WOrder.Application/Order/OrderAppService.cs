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
using Abp.Authorization;
using Abp.Domain.Uow;
using WOrder.Extension;

namespace WOrder.Order
{
    #region 1.0 接口 IOrderAppService
    public interface IOrderAppService : IAsyncCrudAppService<OrderDto, int, GetAllOrderInput, CreateOrderDto, UpdateOrderDto>
    {

        #region 1.0 指派任务,接单，完成订单
        /// <summary>
        /// 将订单指派给具体的人员
        /// </summary>
        /// <param name="handlers"></param>
        /// <returns></returns>
        Task Assign(List<CreateHandlerDto> handlers);

        /// <summary>
        /// 接单
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        Task Accept(int orderId);

        /// <summary>
        /// 完成订单
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        Task Finish(int orderId);

        #endregion

        #region 2.0 抢单
        /// <summary>
        /// 将正常的单子调整位抢单状态
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        Task ChangeToRob(List<int> ids);
        /// <summary>
        /// 抢单
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        Task Robber(int orderId);
        #endregion

    }
    #endregion

    #region 2.0 实现 OrderAppService
    /// <summary>
    /// 订单处理类
    /// </summary>
    [AbpAuthorize]
    public class OrderAppService : AsyncCrudAppService<WOrder_Order, OrderDto, int, GetAllOrderInput, CreateOrderDto, UpdateOrderDto>, IOrderAppService
    {

        private readonly IRepository<WOrder_Order> _orderRepository;
        private readonly IRepository<WOrder_ORecord> _recordRepository;
        private readonly IRepository<WOrder_Handler> _handlerRepository;
        private readonly IRepository<WOrder_AttachFile> _fileRepository;

        private readonly JPushHelper _jpushHelper;

        public OrderAppService(IRepository<WOrder_Order> orderRepository,
            IRepository<WOrder_Handler> handlerRepository,
            IRepository<WOrder_ORecord> recordRepository,
            IRepository<WOrder_AttachFile> fileRepository,
            JPushHelper jpushHelper
          ) : base(orderRepository)
        {
            _orderRepository = orderRepository;
            _handlerRepository = handlerRepository;
            _recordRepository = recordRepository;
            _fileRepository = fileRepository;
            _jpushHelper = jpushHelper;
        }




        #region 1.0 增删改查
        protected override IQueryable<WOrder_Order> CreateFilteredQuery(GetAllOrderInput input)
        {
            return base.CreateFilteredQuery(input)
                //分类
                .WhereIf(!string.IsNullOrEmpty(input.Category), u => u.Category.Equals(input.Category))
                //项目名称
                .WhereIf(!string.IsNullOrEmpty(input.ItemName), u => u.ItemName.Contains(input.ItemName))
                //临时任务和抢单任务
                .WhereIf(input.OrderType.HasValue, u => u.OrderType.Equals(input.OrderType.Value))
                //任务状态
                .WhereIf(input.TStatus.HasValue, u => u.TStatus.Equals(input.TStatus.Value))
                .WhereIf(input.SDate.HasValue, u => u.CreationTime >= input.SDate.Value)
                 .WhereIf(input.EDate.HasValue, u => u.CreationTime <= input.EDate.Value.AddDays(1))
                .Include("CreatorUser")
                .Include("Handlers").Include("Handlers.Handler");
        }

        /// <summary>
        /// 自动生成订单
        /// </summary>
        /// <returns></returns>
        private string GenerateOrderNo()
        {
            //取消
            using (CurrentUnitOfWork.DisableFilter(AbpDataFilters.SoftDelete))
            {
                int curYear = DateTime.Now.Year, curMonth = DateTime.Now.Month;
                var counts = _orderRepository.Count(u => u.CreationTime.Year.Equals(curYear) && u.CreationTime.Month.Equals(curMonth));
                //1：通过日期来
                return $"MO{DateTime.Now.ToString("yyyyMM")}{(counts + 1).ToString().PadLeft(5, '0')}";
            }
        }

        public async override Task<OrderDto> Create(CreateOrderDto input)
        {
            input.OrderNo = GenerateOrderNo();
            WOrder_Order order = input.MapTo<WOrder_Order>();
            await _orderRepository.InsertAsync(order);
            await CurrentUnitOfWork.SaveChangesAsync();
            if (!string.IsNullOrEmpty(input.FileIds))
            {
                string[] strFileIds = input.FileIds.Split(',');
                List<int> fileIds = new List<int>();
                foreach (var item in strFileIds)
                {
                    fileIds.Add(Convert.ToInt32(item));
                }
                var fileList = await _fileRepository.GetAllListAsync(u => fileIds.Contains(u.Id));
                fileList.ForEach(u =>
                {
                    u.ParentId = order.Id.ToString();
                });
            }
            return await Task.FromResult(order.MapTo<OrderDto>());
        }
        #endregion

        #region 2.0 派单

        /// <summary>
        /// 给订单指派人
        /// </summary>
        /// <param name="handlers"></param>
        /// <returns></returns>
        public async Task Assign(List<CreateHandlerDto> handlers)
        {
            var orderIds = handlers.Select(u => u.OrderId).ToList();
            var orderList = await _orderRepository.GetAllListAsync(u => orderIds.Contains(u.Id));
            if (orderList.Count == 0)
            {
                throw new UserFriendlyException("请选择要反派的订单");
            }
            //将订单设置进程中
            orderList.ForEach(u =>
            {
                u.TStatus = TStatus.Running;
            });

            //生成消息类容
            var strData = string.Join(',', orderList.Select(u => u.Category));


            //添加抢单人
            handlers.ForEach(async u =>
           {
               //添加人员
               var newEntity = u.MapTo<WOrder_Handler>();
               u.OStatus = OStatus.Init;
               _handlerRepository.Insert(newEntity);

               //将新增记录保存
               UnitOfWorkManager.Current.SaveChanges();
               //添加记录
               await InsertRecordAsync(newEntity);
               //执行推送消息
               await _jpushHelper.PushToAlias($"新任务来了", strData, u.HandleId.ToString());
           });

        }

        /// <summary>
        /// 获取当前待处理人
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        private async Task<WOrder_Handler> GetHandler(int orderId)
        {
            var userId = AbpSession.UserId.Value;
            return await _handlerRepository.FirstOrDefaultAsync(u => u.OrderId.Equals(orderId) && u.HandleId.Equals(userId));
        }

        /// <summary>
        /// 新增人员的状态记录
        /// </summary>
        /// <param name="handler"></param>
        /// <returns></returns>
        private async Task InsertRecordAsync(WOrder_Handler handler)
        {
            WOrder_ORecord record = new WOrder_ORecord();
            record.HandlerId = handler.Id;
            record.OStatus = handler.OStatus;
            await _recordRepository.InsertAsync(record);
        }

        /// <summary>
        /// 接单
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public async Task Accept(int orderId)
        {
            //1:找到订单处理人
            var handler = await GetHandler(orderId);
            //2：接单
            handler.OStatus = OStatus.Accept;
            //3:添加记录
            await InsertRecordAsync(handler);
        }


        public async Task Finish(int orderId)
        {
            //1:找到订单处理人
            var handler = await GetHandler(orderId);
            //2：接单
            handler.OStatus = OStatus.Finish;
            //3:添加记录
            await InsertRecordAsync(handler);

            //4:将任务主记录更新
            var order = await _orderRepository.GetAsync(orderId);
            order.TStatus = TStatus.Finish;

            await UnitOfWorkManager.Current.SaveChangesAsync();

            //5.看看后面是否需要通知管理员
        }


        #endregion

        #region 3.0 抢单处理
        /// <summary>
        /// 将普通的未处理的派工单变更为抢单
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public async Task ChangeToRob(List<int> ids)
        {
            var orderList = await _orderRepository.GetAllListAsync(u => ids.Contains(u.Id) && u.OrderType.Equals(OrderType.Dispatch) && u.TStatus.Equals(TStatus.Init));
            if (orderList.Count == 0)
            {
                throw new UserFriendlyException("任务状态只有同时是未处理才能进入抢单");
            }
            orderList.ForEach(u =>
            {
                u.OrderType = OrderType.Rob;
            });

        }

        /// <summary>
        /// 抢单
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public async Task Robber(int orderId)
        {
            //1：检查订单状态
            var order = await _orderRepository.GetAsync(orderId);
            if (order.TStatus != TStatus.Init)
            {
                throw new UserFriendlyException("订单被抢啦,下次加油...");
            }

            //2：分配记录
            await Assign(new List<CreateHandlerDto>()
            {
                new CreateHandlerDto()
                {
                    HandleId=AbpSession.GetUserId(),
                    OrderId=orderId,
                    OStatus=OStatus.Init
                }
            });
        }
        #endregion


    }
    #endregion

}
