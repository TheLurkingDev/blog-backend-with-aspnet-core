using Entities.Models;
using Contracts;
using Entities;
using System.Collections.Generic;
using System;
using System.Linq;

namespace Repository
{
    public class BlogPostRepository : RepositoryBase<BlogPost>, IBlogPostRepository
    {
        public BlogPostRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {
            
        }

        public IEnumerable<BlogPost> GetBlogPostsForCategory(Guid categoryId)
        {
            return FindByCondition(post => post.BlogCategoryID == categoryId);
        }

        public BlogPost GetBlogPostById(Guid blogPostId)
        {
            return FindByCondition(post => post.Id == blogPostId).FirstOrDefault();
        }

        public void CreateBlogPost(BlogPost post)
        {
            post.Id = Guid.NewGuid();
            Create(post);
            Save();
        }
    }
}
