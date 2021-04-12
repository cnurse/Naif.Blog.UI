using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Naif.Blog.Framework;
using Naif.Blog.Services;

namespace Naif.Blog.UI.ViewComponents
{
	public class TagCloudViewComponent : ViewComponent
	{
		private IBlogManager _blogManager;
		
		public TagCloudViewComponent(IBlogManager blogManager)
		{
			_blogManager = blogManager;

		}

		public async Task<IViewComponentResult> InvokeAsync(string blogId, int count)
		{
			Dictionary<string, int> model = null;
			
			await Task.Run(() =>
			{
				model = _blogManager.GetTags(blogId, count);
			});
			
			// ReSharper disable once Mvc.ViewComponentViewNotResolved
			return View(model);
		}
	}
}