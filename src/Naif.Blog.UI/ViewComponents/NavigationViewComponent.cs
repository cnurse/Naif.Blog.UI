using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing.Matching;
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
                foreach(var post in _postRepository.GetAllPosts(Blog.Id).Where(p => p.PostType != PostType.Post && p.ParentPostId == parent))
                {
                    switch (post.PostType)
                    {
                        case PostType.Page:
                            menu.Items.Add(new MenuItem
                            {
                                IsActive = false,
                                Link = $"/page/{post.Slug}",
                                Text = post.Title
                            });
                            break;
                        case PostType.Blog:
                            menu.Items.Add(new MenuItem
                            {
                                Link = $"/page/blog/{post.PostTypeDetail}",
                                IsActive = false,
                                Text = post.Title
                            });
                            break;
                        case PostType.Category:
                            menu.Items.Add(new MenuItem
                            {
                                Link = $"/page/category/{post.PostTypeDetail}",
                                IsActive = false,
                                Text = post.Title
                            });
                            break;
                        case PostType.Tag:
                            menu.Items.Add(new MenuItem
                            {
                                Link = $"/page/tag/{post.PostTypeDetail}",
                                IsActive = false,
                                Text = post.Title
                            });
                            break;
                        case PostType.Url:
                            menu.Items.Add(new MenuItem
                            {
                                Link = $"{post.PostTypeDetail}",
                                IsActive = false,
                                Text = post.Title
                            });
                            break;
                    }
                }
            });

            // ReSharper disable once Mvc.ViewComponentViewNotResolved
            return View(menu);
        }
    }
}