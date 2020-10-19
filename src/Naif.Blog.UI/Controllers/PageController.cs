using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Naif.Blog.Controllers;
using Naif.Blog.Framework;
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
    }
}