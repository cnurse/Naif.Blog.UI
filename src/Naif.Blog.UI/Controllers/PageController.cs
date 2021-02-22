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
        private readonly IPostRepository _postRepository;

        public PageController(IBlogRepository blogRepository,
            IBlogContext blogContext,
            IPostRepository postRepository)
            : base(blogRepository, blogContext)
        {
            _postRepository = postRepository;
        }

        [HttpGet]
        [Route("blog/{detail}/{pageIndex?}")]
        public IActionResult ViewBlog(string detail, int? pageIndex)
        {
            var post = _postRepository.GetAllPosts(Blog.Id).SingleOrDefault(p => p.PostTypeDetail == detail &&  p.PostType == PostType.Blog);

            if (post == null)
            {
                return new NotFoundResult();
            }

            var blogViewModel = new BlogViewModel
            {
                Blog = Blog,
                PageIndex = pageIndex ?? 0,
                Post = post,
                Posts = _postRepository.GetAllPosts(Blog.Id).Where(p => (p.PostType == PostType.Post || p.PostType == PostType.Page) && p.IsPublished)
            };
            
            // ReSharper disable once Mvc.ViewNotResolved
            return View("ViewList", blogViewModel);           

        }

        [HttpGet]
        [Route("{slug}")]
        public IActionResult ViewPost(string slug)
        {
            var post = _postRepository.GetAllPosts(Blog.Id).SingleOrDefault(p => p.Slug == slug);

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
            var post = _postRepository.GetAllPosts(Blog.Id).SingleOrDefault(p => p.PostTypeDetail == category && p.PostType == PostType.Category);

            if (post == null)
            {
                return new NotFoundResult();
            }

            var posts = _postRepository.GetAllPosts(Blog.Id).Where(p => (p.PostType == PostType.Post || p.PostType == PostType.Page) && p.Categories.Contains(category) && p.IsPublished);

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
            var post = _postRepository.GetAllPosts(Blog.Id).SingleOrDefault(p => p.PostTypeDetail == tag && p.PostType == PostType.Tag);

            if (post == null)
            {
                return new NotFoundResult();
            }

            var posts = _postRepository.GetAllPosts(Blog.Id).Where(p => (p.PostType == PostType.Post || p.PostType == PostType.Page) && p.Keywords.Contains(tag) && p.IsPublished);

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