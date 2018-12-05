using Entities.DataModels;

namespace Entities.Extensions
{
    public static class BlogPostExtensions
    {
        public static void Map(this BlogPost dbBlogPost, BlogPost blogPost)
        {
            dbBlogPost.Title = blogPost.Title;
            dbBlogPost.Slug = blogPost.Slug;
            dbBlogPost.Content = blogPost.Content;
            dbBlogPost.DateCreated = blogPost.DateCreated;
            dbBlogPost.LikeCount = blogPost.LikeCount;
        }
    }
}
