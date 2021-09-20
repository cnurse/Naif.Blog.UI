using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Naif.Blog.Framework;
using Naif.Blog.Services;
using Naif.Blog.UI.ViewModels;

namespace Naif.Blog.UI.Controllers
{
    [Route("Admin")]
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly IBlogManager _blogManager;

        public AdminController(IBlogContext blogContext, IBlogManager blogManager)
        {
            Blog = blogContext.Blog;
            _blogManager = blogManager;
        }
        
        public Models.Blog Blog { get; }

        [Route("BlogSettings")]
        public IActionResult BlogSettings()
        {
            var blogViewModel = new BlogViewModel
            {
                Blog = Blog,
            };
            
            // ReSharper disable once Mvc.ViewNotResolved
            return View("BlogSettings", blogViewModel);           

        }
        
        [Route("EditSettings")]
        public IActionResult EditSettings(Blog blog)
        {
            var blogViewModel = new BlogViewModel
            {
                Blog = Blog,
            };
            
            // ReSharper disable once Mvc.ViewNotResolved
            return Redirect(Blog.HomeRedirectUrl);
        }

    }
}