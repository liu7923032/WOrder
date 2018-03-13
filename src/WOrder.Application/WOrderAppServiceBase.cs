using System;
using Abp.Application.Services;
using Abp.Authorization;

namespace WOrder
{
    /// <summary>
    /// Derive your application services from this class.
    /// </summary>
    [AbpAuthorize]
    public abstract class WOrderAppServiceBase : ApplicationService
    {
        protected WOrderAppServiceBase()
        {
            LocalizationSourceName = WOrderConsts.LocalizationSourceName;
        }

        /// <summary>
        /// 获取当前登陆人的UserId
        /// </summary>
        protected long UserId
        {
            get
            {
                return AbpSession.UserId.Value;
            }
        }

        /// <summary>
        /// 检查是否包含js代码,如果包含将js处理掉
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        protected string CheckJsAndProcess(string input)
        {
            //对js进行转义处理
            if (input.Contains("<") || input.Contains(">"))
            {
                input= input.Replace("<", "&lt").Replace(">", "&gt");
            }
            return input;
        }
    }
}