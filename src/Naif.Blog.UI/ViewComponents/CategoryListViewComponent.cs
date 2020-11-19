using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Naif.Blog.Framework;
using Naif.Blog.Models;
using Naif.Blog.Services;

namespace Naif.Blog.UI.ViewComponents
{
    public class CategoryListViewComponent : BaseViewComponent
    {
        public CategoryListViewComponent(IBlogRepository blogRepository, IBlogContext blogContext)
            : base(blogRepository, blogContext) { }

        public async Task<IViewComponentResult> InvokeAsync(string cssClass)
        {
            var menu = new Menu
            {
                CssClass = cssClass,
                IsActiveCssClass = "active",
                Items = new List<MenuItem>()
            };


			
            await Task.Run(() =>
            {
                var categories = BlogRepository.GetCategories(Blog.Id);
                foreach (var category in categories)
                {
                    var menuItem = new MenuItem()
                    {
                        IsActive = false,
                        Link = $"/category/{category.Key}",
                        Text = $"{category.Key} - ({category.Value})"
                    };
                    menu.Items.Add(menuItem);
                }
            });

            // ReSharper disable once Mvc.ViewComponentViewNotResolved
            return View(menu);
        }
    }
}
