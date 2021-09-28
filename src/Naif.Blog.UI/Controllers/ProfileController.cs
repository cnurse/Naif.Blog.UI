using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Naif.Blog.Framework;
using Naif.Blog.Services;
using Naif.Blog.UI.ViewModels;

namespace Naif.Blog.UI.Controllers
{
    [Authorize]
    [Route("Profile")]
    public class ProfileController : BaseUIController
    {
        public ProfileController(IBlogContext blogContext, IBlogManager blogManager) : base(blogContext, blogManager)
        {
        }

        [Route("Claims")]
        public IActionResult Claims()
        {
            return View(ViewModel);
        }
    }
}