using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Naif.Blog.UI.ViewComponents
{
    public class FooterViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            // ReSharper disable once Mvc.ViewComponentViewNotResolved
            return View();
        }

    }
}
