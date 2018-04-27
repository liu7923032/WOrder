using System.Threading.Tasks;
using WOrder.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Abp.Web.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Abp;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Hosting;
using WOrder.Configuration;
using WOrder.Integral;
using WOrder.UserApp;
using System.Net.Http;
using Newtonsoft.Json;
using Dark.Common.Utils;
using Abp.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using WOrder.Web.Core.Controllers;
using WOrder.Extension;
using Abp.UI;

namespace WOrder.Web.Controllers
{

    public class AccountController : WOrderControllerBase
    {
        private IUserAppService _userAppService;

        private static readonly string CookieScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        //private static readonly string CookieScheme = JwtBearerDefaults.AuthenticationScheme;

        private readonly IConfigurationRoot _appConfiguration;
        private IIntegralAppService _integralService;
        private IHostingEnvironment _env;

        private JPushHelper _jpushHelper;
        public AccountController(IUserAppService userAppService, IIntegralAppService integralAppService, IHostingEnvironment env, JPushHelper jPushHelper)
        {
            _userAppService = userAppService;
            _appConfiguration = AppConfigurations.Get(env.ContentRootPath, env.EnvironmentName);
            _integralService = integralAppService;
            _env = env;
            _jpushHelper = jPushHelper;
        }

        public async Task<ActionResult> Login()
        {
            return await Task.FromResult(View());
        }



        [HttpPost]
        public async Task<JsonResult> LoginAsync([FromBody]LoginModel login)
        {

            if (!ModelState.IsValid)
            {
                throw new UserFriendlyException("参数异常");
            }
            await LoginAysnc(login);
            //跳转地址
            //return RedirectToAction("Index", "Home");
            return Json(new AjaxResponse() { TargetUrl = "/Home/Index" });
        }


        private async Task LoginAysnc(LoginModel login)
        {
            var user = await _userAppService.SignAsync(login);
            //证件当事人
            var claimPrincipal = await _userAppService.GetPrincipalAsync(user.Id.ToString(), user.UserName, CookieScheme);

            await HttpContext.SignOutAsync(CookieScheme);
            //系统登陆
            await HttpContext.SignInAsync(CookieScheme, claimPrincipal, new AuthenticationProperties() { IsPersistent = login.IsRemember });

        }

        public async Task<ActionResult> SignOut()
        {
            await HttpContext.SignOutAsync(CookieScheme);
            return RedirectToAction("Login");
        }


        /// <summary>
        /// 为了保持网站能够持续响应
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        public async Task<AjaxResponse> ToResponse()
        {
            return await Task.FromResult(new AjaxResponse() { Result = "成功响应" });
        }

        /// <summary>
        /// 用户审核
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult> UserApprove()
        {
            return await Task.FromResult(View());
        }


    }
}