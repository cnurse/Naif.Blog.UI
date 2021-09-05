using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Naif.Blog.Controllers;
using Naif.Blog.Framework;
using Naif.Blog.Models;
using Naif.Blog.Services;
using Naif.Blog.UI.ViewModels;
// ReSharper disable MemberCanBePrivate.Global

namespace Naif.Blog.UI.Controllers
{
	[Route("post")]
    public class PostController : BaseController
    {
        private readonly IBlogManager _blogManager;

        public PostController(IBlogContext blogContext, IBlogManager blogManager)
        {
            Blog = blogContext.Blog;
            _blogManager = blogManager;
        }
        
        public Models.Blog Blog { get; }

        [HttpGet]
        [Route("cancel/{returnUrl}")]
        public IActionResult Cancel(string returnUrl)
        {
            return Redirect(returnUrl);
        }
        
        [HttpGet]
        [Route("")]
        [Route("index/{page?}")]
        public IActionResult Index(int? page)
        {
            var blogViewModel = new BlogViewModel
            {
                Blog = Blog,
                PageIndex = page ?? 0,
                Posts = _blogManager.GetPosts(Blog.Id, p => p.PostType == PostType.Post && p.IsPublished),
                BaseUrl = "/post/index"
            };
            
            // ReSharper disable once Mvc.ViewNotResolved
            return View("ViewList", blogViewModel);           
        }

        [HttpGet]
        [Route("~/category/{category}/{page?}")]
        public IActionResult ViewCategory(string category, int? page)
        {
            var blogViewModel = new BlogViewModel
            {
                Blog = Blog,
                PageIndex = page ?? 0,
                Posts = _blogManager.GetPosts(Blog.Id, p => Post.SearchPredicate(p) && p.Categories.Any(c => c.Name == category)),
                BaseUrl = $"/category/{category}"
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
            return View("ViewPost", blogViewModel);
        }

        [HttpGet]
        [Route("~/tag/{tag}/{page?}")]
        public IActionResult ViewTag(string tag, int? page)
        {
            var blogViewModel = new BlogViewModel
            {
                Blog = Blog,
                PageIndex = page ?? 0,
                Posts = _blogManager.GetPosts(Blog.Id, p => Post.SearchPredicate(p) && p.Tags.Any(c => c.Name == tag)),
                BaseUrl = $"/tag/{tag}"
            };

            // ReSharper disable once Mvc.ViewNotResolved
            return View("ViewList", blogViewModel);
        }
    }
}