using System.Collections.Generic;
using Naif.Blog.Models;

namespace Naif.Blog.UI.ViewModels
{
    public class BlogViewModel
    {
        public Naif.Blog.Models.Blog Blog { get; set; }
        
        public int PageIndex { get; set; }
        
        public Post Post { get; set; }

        public IEnumerable<Post> Posts { get; set; }
        
        public string BaseUrl { get; set; }
    }
}