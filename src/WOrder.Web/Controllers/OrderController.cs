﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.AspNetCore.Mvc.Authorization;
using WOrder.Domain.Entities;
using WOrder.Order;
using Microsoft.AspNetCore.Mvc;

namespace WOrder.Web.Controllers
{
    public class OrderController : WOrderControllerBase
    {
        private IOrderAppService _orderAppService;

        public OrderController(IOrderAppService orderAppService)
        {
            _orderAppService = orderAppService;
        }

        /// <summary>
        /// 保修管理
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Index()
        {
            return await Task.FromResult(View());
        }

        
        /// <summary>
        /// 稽核巡检
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Audit()
        {
            return await Task.FromResult(View());
        }

        /// <summary>
        /// 反馈管理
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> FeedBack()
        {
            return await Task.FromResult(View());
        }

        /// <summary>
        /// 投诉管理
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult> Complaint()
        {
            return await Task.FromResult(View());
        }



    }
}