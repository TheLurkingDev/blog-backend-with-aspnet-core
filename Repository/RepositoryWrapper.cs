using Contracts;
using Entities;

namespace Repository
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private RepositoryContext _repositoryContext;
        private IBlogCategoryRepository _blogCategoryRepository;
        private IBlogPostRepository _blogPostRepository;
        private IWebsiteRepository _websiteRepository;

        public IBlogCategoryRepository BlogCategoryRepository
        {
            get
            {
                if(_blogCategoryRepository == null)
                {
                    _blogCategoryRepository = new BlogCategoryRepository(_repositoryContext);
                }

                return _blogCategoryRepository;
            }
        }

        public IBlogPostRepository BlogPostRepository
        {
            get
            {
                if(_blogPostRepository == null)
                {
                    _blogPostRepository = new BlogPostRepository(_repositoryContext);
                }

                return _blogPostRepository;
            }
        }

        public IWebsiteRepository WebsiteRepository
        {
            get
            {
                if(_websiteRepository == null)
                {
                    _websiteRepository = new WebsiteRepository(_repositoryContext);
                }

                return _websiteRepository;
            }
        }
    }
}
