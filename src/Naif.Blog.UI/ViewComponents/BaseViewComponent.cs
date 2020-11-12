using Microsoft.AspNetCore.Mvc;
using Naif.Blog.Framework;
using Naif.Blog.Models;
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

        protected MenuItem CreateMenuItem(Post post)
        {
            MenuItem item = null;
            switch (post.PostType)
            {
                case PostType.Post:
                    item = new MenuItem
                    {
                        IsActive = false,
                        Link = $"/post/{post.Slug}",
                        Text = post.Title
                    };
                    break;
                case PostType.Page:
                    item = new MenuItem
                    {
                        IsActive = false,
                        Link = $"/page/{post.Slug}",
                        Text = post.Title
                    };
                    break;
                case PostType.Blog:
                    item = new MenuItem
                    {
                        Link = $"/page/blog/{post.PostTypeDetail}",
                        IsActive = false,
                        Text = post.Title
                    };
                    break;
                case PostType.Category:
                    item = new MenuItem
                    {
                        Link = $"/page/category/{post.PostTypeDetail}",
                        IsActive = false,
                        Text = post.Title
                    };
                    break;
                case PostType.Tag:
                    item = new MenuItem
                    {
                        Link = $"/page/tag/{post.PostTypeDetail}",
                        IsActive = false,
                        Text = post.Title
                    };
                    break;
                case PostType.Url:
                    item = new MenuItem
                    {
                        Link = $"{post.PostTypeDetail}",
                        IsActive = false,
                        Text = post.Title
                    };
                    break;
            }

            return item;
        }
    }
}
