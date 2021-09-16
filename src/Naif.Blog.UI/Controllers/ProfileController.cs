using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Naif.Blog.UI.Controllers
{
    [Authorize]
    [Route("Profile")]
    public class ProfileController : Controller
    {
        [Route("Claims")]
        public IActionResult Claims()
        {
            return View();
        }
    }
}