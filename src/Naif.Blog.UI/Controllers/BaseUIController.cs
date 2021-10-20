using System;
using System.Linq;
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

        protected Post SavePost(PostViewModel postViewModel)
        {
            if (postViewModel.ParentPostId == null)
            {
                postViewModel.ParentPostId = String.Empty;
            }

            Post match;
            
            //Check if add mode
            if (string.IsNullOrWhiteSpace(postViewModel.Slug))
            {
                match = new Post
                {
                    PostId = postViewModel.PostId,
                    BlogId = Blog.BlogId
                };
            }
            else
            {
                match = BlogManager.GetPost(Blog.BlogId, p => p.PostId == postViewModel.PostId);
            }
 
            if (match != null)
            {
                //Merge PostViewModel into matched Post
                postViewModel.ToPost(match);
                
                //Create Slug if empty
                if (!string.Equals(match.Slug, postViewModel.Slug, StringComparison.OrdinalIgnoreCase)  && !string.IsNullOrWhiteSpace(postViewModel.Slug))
                {
                    match.Slug = CreateSlug(postViewModel.Slug);
                }

                match.LastModified = DateTime.UtcNow;

                if (!match.IsPublished)
                {
                    match.PubDate = DateTime.UtcNow;
                }

                BlogManager.SavePost(match);
            }

            return match;
        }


    }
}