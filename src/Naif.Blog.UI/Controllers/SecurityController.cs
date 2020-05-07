using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Naif.Blog.Controllers;
using Naif.Blog.Framework;
using Naif.Blog.Services;
using Naif.Blog.UI.ViewModels;

namespace Naif.Blog.Web.Controllers
{
    public class SecurityController : BaseController
    {
        public SecurityController(IBlogRepository blogRepository, IBlogContext blogContext)
            : base(blogRepository, blogContext)
        {
        }

        [Authorize]
        public IActionResult Index()
        {
            return View(new BlogViewModel { Blog = Blog });
        }
    }
}
