using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Naif.Blog.Framework;
using Naif.Blog.Models;
using Naif.Blog.Services;

namespace Naif.Blog.UI.ViewComponents
{
    public class NavigationViewComponent : BaseViewComponent
    {
        private readonly IPostRepository _postRepository;

        public NavigationViewComponent(IBlogRepository blogRepository, IBlogContext blogContext, IPostRepository postRepository)
            : base(blogRepository, blogContext)
        {
            _postRepository = postRepository;
        }
        
        public async Task<IViewComponentResult> InvokeAsync(string parent)
        {
            var menu = new Menu
            {
                CssClass = "navbar-nav",
                IsActiveCssClass = "active",
                Items = new List<MenuItem>()
            };

            await Task.Run(() =>
            {
                foreach(var page in _postRepository.GetAllPosts(Blog.Id).Where(p => p.ParentPostId == parent))
                {
                    if (page.PostType == PostType.Post)
                    {
                        menu.Items.Add(new MenuItem
                        {
                            Controller = "Post",
                            Action = "Index",
                            IsActive = false,
                            Text = page.Title
                        });
                    }
                    else
                    {
                        menu.Items.Add(new MenuItem
                        {
                            IsActive = false,
                            Link = $"/page/{page.Slug}",
                            Text = page.Title
                        });
                    }
                }
            });

            // ReSharper disable once Mvc.ViewComponentViewNotResolved
            return View(menu);
        }
    }
}