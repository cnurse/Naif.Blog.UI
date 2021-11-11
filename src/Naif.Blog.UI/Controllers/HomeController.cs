using System;
using Microsoft.AspNetCore.Mvc;
using Naif.Blog.Controllers;
using Naif.Blog.Framework;
using Naif.Blog.ViewModels;
using Naif.Core.Models;

namespace Naif.Blog.UI.Controllers
{
    [Route("Home")]
    public class HomeController : BaseController
    {
        private User _user;
        
        public HomeController(IBlogContext blogContext)
        {
            Blog = blogContext.Blog;
            _user = blogContext.User;
        }

        public Models.Blog Blog { get; }

        [Route("Index")]
        [Route("~/")]
        public IActionResult Index()
        {
            IActionResult result;

            if (String.IsNullOrEmpty(Blog.HomeRedirectUrl))
            {
                result = View(new BlogViewModel { Blog = Blog });
            }
            else
            {
                result = Redirect(Blog.HomeRedirectUrl);
            }

            return result;
        }

    }
}
