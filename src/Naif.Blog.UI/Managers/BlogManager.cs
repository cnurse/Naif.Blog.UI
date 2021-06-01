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
        private readonly ICategoryRepository _categoryRepository;
        private readonly ITagRepository _tagRepository;
        
        public BlogManager(IBlogRepository blogRepository, IPostRepository postRepository, ITagRepository tagRepository, ICategoryRepository categoryRepository)
        {
            _blogRepository = blogRepository;
            _postRepository = postRepository;
            _categoryRepository = categoryRepository;
            _tagRepository = tagRepository;
        }

        public Models.Blog GetBlog(Func<Models.Blog, bool> predicate, bool includeCategoriesAndTags)
        {
           var blog = _blogRepository.GetBlog(predicate);
           
           if (includeCategoriesAndTags)
           {
               blog.Categories = _categoryRepository.GetCategories(blog.Id);
               blog.Tags = _tagRepository.GetTags(blog.Id);
           }

           return blog;
        }

        public void DeletePost(Post post)
        {
            _postRepository.DeletePost(post);
        }

        public Post GetPost(string blogId, Func<Post, bool> predicate)
        {
            return _postRepository.GetAllPosts(blogId).SingleOrDefault(predicate);
        }

        public IEnumerable<Post> GetRecentPosts(string blogId, int count)
        {
            return (count < 0) 
                ? _postRepository.GetAllPosts(blogId).OrderByDescending(p => p.LastModified) 
                : _postRepository.GetAllPosts(blogId).OrderByDescending(p => p.LastModified).Take(count);
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