using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Naif.Blog.Models;

namespace Naif.Blog.UI.ViewComponents
{
	public class TagCloudViewComponent : ViewComponent
	{
		public async Task<IViewComponentResult> InvokeAsync(Models.Blog blog, int count)
		{
			IList<Tag> model = null;
			
			await Task.Run(() =>
			{
				model = blog.Tags;
			});
			
			// ReSharper disable once Mvc.ViewComponentViewNotResolved
			return View(model);
		}
	}
}