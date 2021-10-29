using Microsoft.AspNetCore.Mvc;
using Naif.Blog.Framework;
using Naif.Blog.Security;
using Naif.Blog.Services;

namespace Naif.Blog.UI.Controllers
{
    public class ErrorController : BaseUIController
    {
        public ErrorController(IBlogContext blogContext, IBlogManager blogManager, IPostAuthorizationProcessor authorizationProcessor) : base(blogContext, blogManager, authorizationProcessor)
        {
        }

        public IActionResult Index()
        {
            return Code(Response.StatusCode.ToString());
        }
        
        [Route("/error/code/{errCode}")]
        public IActionResult Code(string errCode)
        {
            if (errCode == "404")
            {
                return View("404", ViewModel);
            }

            if (errCode == "500")
            {
                return View("500", ViewModel);
            }

            return View("Unknown", ViewModel);
        }
    }
}