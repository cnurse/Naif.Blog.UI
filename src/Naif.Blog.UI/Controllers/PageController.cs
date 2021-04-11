using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Naif.Blog.Controllers;
using Naif.Blog.Framework;
using Naif.Blog.Models;
using Naif.Blog.Services;
using Naif.Blog.UI.ViewModels;

namespace Naif.Blog.UI.Controllers
{
    [Route("Page")]
    public class PageController : BaseController
    {
        private readonly IBlogManager _blogManager;

        public PageController(IBlogContext blogContext, IBlogManager blogManager)
        {
            Blog = blogContext.CurrentBlog;
            _blogManager = blogManager;
        }
        
        public Models.Blog Blog { get; }
        
        [HttpGet]
        [Route("blog/{detail}/{pageIndex?}")]
        public IActionResult ViewBlog(string detail, int? pageIndex)
        {
            var post = _blogManager.GetPost(Blog.Id, p => p.PostTypeDetail == detail && p.PostType == PostType.Blog);

            if (post == null)
            {
                return new NotFoundResult();
            }

            var blogViewModel = new BlogViewModel
            {
                Blog = Blog,
                PageIndex = pageIndex ?? 0,
                Post = post,
                Posts = _blogManager.GetPosts(Blog.Id, p => Post.SearchPredicate(p))
            };
            
            // ReSharper disable once Mvc.ViewNotResolved
            return View("ViewList", blogViewModel);           

        }

        [HttpGet]
        [Route("{slug}")]
        public IActionResult ViewPost(string slug)
        {
            var post = _blogManager.GetPost(Blog.Id, p => p.Slug == slug);

            if (post == null)
            {
                return new NotFoundResult();
            }

            var blogViewModel = new BlogViewModel
            {
                Blog = Blog,
                PageIndex = 0,
                Post = post
            };

            // ReSharper disable once Mvc.ViewNotResolved
            return View("ViewPage", blogViewModel);
        }

        [HttpGet]
        [Route("category/{category}/{pageIndex?}")]
        public IActionResult ViewCategory(string category, int? pageIndex)
        {
            var post = _blogManager.GetPost(Blog.Id, p => p.PostTypeDetail == category && p.PostType == PostType.Category);

            if (post == null)
            {
                return new NotFoundResult();
            }

            var posts = _blogManager.GetPosts(Blog.Id, p => Post.SearchPredicate(p) && p.Categories.Contains(category));

            var blogViewModel = new BlogViewModel
            {
                Blog = Blog,
                PageIndex = pageIndex ?? 0,
                Post = post,
                Posts = posts
            };

            // ReSharper disable once Mvc.ViewNotResolved
            return View("ViewList", blogViewModel);
        }

        [HttpGet]
        [Route("tag/{tag}/{pageIndex?}")]
        public IActionResult ViewTag(string tag, int? pageIndex)
        {
            var post = _blogManager.GetPost(Blog.Id, p => p.PostTypeDetail == tag && p.PostType == PostType.Tag);

            if (post == null)
            {
                return new NotFoundResult();
            }

            var posts = _blogManager.GetPosts(Blog.Id, p => Post.SearchPredicate(p) && p.Categories.Contains(tag));

            var blogViewModel = new BlogViewModel
            {
                Blog = Blog,
                PageIndex = pageIndex ?? 0,
                Post = post,
                Posts = posts
            };

            // ReSharper disable once Mvc.ViewNotResolved
            return View("ViewList", blogViewModel);
        }
    }

}