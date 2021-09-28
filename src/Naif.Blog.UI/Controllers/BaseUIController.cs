using Naif.Blog.Controllers;
using Naif.Blog.Framework;
using Naif.Blog.Models;
using Naif.Blog.Services;
using Naif.Blog.UI.ViewModels;

namespace Naif.Blog.UI.Controllers
{
    public abstract class BaseUIController : BaseController
    {
        protected BaseUIController(IBlogContext blogContext, IBlogManager blogManager)
        {
            Blog = blogContext.Blog;
            BlogManager = blogManager;
            ViewModel = new BlogViewModel()
            {
                Blog = Blog,
                PageIndex = 0,
                Pages = BlogManager.GetPosts(Blog.BlogId, p => p.PostType != PostType.Post),
                Posts = BlogManager.GetPosts(Blog.BlogId, p => Post.SearchPredicate(p))
            };

        }


        protected Models.Blog Blog { get; }
        
        protected IBlogManager BlogManager { get; }
        
        protected BlogViewModel ViewModel { get; set; }
    }
}