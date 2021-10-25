using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Naif.Blog.Framework;
using Naif.Blog.Models;
using Naif.Blog.Services;
using Naif.Blog.UI.ViewModels;
using Naif.Core.Models;

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
            var index = page ?? 0;

            var post = BlogManager.GetPost(Blog.BlogId, p => p.PostTypeDetail == detail && p.PostType == PostType.Blog);

            if (post == null)
            {
                return new NotFoundResult();
            }

            ViewModel.PageIndex = index;
            ViewModel.Post = post;
            ViewModel.Post = post;
            ViewModel.BaseUrl = $"/page/blog/{detail}";
            ViewModel.ReturnUrl = $"/page/blog/{detail}/{index}";

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
            var index = page ?? 0;

            var post = BlogManager.GetPost(Blog.BlogId, p => p.PostTypeDetail == category && p.PostType == PostType.Category);

            if (post == null)
            {
                return new NotFoundResult();
            }

            var posts = ViewModel.Posts.Where(p => p.Categories.Any(c => c.Name == category));

            ViewModel.PageIndex = index;
            ViewModel.Post = post;
            ViewModel.Posts = posts;
            ViewModel.BaseUrl = $"/page/category/{category}";
            ViewModel.ReturnUrl = $"/page/category/{category}/{index}";

            // ReSharper disable once Mvc.ViewNotResolved
            return View("ViewList", ViewModel);
        }

        [HttpGet]
        [Route("tag/{tag}/{pageIndex?}")]
        public IActionResult ViewTag(string tag, int? page)
        {
            var index = page ?? 0;

            var post = BlogManager.GetPost(Blog.BlogId, p => p.PostTypeDetail == tag && p.PostType == PostType.Tag);

            if (post == null)
            {
                return new NotFoundResult();
            }

            var posts = ViewModel.Posts.Where(p => p.Tags.Any(c => c.Name == tag));

            ViewModel.PageIndex = index;
            ViewModel.Post = post;
            ViewModel.Posts = posts;
            ViewModel.BaseUrl = $"/page/tag/{tag}";
            ViewModel.ReturnUrl = $"/page/tag/{tag}/{index}";

            // ReSharper disable once Mvc.ViewNotResolved
            return View("ViewList", ViewModel);
        }

        [Route("list")]
        public IActionResult List()
        {
            var post = ViewModel.Pages.Where(p => p.ParentPostId == "").OrderBy(p => p.PageOrder).FirstOrDefault();

            ViewModel.Post = post;
            ViewModel.BaseUrl = "/page/list";
            ViewModel.ReturnUrl = $"/page/list";

            // ReSharper disable once Mvc.ViewNotResolved
            return View("List", ViewModel);
        }

        [HttpGet]
        [Route("list/page/{postId}")]
        public IActionResult List(string postId)
        {
            var post = ViewModel.Pages.SingleOrDefault(p => p.PostId == postId);
            ViewModel.Post = post;
            ViewModel.BaseUrl = "/page/list";
            ViewModel.ReturnUrl = $"/page/list";

            // ReSharper disable once Mvc.ViewNotResolved
            return View("List", ViewModel);
        }

        [HttpGet]
        [Route("add")]
        public IActionResult Add()
        {
            ViewModel.Post = new Post {PostType = PostType.Page };
            ViewModel.ReturnUrl = $"/page/list";

            // ReSharper disable once Mvc.ViewNotResolved
            return View("List", ViewModel);
        }

        [HttpGet]
        [Route("edit/{postId}")]
        public IActionResult Edit(string postId)
        {
            var post = ViewModel.Pages.SingleOrDefault(p => p.PostId == postId);
            ViewModel.Post = post;
            if (post != null && !string.IsNullOrEmpty(post.Slug))
            {
                ViewModel.ReturnUrl = $"/page/{post.Slug}";
            }

            return View("EditPage", ViewModel);
        }

        [HttpPost]
        [Route("save")]
        public IActionResult Save(PostViewModel post, string returnUrl)
        {
            Post match = SavePost(post);

            IActionResult result;
            if (String.IsNullOrEmpty(returnUrl))
            {
                ViewModel.Post = match;
                ViewModel.BaseUrl = "/page/list";
                ViewModel.Messages = new List<Message> { new() { Type = MessageType.Success, Text = "Page saved successfully" } };
                result = View("List", ViewModel);
            }
            else
            {
                result = Redirect(returnUrl);
            }

            return result;
        }
    }
}
