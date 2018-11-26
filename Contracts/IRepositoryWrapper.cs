namespace Contracts
{
    public interface IRepositoryWrapper
    {
        IBlogCategoryRepository BlogCategoryRepository { get; }
        IBlogPostRepository BlogPostRepository { get; }
        IWebsiteRepository WebsiteRepository { get;  }
        IUserRepository UserRepository { get; }
    }
}
