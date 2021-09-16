using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Naif.Blog.Framework;
using Naif.Blog.UI.ViewModels;

namespace Naif.Blog.UI.Controllers
{
    [Authorize]
    [Route("Profile")]
    public class ProfileController : Controller
    {
        public ProfileController(IBlogContext blogContext)
        {
            Blog = blogContext.Blog;
        }

        public Models.Blog Blog { get; }

        [Route("Claims")]
        public IActionResult Claims()
        {
            return View(new BlogViewModel { Blog = Blog });
        }
    }
}