using Microsoft.AspNetCore.Mvc;
using Naif.Blog.Framework;
using Naif.Blog.Services;

namespace Naif.Blog.UI.ViewComponents
{
    public abstract class BaseViewComponent : ViewComponent
    {
        protected BaseViewComponent(IBlogRepository blogRepository, IBlogContext blogContext)
        {
            Blog = blogContext.CurrentBlog;
            BlogRepository = blogRepository;
        }

        public Models.Blog Blog { get; }

        protected IBlogRepository BlogRepository { get; set; }
    }
}
