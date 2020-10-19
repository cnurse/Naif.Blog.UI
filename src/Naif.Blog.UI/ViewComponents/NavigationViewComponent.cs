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
                foreach(var post in _postRepository.GetAllPosts(Blog.Id).Where(p => p.PostType == PostType.Page && p.ParentPostId == parent))
                {
                    if (post.PostType == PostType.Post)
                    {
                        menu.Items.Add(new MenuItem
                        {
                            Controller = "Post",
                            Action = "Index",
                            IsActive = false,
                            Text = post.Title
                        });
                    }
                    else
                    {
                        menu.Items.Add(new MenuItem
                        {
                            IsActive = false,
                            Link = $"/post/{post.Slug}",
                            Text = post.Title
                        });
                    }
                }
            });

            // ReSharper disable once Mvc.ViewComponentViewNotResolved
            return View(menu);
        }
    }
}