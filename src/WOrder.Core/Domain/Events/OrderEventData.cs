using System;
using System.Collections.Generic;
using System.Text;
using Abp.Events.Bus;
using WOrder.Domain.Entities;

namespace WOrder.Domain.Events
{
    public class OrderEventData : EventData
    {

        public OrderStatus OldStatus { get; set; }

        public WOrder_Order Order { get; set; }
    }
}
