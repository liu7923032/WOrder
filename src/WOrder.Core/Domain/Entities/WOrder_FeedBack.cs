using System;
using System.Collections.Generic;
using System.Text;

namespace WOrder.Domain.Entities
{
    public class WOrder_Feedback
    {
        /// <summary>
        /// 反馈标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 反馈描述
        /// </summary>
        public string Description { get; set; }
    }
}
