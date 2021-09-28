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
    public class PostController : BaseUIController
    {
        public PostController(IBlogContext blogContext, IBlogManager blogManager) : base(blogContext, blogManager)
        {
        }

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
            ViewModel.PageIndex = page ?? 0;
            ViewModel.BaseUrl = "/post/index";
            
            // ReSharper disable once Mvc.ViewNotResolved
            return View("ViewList", ViewModel);           
        }

        [HttpGet]
        [Route("~/category/{category}/{page?}")]
        public IActionResult ViewCategory(string category, int? page)
        {
            var posts = ViewModel.Posts.Where(p => p.Categories.Any(c => c.Name == category));

            ViewModel.Posts = posts;
            ViewModel.PageIndex = page ?? 0;
            ViewModel.BaseUrl = $"/category/{category}";
            
            // ReSharper disable once Mvc.ViewNotResolved
            return View("ViewList", ViewModel);
        }

        [HttpGet]
        [Route("{slug}")]
        public IActionResult ViewPost(string slug)
        {
            var post = ViewModel.Posts.SingleOrDefault(p => p.Slug == slug);

            if (post == null)
            {
                return new NotFoundResult();
            }

            ViewModel.Post = post;

            // ReSharper disable once Mvc.ViewNotResolved
            return View("ViewPost", ViewModel);
        }

        [HttpGet]
        [Route("~/tag/{tag}/{page?}")]
        public IActionResult ViewTag(string tag, int? page)
        {
            var posts = ViewModel.Posts.Where(p => p.Tags.Any(c => c.Name == tag));

            ViewModel.Posts = posts;
            ViewModel.PageIndex = page ?? 0;
            ViewModel.BaseUrl = $"/tag/{tag}";

            // ReSharper disable once Mvc.ViewNotResolved
            return View("ViewList", ViewModel);
        }
    }
}