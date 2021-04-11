using System;
using System.Collections.Generic;
using System.Linq;
using Naif.Blog.Models;
using Naif.Blog.Services;

namespace Naif.Blog.UI.Managers
{
    public class BlogManager : IBlogManager
    {
        private readonly IBlogRepository _blogRepository;
        private readonly IPostRepository _postRepository;
        
        public BlogManager(IBlogRepository blogRepository, IPostRepository postRepository)
        {
            _blogRepository = blogRepository;
            _postRepository = postRepository;
        }
        
        public void DeletePost(Post post)
        {
            _postRepository.DeletePost(post);
        }

        public Dictionary<string, int> GetCategories(string blogId, int count)
        {
            return _blogRepository.GetCategories(blogId).OrderByDescending(c => c.Value).Take(count).ToDictionary(x => x.Key, x=> x.Value);
        }

        public Post GetPost(string blogId, Func<Post, bool> predicate)
        {
            return _postRepository.GetAllPosts(blogId).SingleOrDefault(predicate);
        }

        public IEnumerable<Post> GetRecentPosts(string blogId, int count)
        {
            return _postRepository.GetAllPosts(blogId).OrderByDescending(p => p.LastModified).Take(count);
        }

        public IEnumerable<Post> GetPosts(string blogId, Func<Post, bool> predicate)
        {
            return _postRepository.GetAllPosts(blogId).Where(predicate);
        }

        public string SaveMedia(string blogId, MediaObject media)
        {
            return _blogRepository.SaveMedia(blogId, media);
        }

        public void SavePost(Post post)
        {
            _postRepository.SavePost(post);
        }
    }
}