﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Naif.Blog.Framework;
using Naif.Blog.Services;

namespace Naif.Blog.UI.ViewComponents
{
    public class CategoryListViewComponent : BaseViewComponent
    {
        public CategoryListViewComponent(IBlogRepository blogRepository, IBlogContext blogContext)
            : base(blogRepository, blogContext) { }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            Dictionary<string, int> model = null;
			
            await Task.Run(() =>
            {
                model = BlogRepository.GetCategories(Blog.Id);
            });

            // ReSharper disable once Mvc.ViewComponentViewNotResolved
            return View(model);
        }
    }
}
