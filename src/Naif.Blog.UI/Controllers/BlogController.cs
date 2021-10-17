using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Naif.Blog.Framework;
using Naif.Blog.Models;
using Naif.Blog.Services;
using Naif.Blog.UI.ViewModels;

namespace Naif.Blog.UI.Controllers
{
    [Route("blog")]
    [Authorize(Roles = "Admin")]
    public class BlogController : BaseUIController
    {
        public BlogController(IBlogContext blogContext, IBlogManager blogManager) : base(blogContext, blogManager)
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