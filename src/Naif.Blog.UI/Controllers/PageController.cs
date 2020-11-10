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
        [Route("blog/{detail}/{page?}")]
        public IActionResult ViewBlog(string detail, int? page)
        {
            var post = _postRepository.GetAllPosts(Blog.Id).SingleOrDefault(p => p.PostTypeDetail == detail &&  p.PostType == PostType.Blog);

            if (post == null)
            {
                return new NotFoundResult();
            }

            var blogViewModel = new BlogViewModel
            {
                Blog = Blog,
                PageIndex = page ?? 0,
                Posts = _postRepository.GetAllPosts(Blog.Id).Where(p => p.PostType == PostType.Post && p.IsPublished)
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
        [Route("category/{category}/{page?}")]
        public IActionResult ViewCategory(string category)
        {
            var post = _postRepository.GetAllPosts(Blog.Id).SingleOrDefault(p => p.PostTypeDetail == category && p.PostType == PostType.Category);

            if (post == null)
            {
                return new NotFoundResult();
            }

            var posts = _postRepository.GetAllPosts(Blog.Id).Where(p => p.PostType == PostType.Post && p.Categories.Contains(category) && p.IsPublished);

            var blogViewModel = new BlogViewModel
            {
                Blog = Blog,
                PageIndex = 0,
                Post = post,
                Posts = posts
            };

            // ReSharper disable once Mvc.ViewNotResolved
            return View("ViewList", blogViewModel);
        }

        [HttpGet]
        [Route("tag/{tag}/{page?}")]
        public IActionResult ViewTag(string tag)
        {
            var post = _postRepository.GetAllPosts(Blog.Id).SingleOrDefault(p => p.PostTypeDetail == tag && p.PostType == PostType.Tag);

            if (post == null)
            {
                return new NotFoundResult();
            }

            var posts = _postRepository.GetAllPosts(Blog.Id).Where(p => p.PostType == PostType.Post && p.Keywords.Contains(tag) && p.IsPublished);

            var blogViewModel = new BlogViewModel
            {
                Blog = Blog,
                PageIndex = 0,
                Post = post,
                Posts = posts
            };

            // ReSharper disable once Mvc.ViewNotResolved
            return View("ViewList", blogViewModel);
        }
    }

}