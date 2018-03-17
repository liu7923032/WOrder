using System;
using System.Collections.Generic;
using System.Text;
using Abp.Domain.Repositories;
using Abp.Events.Bus.Handlers;
using Abp.Net.Mail;
using WOrder.Domain.Entities;
using Dark.Common.Extension;
using Abp.Dependency;
using System.Threading.Tasks;
using Abp.Notifications;
using Abp.Runtime.Session;
using Abp;

namespace WOrder.Domain.Events
{
    public class OrderChangeHandler : IEventHandler<OrderEventData>, ITransientDependency
    {
        private readonly IEmailSender _emailSender;
        private readonly IRepository<WOrder_Account, long> _userRepository;
        private readonly INotificationPublisher _notificationPublisher;
        private readonly IAbpSession _abpSession;

        public OrderChangeHandler(IEmailSender emailSender,
            IRepository<WOrder_Account, long> userRepository,
            INotificationPublisher notificationPublisher,
            IAbpSession abpSession)
        {
            _emailSender = emailSender;
            _userRepository = userRepository;
            _notificationPublisher = notificationPublisher;
            _abpSession = abpSession;
        }

        public void HandleEvent(OrderEventData eventData)
        {
            //发送邮箱
            if (eventData is OrderEventData)
            {
                var order = eventData.Order;
                var user = _userRepository.Get(order.CreatorUserId.Value);
                if (!string.IsNullOrEmpty(user.Email))
                {
                    var body = $"<div>订单编号:<a href='https://e.mdsd.cn:9100/Order/Index'>{order.OrderNo}</a></div><div>状态变更:<span style='color:blue;'>{eventData.OldStatus.GetDescription()}</span>-><span style='color:blue;'>{order.TStatus.GetDescription()}</span></div>";
                    Task.Run(() =>
                    {
                        //1：邮箱通知
                        _emailSender.Send(user.Email, "积分商城-商品到货", body);

                        //2.给用户发送邮件通知,提示人员已经接单
                        UserIdentifier userIdentifier = new UserIdentifier(null, order.CreatorUserId.Value);
                        _notificationPublisher.Publish("订单审批完成通知", new MessageNotificationData("订单申请已审批通过"), null, NotificationSeverity.Success, new UserIdentifier[] { userIdentifier });
                    });
                }
            }
        }
    }
}
