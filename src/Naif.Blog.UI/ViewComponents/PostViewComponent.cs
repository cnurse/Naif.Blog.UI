using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Naif.Blog.Models;

#pragma warning disable 1998

namespace Naif.Blog.UI.ViewComponents
{
	public class PostViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(Post post)
        {
            // ReSharper disable once Mvc.ViewComponentViewNotResolved
            return View(post);
        }
    }
}
