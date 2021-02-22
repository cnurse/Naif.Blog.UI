using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Naif.Blog.Framework;
using Naif.Blog.Models;
using Naif.Blog.Services;

namespace Naif.Blog.UI.ViewComponents
{
    public class RecentPostsViewComponent : BaseViewComponent
    {
        private readonly IPostRepository _postRepository;

        public RecentPostsViewComponent(IBlogRepository blogRepository, IBlogContext blogContext,
            IPostRepository postRepositry)
            : base(blogRepository, blogContext)
        {
            _postRepository = postRepositry;
        }

        public async Task<IViewComponentResult> InvokeAsync(int count, string cssClass)
        {
            var menu = new Menu
            {
                CssClass = cssClass,
                IsActiveCssClass = "active",
                Items = new List<MenuItem>()
            };


            await Task.Run(() =>
            {
                var recentPosts = _postRepository.GetAllPosts(Blog.Id).OrderByDescending(p => p.LastModified).Take(count);
                foreach (var post in recentPosts)
                {
                    var menuItem = CreateMenuItem(post);
                    if (post.LastModified > post.PubDate.AddDays(1))
                    {
                        menuItem.Text += "<small><em> -  updated</em></small>";
                    }


                    menu.Items.Add(menuItem);
                }
            });

            // ReSharper disable once Mvc.ViewComponentViewNotResolved
            return View(menu);

        }
    }
}