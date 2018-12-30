using Entities.DataModels;
using System;

namespace Contracts
{
    public interface IBlogCategoryRepository : IRepositoryBase<BlogCategory>
    {        
        BlogCategory GetBlogCategoryById(Guid blogCategoryId);
        void CreateBlogCategory(BlogCategory blogCategory);
        void UpdateBlogCategory(BlogCategory dbBlogCategory, BlogCategory blogCategory);
        void DeleteBlogCategory(BlogCategory blogCategory);
    }
}
