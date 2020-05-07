using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Naif.Blog.Framework;
using Naif.Blog.Services;

#pragma warning disable 1998

namespace Naif.Blog.UI.ViewComponents
{
    public class CopyrightViewComponent : BaseViewComponent
    {
        public CopyrightViewComponent(IBlogRepository blogRepository, IBlogContext blogContext)
            : base(blogRepository, blogContext) { }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            // ReSharper disable once Mvc.ViewComponentViewNotResolved
            return View(Blog);
        }
    }
}
