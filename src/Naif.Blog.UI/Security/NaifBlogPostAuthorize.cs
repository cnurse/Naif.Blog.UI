using System;
using Naif.Blog.Models;
using Naif.Blog.Security;
using Naif.Core.Constants;
using Naif.Core.Framework;
using Naif.Core.Models;

namespace Naif.Blog.UI.Security
{
    public class NaifBlogPostAuthorize : IPostAuthorize
    {
        public bool CanViewPost(Post post, User user)
        {
            bool canView = true;
            if (!user.IsAuthenticated || !user.IsInRole(RoleNames.Admin))
            {
                canView = post.IncludeInLists && post.IsPublished && post.PubDate <= DateTime.UtcNow;
            }

            return canView;
        }
    }
}