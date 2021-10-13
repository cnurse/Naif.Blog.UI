using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using Naif.Blog.Models;

namespace Naif.Blog.UI.ViewModels
{
    public class EditPostViewModel
    {
        public IList<Message> Messages { get; set; }

        public int PageIndex { get; set; }

        public PostViewModel Post { get; set; }

        public List<SelectListItem> Pages { get; set; }
    }
}
