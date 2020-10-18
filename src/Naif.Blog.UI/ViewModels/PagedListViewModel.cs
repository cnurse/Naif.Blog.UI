using System.Collections.Generic;
using Naif.Blog.Models;

namespace Naif.Blog.UI.ViewModels
{
    public class PagedListViewModel
    {
        public bool IsPage { get; set; }
        public IEnumerable<Post> Posts { get; set; }
        public Pager Pager { get; set; }
    }
}