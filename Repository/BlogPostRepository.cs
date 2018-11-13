using Entities.Models;
using Contracts;
using Entities;

namespace Repository
{
    public class BlogPostRepository : RepositoryBase<BlogPost>, IBlogPostRepository
    {
        public BlogPostRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {

        }
    }
}
