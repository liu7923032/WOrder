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
    public interface IOrderAppService : IAsyncCrudAppService<OrderDto, long, GetAllOrderInput, CreateOrderDto, UpdateOrderDto>
    {

        Task<PagedResultDto<OrderDto>> GetMyOrders(GetMyOrderInput input);
        #region 1.0 指派任务,接单，完成订单
        /// <summary>
        /// 将订单指派给具体的人员
        /// </summary>
        /// <param name="handlers"></param>
        /// <returns></returns>
        Task<bool> Assign(List<CreateHandlerDto> handlers);

        /// <summary>
        /// 接单
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        Task<bool> Accept(long orderId);

        /// <summary>
        /// 完成订单
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        Task<bool> Finish(long orderId);

        #endregion

        #region 2.0 抢单
        /// <summary>
        /// 将正常的单子调整位抢单状态
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        Task<bool> ChangeToRob(List<long> ids);
        /// <summary>
        /// 抢单
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        Task<bool> Robber(long orderId);
        #endregion


        #region 3.0 数据统计
        //待待处理流程数
        Task<int> GetTStatusCount(TStatus status);

        //最近一周完成统计

        //最近一周完成分类统计
        Task<List<NameValueDto>> GetCategoryCount(GetDateInput input);

        #endregion
    }
    #endregion

    #region 2.0 实现 OrderAppService
    /// <summary>
    /// 订单处理类
    /// </summary>
    [AbpAuthorize]
    public class OrderAppService : AsyncCrudAppService<WOrder_Order, OrderDto, long, GetAllOrderInput, CreateOrderDto, UpdateOrderDto>, IOrderAppService
    {

        private readonly IRepository<WOrder_Order, long> _orderRepository;
        private readonly IRepository<WOrder_ORecord> _recordRepository;
        private readonly IRepository<WOrder_Handler> _handlerRepository;
        private readonly IRepository<WOrder_AttachFile> _fileRepository;
        private readonly JPushHelper _jpushHelper;
        private readonly IRealTimeNotifier _realTimeNotifier;


        public OrderAppService(IRepository<WOrder_Order, long> orderRepository,
            IRepository<WOrder_Handler> handlerRepository,
            IRepository<WOrder_ORecord> recordRepository,
            IRepository<WOrder_AttachFile> fileRepository,
            JPushHelper jpushHelper,
            IRealTimeNotifier realTimeNotifier
          ) : base(orderRepository)
        {
            _orderRepository = orderRepository;
            _handlerRepository = handlerRepository;
            _recordRepository = recordRepository;
            _fileRepository = fileRepository;
            _jpushHelper = jpushHelper;
            _realTimeNotifier = realTimeNotifier;
        }



        #region 1.0 增删改查
        protected override IQueryable<WOrder_Order> CreateFilteredQuery(GetAllOrderInput input)
        {
            var userId = AbpSession.GetUserId();


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
                .WhereIf(input.CreatorId.HasValue, u => u.CreatorUserId == input.CreatorId.Value)
                .Include("CreatorUser")
                .Include("Handlers").Include("Handlers.Handler");

        }


        public async override Task<OrderDto> Get(EntityDto<long> input)
        {
            var dto = await _orderRepository.GetAll().Where(u => u.Id.Equals(input.Id))
                 .Include("CreatorUser")
                 .Include("Handlers")
                 .Include("Handlers.Handler").FirstOrDefaultAsync();
            return await Task.FromResult(dto.MapTo<OrderDto>());
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
                List<int> fileIds = input.FileIds.ToListBySplit();
                var fileList = await _fileRepository.GetAllListAsync(u => fileIds.Contains(u.Id));
                fileList.ForEach(u =>
                {
                    u.ParentId = order.Id.ToString();
                    u.Module = "order";
                });
            }

            await NotificationToAdmin(order);
            return await Task.FromResult(order.MapTo<OrderDto>());
        }

        /// <summary>
        /// 通知管理有心任务来了
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        private async Task NotificationToAdmin(WOrder_Order order)
        {
            UserNotification userNotification = new UserNotification();
            NotificationData notificationData = new NotificationData();
            notificationData["category"] = order.Category;
            notificationData["title"] = order.ItemName;
            userNotification.Notification =
                new TenantNotification()
                {
                    CreationTime = DateTime.Now,
                    NotificationName = "新消息",
                    Severity = NotificationSeverity.Info,
                    Data = notificationData
                };
            userNotification.TenantId = AbpSession.GetTenantId();
            //通知管理员
            userNotification.UserId = 1;
            userNotification.Id = Guid.NewGuid();
            await _realTimeNotifier.SendNotificationsAsync(new UserNotification[] { userNotification });
        }

        //异步更新
        public async override Task<OrderDto> Update(UpdateOrderDto input)
        {

            if (!string.IsNullOrEmpty(input.FileIds))
            {
                List<int> ids = input.FileIds.ToListBySplit();
                var newFiles = await _fileRepository.GetAllListAsync(u => string.IsNullOrEmpty(u.ParentId) && ids.Contains(u.Id));

                newFiles.ForEach(u =>
                {
                    u.ParentId = input.Id.ToString();
                });
            }
            return await base.Update(input);
        }

        public async Task<PagedResultDto<OrderDto>> GetMyOrders(GetMyOrderInput input)
        {
            var userId = AbpSession.GetUserId();
            //订单
            var orderData = CreateFilteredQuery(new GetAllOrderInput()
            {
                Category = input.Category,
                ItemName = input.ItemName
            });

            //我的状态
            var myTask = _handlerRepository.GetAll().Where(u => u.HandleId.Equals(userId));
            if (input.UserStatus == 0)
            {
                myTask = myTask.Where(u => u.OStatus.Equals(OStatus.Init) || u.OStatus.Equals(OStatus.Accept));
            }
            else
            {
                myTask = myTask.Where(u => u.OStatus.Equals(OStatus.Finish));
            }

            var data = from a in orderData
                       join b in myTask
                       on a.Id equals b.OrderId
                       select a;
            //分页和排序
            var result = data.OrderByDescending(u => u.CreationTime).OrderByDescending(u => u.OrderNo);

            int total = result.Count();
            return await Task.FromResult(new PagedResultDto<OrderDto>() { TotalCount = total, Items = result.Skip(input.SkipCount).Take(input.MaxResultCount).MapTo<List<OrderDto>>() });
        }
        #endregion

        #region 2.0 派单

        /// <summary>
        /// 给订单指派人
        /// </summary>
        /// <param name="handlers"></param>
        /// <returns></returns>
        public async Task<bool> Assign(List<CreateHandlerDto> handlers)
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
                u.TStatus = TStatus.Wait;
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

            });
            //执行推送消息
            await _jpushHelper.PushToAlias($"有新任务来了", strData, handlers.Select(u => u.HandleId.ToString()).ToList());
            return await Task.FromResult(true);
        }

        /// <summary>
        /// 获取当前待处理人
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        private async Task<WOrder_Handler> GetHandler(long orderId)
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
        public async Task<bool> Accept(long orderId)
        {
            //1:找到订单处理人
            var handler = await GetHandler(orderId);
            //2：接单
            handler.OStatus = OStatus.Accept;
            var order = await _orderRepository.FirstOrDefaultAsync(u => u.Id.Equals(handler.OrderId));
            order.TStatus = TStatus.Running;
            //3:添加记录
            await InsertRecordAsync(handler);
            return await Task.FromResult(true);
        }


        public async Task<bool> Finish(long orderId)
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
            return await Task.FromResult(true);

        }


        #endregion

        #region 3.0 抢单处理
        /// <summary>
        /// 将普通的未处理的派工单变更为抢单
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public async Task<bool> ChangeToRob(List<long> ids)
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

            return await Task.FromResult(true);
        }

        /// <summary>
        /// 抢单
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public async Task<bool> Robber(long orderId)
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
            return await Task.FromResult(true);
        }



        #endregion


        #region 4.0 报表统计
        public async Task<int> GetTStatusCount(TStatus status)
        {
            return await _orderRepository.CountAsync(u => u.TStatus.Equals(status));
        }


        /// <summary>
        /// 通过订单的完结时间来查询订单类别的完成情况
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<List<NameValueDto>> GetCategoryCount(GetDateInput input)
        {
            var data = _orderRepository.GetAll()
                                .WhereIf(input.TStatus.HasValue, u => u.TStatus.Equals(input.TStatus.Value))
                                .WhereIf(input.SDate.HasValue, u => u.LastModificationTime > input.SDate)
                                .WhereIf(input.EDate.HasValue, u => u.LastModificationTime < input.EDate);

            return await Task.FromResult(data.GroupBy(u => u.Category)
                                             .Select(u => new NameValueDto
                                             { Name = u.Key, Value = u.Count().ToString() })
                                             .ToList()
                                             );
        }




        #endregion

    }
    #endregion

}
