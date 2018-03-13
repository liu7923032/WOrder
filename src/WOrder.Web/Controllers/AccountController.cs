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

namespace WOrder.Web.Controllers
{
    public class AccountController : WOrderControllerBase
    {
        private IUserAppService _userAppService;

        private static readonly string CookieScheme = CookieAuthenticationDefaults.AuthenticationScheme;

        private readonly IConfigurationRoot _appConfiguration;
        private IIntegralAppService _integralService;
        private IHostingEnvironment _env;
        public AccountController(IUserAppService userAppService, IIntegralAppService integralAppService, IHostingEnvironment env)
        {
            _userAppService = userAppService;
            _appConfiguration = AppConfigurations.Get(env.ContentRootPath, env.EnvironmentName);
            _integralService = integralAppService;
            _env = env;
        }


        public ActionResult Login()
        {
            return View();
        }

        private async Task LoginAysnc(LoginModel login)
        {
            var user = await _userAppService.SignAsync(login);
            //证件当事人
            var claimPrincipal = await _userAppService.GetPrincipalAsync(user, CookieScheme);

            await HttpContext.SignOutAsync(CookieScheme);
            //系统登陆
            await HttpContext.SignInAsync(CookieScheme, claimPrincipal, new AuthenticationProperties() { IsPersistent = login.IsRemember });

        }




        [AllowAnonymous]
        [HttpPost]
        public async Task<JsonResult> LoginAsync([FromBody]LoginModel login)
        {
            if (!ModelState.IsValid)
            {
                throw new AbpException("");
            }
            await LoginAysnc(login);
            //跳转地址
            //return RedirectToAction("Index", "Home");
            return Json(new AjaxResponse() { TargetUrl = "/Home/Index" });
        }

        public async Task<ActionResult> SignOut()
        {
            await HttpContext.SignOutAsync(CookieScheme);
            return RedirectToAction("Login");
        }

        /// <summary>
        /// 通过其他方式登陆
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        public async Task<ActionResult> ExternalLogin()
        {
            string account = Request.Query["gongHao"];
            //如果账号不存在,那么直接跳转到登陆页面
            if (string.IsNullOrEmpty(account))
            {
                return Redirect("/Account/Login");
            }
            //var loginUser = Request.Cookies["MDSD.LoginUser"];
            //获取当前信息
            var user = await _userAppService.GetUserByAccountAsync(account);
            //获取证件当事人
            var claimPrincipal = await _userAppService.GetPrincipalAsync(user, CookieScheme);
            //登陆系统
            await HttpContext.SignInAsync(CookieScheme, claimPrincipal, new AuthenticationProperties() { IsPersistent = false });
            //跳转地址
            return Redirect("/Home/Index");
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
        /// 用于同步积分
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [DontWrapResult]
        [HttpPost]
        [AllowAnonymous]
        public async Task<IntegralDto> SyncIntegral(CreateIntegralInput input)
        {
            return await _integralService.Create(input);
        }


        /// <summary>
        /// 积分信息
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult> Integral()
        {
            var user = await _userAppService.GetUserById(AbpSession.UserId.Value);
            //ViewBag.Integral = user.Integral;
            //计算当前人员的积分
            return await Task.FromResult(View());
        }

        public async Task<ActionResult> IntegralAbout()
        {
            return await Task.FromResult(View());
        }
    }
}