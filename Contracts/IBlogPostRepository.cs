using Entities.Models;
using System;
using System.Collections.Generic;

namespace Contracts
{
    public interface IBlogPostRepository : IRepositoryBase<BlogPost>
    {
        IEnumerable<BlogPost> GetBlogPostsForCategory(Guid categoryId);
        BlogPost GetBlogPostById(Guid blogPostId);
        void CreateBlogPost(BlogPost post);
        void UpdateBlogPost(BlogPost dbBlogPost, BlogPost blogPost);
    }
}
