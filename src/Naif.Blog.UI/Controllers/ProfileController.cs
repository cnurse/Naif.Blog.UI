using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Naif.Blog.Framework;
using Naif.Blog.Security;
using Naif.Blog.Services;

namespace Naif.Blog.UI.Controllers
{
    [Authorize]
    [Route("Profile")]
    public class ProfileController : BaseUIController
    {
        public ProfileController(IBlogContext blogContext, IBlogManager blogManager, IPostAuthorizationProcessor authorizationProcessor) : base(blogContext, blogManager, authorizationProcessor)
        {
        }

        [Route("Claims")]
        public IActionResult Claims()
        {
            return View(ViewModel);
        }
    }
}