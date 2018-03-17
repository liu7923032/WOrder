using System;
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
        /// 我的订单页面
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Index()
        {
            return await Task.FromResult(View());
        }

        /// <summary>
        /// 查询订单的详细信息
        /// </summary>
        /// <param name="ids">购物车的id</param>
        /// <returns></returns>
        public async Task<ActionResult> Details(string id)
        {

            var path = ((Microsoft.AspNetCore.Server.Kestrel.Core.Internal.Http.FrameRequestHeaders)((Microsoft.AspNetCore.Http.Internal.DefaultHttpRequest)Request).Headers).HeaderReferer;
            if (path.ToString().Contains("Approve"))
            {
                ViewBag.ReqUrl = "/Order/Approve";
                ViewBag.IsComment = 0;
            }
            else
            {
                ViewBag.ReqUrl = "/Order/Index";
                ViewBag.IsComment = 1;
            }

            ViewBag.Ids = id;
            //获取订单详细信息
            return await Task.FromResult(View());
        }



        /// <summary>
        /// 审核订单页面
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult> Approve()
        {

            return View();
        }



    }
}