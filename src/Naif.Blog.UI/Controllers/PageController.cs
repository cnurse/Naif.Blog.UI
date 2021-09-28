using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Naif.Blog.Controllers;
using Naif.Blog.Framework;
using Naif.Blog.Models;
using Naif.Blog.Services;
using Naif.Blog.UI.ViewModels;

namespace Naif.Blog.UI.Controllers
{
    [Route("page")]
    public class PageController : BaseUIController
    {
        public PageController(IBlogContext blogContext, IBlogManager blogManager) : base(blogContext, blogManager)
        {
        }

        [HttpGet]
        [Route("blog/{detail}/{page?}")]
        public IActionResult ViewBlog(string detail, int? page)
        {
            var post = BlogManager.GetPost(Blog.BlogId, p => p.PostTypeDetail == detail && p.PostType == PostType.Blog);

            if (post == null)
            {
                return new NotFoundResult();
            }

            ViewModel.PageIndex = page ?? 0;
            ViewModel.Post = post;
            ViewModel.BaseUrl = $"/page/blog/{detail}";

            // ReSharper disable once Mvc.ViewNotResolved
            return View("ViewList", ViewModel);

        }

        [HttpGet]
        [Route("{slug}")]
        public IActionResult ViewPost(string slug)
        {
            var post = BlogManager.GetPost(Blog.BlogId, p => p.Slug == slug);

            if (post == null)
            {
                return new NotFoundResult();
            }

            ViewModel.Post = post;

            // ReSharper disable once Mvc.ViewNotResolved
            return View("ViewPage", ViewModel);
        }

        [HttpGet]
        [Route("category/{category}/{page?}")]
        public IActionResult ViewCategory(string category, int? page)
        {
            var post = BlogManager.GetPost(Blog.BlogId, p => p.PostTypeDetail == category && p.PostType == PostType.Category);

            if (post == null)
            {
                return new NotFoundResult();
            }

            var posts = ViewModel.Posts.Where(p => p.Categories.Any(c => c.Name == category));

            ViewModel.PageIndex = page ?? 0;
            ViewModel.Post = post;
            ViewModel.Posts = posts;
            ViewModel.BaseUrl = $"/page/category/{category}";

            // ReSharper disable once Mvc.ViewNotResolved
            return View("ViewList", ViewModel);
        }

        [HttpGet]
        [Route("tag/{tag}/{pageIndex?}")]
        public IActionResult ViewTag(string tag, int? page)
        {
            var post = BlogManager.GetPost(Blog.BlogId, p => p.PostTypeDetail == tag && p.PostType == PostType.Tag);

            if (post == null)
            {
                return new NotFoundResult();
            }

            var posts = ViewModel.Posts.Where(p => p.Tags.Any(c => c.Name == tag));

            ViewModel.PageIndex = page ?? 0;
            ViewModel.Post = post;
            ViewModel.Posts = posts;
            ViewModel.BaseUrl = $"/page/tag/{tag}";

            // ReSharper disable once Mvc.ViewNotResolved
            return View("ViewList", ViewModel);
        }
    }
}
