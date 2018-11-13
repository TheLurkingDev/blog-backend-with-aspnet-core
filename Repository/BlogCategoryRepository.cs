using Contracts;
using Entities;
using Entities.Models;

namespace Repository
{
    public class BlogCategoryRepository : RepositoryBase<BlogCategory>, IBlogCategoryRepository
    {
        public BlogCategoryRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {

        }
    }
}
