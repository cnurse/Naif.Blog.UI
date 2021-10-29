using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Naif.Blog.Framework;
using Naif.Blog.Security;
using Naif.Blog.Services;
using Naif.Core.Constants;

namespace Naif.Blog.UI.Controllers
{
    [Route("blog")]
    [Authorize(Roles = RoleNames.Admin)]
    public class BlogController : BaseUIController
    {
        public BlogController(IBlogContext blogContext, IBlogManager blogManager, IPostAuthorizationProcessor authorizationProcessor) : base(blogContext, blogManager, authorizationProcessor)
        {
        }

        [Route("settings")]
        public IActionResult Settings()
        {
            // ReSharper disable once Mvc.ViewNotResolved
            return View("Settings", ViewModel);           
        }
        
        [HttpPost]
        [Route("edit")]
        public IActionResult Edit(Models.Blog blog)
        {
            BlogManager.SaveBlog(blog);
            
            // ReSharper disable once Mvc.ViewNotResolved
            return Redirect(Blog.HomeRedirectUrl);
        }
    }
}