using Microsoft.AspNetCore.Mvc;
using Naif.Blog.Controllers;
using Naif.Blog.Framework;
using Naif.Blog.Services;
using Naif.Blog.UI.ViewModels;

namespace Naif.Blog.UI.Controllers
{
    public class ErrorController : BaseController
    {
        public ErrorController(IBlogContext blogContext)
        {
            Blog = blogContext.CurrentBlog;
        }

        public Models.Blog Blog { get; }

        public IActionResult Index()
        {
            return Code(Response.StatusCode.ToString());
        }
        
        [Route("/error/code/{errCode}")]
        public IActionResult Code(string errCode)
        {
            if (errCode == "404")
            {
                return View("404", new BlogViewModel { Blog = Blog});
            }

            if (errCode == "500")
            {
                return View("500", new BlogViewModel { Blog = Blog});
            }

            return View("Unknown", new BlogViewModel { Blog = Blog});
        }
    }
}