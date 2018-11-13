using Contracts;
using Entities;
using Entities.Models;

namespace Repository
{
    public class WebsiteRepository : RepositoryBase<Website>, IWebsiteRepository
    {
        public WebsiteRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {

        }
    }
}
