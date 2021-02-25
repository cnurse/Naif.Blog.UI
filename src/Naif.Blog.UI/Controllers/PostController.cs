using System;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Naif.Blog.Controllers;
using Naif.Blog.Framework;
using Naif.Blog.Models;
using Naif.Blog.Services;
using Naif.Blog.UI.ViewModels;

namespace Naif.Blog.UI.Controllers
{
	[Route("Post")]
    public class PostController : BaseController
    {
        private readonly IPostRepository _postRepository;
        private readonly IAuthorizationService _authorizationService;

        public PostController(IAuthorizationService authorizationService,
            IBlogRepository blogRepository,
            IBlogContext blogContext,
            IPostRepository postRepository)
            : base(blogRepository, blogContext)
        {
            _authorizationService = authorizationService;
            _postRepository = postRepository;
        }

        [HttpGet]
        [Route("Cancel/{returnUrl}")]
        public IActionResult Cancel(string returnUrl)
        {
            return Redirect(returnUrl);
        }
        
        [HttpGet]
        [Route("")]
        [Route("Index/{page?}")]
        public IActionResult Index(int? page)
        {
            var blogViewModel = new BlogViewModel
            {
                Blog = Blog,
                PageIndex = page ?? 0,
                Posts = _postRepository.GetAllPosts(Blog.Id).Where(p => p.PostType == PostType.Post && p.IsPublished)
            };
            
            // ReSharper disable once Mvc.ViewNotResolved
            return View("Index", blogViewModel);           
        }

        [HttpGet]
        [Route("~/category/{category}/{page?}")]
        public IActionResult ViewCategory(string category, int? page)
        {
            var blogViewModel = new BlogViewModel
            {
                Blog = Blog,
                PageIndex = page ?? 0,
                Posts = _postRepository.GetAllPosts(Blog.Id).Where(p => Post.SearchPredicate(p) && p.Categories.Contains(category))
            };

            // ReSharper disable once Mvc.ViewNotResolved
            return View("Index", blogViewModel);
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
                Posts = _postRepository.GetAllPosts(Blog.Id).Where(p => Post.SearchPredicate(p) && p.Keywords.Contains(tag))
            };

            // ReSharper disable once Mvc.ViewNotResolved
            return View("Index", blogViewModel);
        }
    }
}