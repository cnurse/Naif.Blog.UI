using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Naif.Auth0.Security;
using Naif.Blog.Framework;
using Naif.Blog.Security;
using Naif.Blog.Services;
using Naif.Blog.UI.Managers;
using Naif.Blog.UI.Security;

namespace Naif.Blog.UI.Framework
{
    public static class ServiceCollectionExtensions
    {
        public static void AddNaifBlog(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IBlogRepository, FileBlogRepository>();
            services.AddTransient<IPostRepository, JsonPostRepository>();
            services.AddTransient<ICategoryRepository, FileCategoryRepository>();
            services.AddTransient<ITagRepository, FileTagRepository>();
            services.AddTransient<IBlogManager, BlogManager>();
            services.AddScoped<IBlogContext, BlogContext>();
            services.AddScoped<IPostAuthorizationProcessor, PostAuthorizationProcessor>();
            services.AddScoped<IPostAuthorize, NaifBlogPostAuthorize>();

            services.Configure<XmlRpcSecurityOptions>(configuration.GetSection("XmlRpcSecurity"));

            services.AddAuth0(configuration);

            services.Configure<RazorViewEngineOptions>(options =>
            {
                options.ViewLocationExpanders.Add(new ThemeViewLocationExpander());
            });
        }
    }
}