using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Naif.Blog.Models;

#pragma warning disable 1998

namespace Naif.Blog.UI.ViewComponents
{
    public class ListViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(IEnumerable<Post> list)
        {
            
            // ReSharper disable once Mvc.ViewComponentViewNotResolved
            return View(list);
        }
    }
}


