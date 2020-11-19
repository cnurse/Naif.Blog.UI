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
        
        public async Task<IViewComponentResult> InvokeAsync(string postId, string cssClass, bool includeParent = false)
        {
            var menu = new Menu
            {
                CssClass = cssClass,
                IsActiveCssClass = "active",
                Items = new List<MenuItem>()
            };

            if (includeParent && !string.IsNullOrEmpty(postId))
            {
                var currentPost = _postRepository.GetAllPosts(Blog.Id).Single(p => p.PostType != PostType.Post && p.PostId == postId);

                if (currentPost != null && !string.IsNullOrEmpty(currentPost.ParentPostId))
                {
                    var parentPost = _postRepository.GetAllPosts(Blog.Id).Single(p => p.PostType != PostType.Post && p.PostId == currentPost.ParentPostId);
                    if (parentPost != null)
                    {
                        var menuItem = CreateMenuItem(parentPost);
                        menuItem.Text = "Return to " + menuItem.Text;
                        menu.Items.Add(menuItem);
                    }
                }
            }

            await Task.Run(() =>
            {
                foreach(var post in _postRepository.GetAllPosts(Blog.Id).Where(p => p.PostType != PostType.Post && p.ParentPostId == postId))
                {
                    menu.Items.Add(CreateMenuItem(post));
                }
            });

            // ReSharper disable once Mvc.ViewComponentViewNotResolved
            return View(menu);
        }
    }
}