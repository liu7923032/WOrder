using Abp.AspNetCore.Mvc.Views;

namespace WOrder.Web.Views
{
    public abstract class WOrderRazorPage<TModel> : AbpRazorPage<TModel>
    {
        protected WOrderRazorPage()
        {
            LocalizationSourceName = WOrderConsts.LocalizationSourceName;
        }
    }
}
