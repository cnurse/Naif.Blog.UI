using System;
using System.Collections.Generic;
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
            var index = page ?? 0;
            ViewModel.PageIndex = index;
            ViewModel.BaseUrl = $"/post/index";
            ViewModel.ReturnUrl = $"/post/index/{index}";
            
            // ReSharper disable once Mvc.ViewNotResolved
            return View("ViewList", ViewModel);           
        }

        [HttpGet]
        [Route("~/category/{category}/{page?}")]
        public IActionResult ViewCategory(string category, int? page)
        {
            var index = page ?? 0;
            var posts = ViewModel.Posts.Where(p => p.Categories.Any(c => c.Name == category));

            ViewModel.Posts = posts;
            ViewModel.PageIndex = index;
            ViewModel.BaseUrl = $"/category/{category}";
            ViewModel.ReturnUrl = $"/category/{category}/{index}";
            
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
            var index = page ?? 0;
            var posts = ViewModel.Posts.Where(p => p.Tags.Any(c => c.Name == tag));

            ViewModel.Posts = posts;
            ViewModel.PageIndex = index;
            ViewModel.BaseUrl = $"/tag/{tag}";
            ViewModel.ReturnUrl = $"/tag/{tag}/{index}";

            // ReSharper disable once Mvc.ViewNotResolved
            return View("ViewList", ViewModel);
        }
        
        [HttpGet]
        [Route("list/{page?}")]
        public IActionResult List(int? page)
        {
            var index = page ?? 0;
            ViewModel.Posts = BlogManager.GetPosts(Blog.BlogId, p => p.PostType == PostType.Post);
            ViewModel.PageIndex = index;
            ViewModel.Post = ViewModel.Posts.FirstOrDefault();
            ViewModel.BaseUrl = "/post/list";
            ViewModel.ReturnUrl = $"/post/list/{index}";

            // ReSharper disable once Mvc.ViewNotResolved
            return View("List", ViewModel);
        }

        [HttpGet]
        [Route("list/{page}/add")]
        public IActionResult Add(int page)
        {
            ViewModel.Posts = BlogManager.GetPosts(Blog.BlogId, p => p.PostType == PostType.Post);
            ViewModel.PageIndex = page;
            ViewModel.Post = new Post();
            ViewModel.BaseUrl = "/post/list";
            ViewModel.ReturnUrl = $"/post/list/{page}";

            // ReSharper disable once Mvc.ViewNotResolved
            return View("List", ViewModel);
        }
        
        [HttpGet]
        [Route("edit/{postId}")]
        public IActionResult Edit(string postId, string returnUrl)
        {
            var post = ViewModel.Posts.SingleOrDefault(p => p.PostId == postId);
            ViewModel.Post = post;
            
            if (!String.IsNullOrEmpty(returnUrl))
            {
                ViewModel.ReturnUrl = returnUrl;
            }

            return View("EditPost", ViewModel);
        }

        [HttpGet]
        [Route("list/{page}/edit/{postId}")]
        public IActionResult Edit(int page, string postId)
        {
            ViewModel.Posts = BlogManager.GetPosts(Blog.BlogId, p => p.PostType == PostType.Post);
            ViewModel.PageIndex = page;
            ViewModel.Post = ViewModel.Posts.SingleOrDefault(p => p.PostId == postId);
            ViewModel.BaseUrl = "/post/list";
            ViewModel.ReturnUrl = $"/post/list/{page}";

            // ReSharper disable once Mvc.ViewNotResolved
            return View("List", ViewModel);
        }

        [HttpGet]
        [Route("delete/{postId}")]
        public IActionResult Delete(string postId, string returnUrl)
        {
            var post = BlogManager.GetPost(Blog.BlogId, p => p.PostId == postId);
            
            BlogManager.DeletePost(post);
            
            return Redirect(returnUrl);
        }

        [HttpPost]
        [Route("list/{page}/save")]
        public IActionResult Save(int page, PostViewModel post)
        {
            Post match = SavePost(post);

            ViewModel.PageIndex = page;
            ViewModel.Posts = BlogManager.GetPosts(Blog.BlogId, p => p.PostType == PostType.Post);
            ViewModel.Post = match;
            ViewModel.BaseUrl = "/post/list";
            ViewModel.Messages = new List<Message> { new() { Type = MessageType.Success, Text = "Post saved successfully" } };
            ViewModel.ReturnUrl = $"/post/list/{page}";

            // ReSharper disable once Mvc.ViewNotResolved
            return View("List", ViewModel);
        }

        [HttpPost]
        [Route("save")]
        public IActionResult Save(PostViewModel post, string returnUrl)
        {
            Post match = SavePost(post);

            return Redirect(returnUrl);
        }

    }
}