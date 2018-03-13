using System.Threading.Tasks;
using Abp.Localization;
using Microsoft.AspNetCore.Mvc;

namespace WOrder.Web.Views.Shared.Components.LanguageSelection
{
    public class LanguageSelectionViewComponent : WOrderViewComponent
    {
        private readonly ILanguageManager _languageManager;

        public LanguageSelectionViewComponent(ILanguageManager languageManager)
        {
            _languageManager = languageManager;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var model = await Task.FromResult(new LanguageSelectionViewModel
            {
                CurrentLanguage = _languageManager.CurrentLanguage,
                Languages = _languageManager.GetLanguages(),
                CurrentUrl = Request.Path
            });

            return View(model);
        }
    }
}
