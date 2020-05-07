using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Naif.Blog.UI.ViewComponents
{
    public class HeadViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(Models.Blog blog)
        {
            // ReSharper disable once Mvc.ViewComponentViewNotResolved
            return View(blog);
        }

    }
}
