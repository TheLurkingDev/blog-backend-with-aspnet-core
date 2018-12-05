using Entities.DataModels;
using System;
using System.Collections.Generic;

namespace Contracts
{
    public interface IBlogCategoryRepository : IRepositoryBase<BlogCategory>
    {
        IEnumerable<BlogCategory> GetBlogCategoriesForWebsite(Guid websiteId);
        BlogCategory GetBlogCategoryById(Guid blogCategoryId);
        void CreateBlogCategory(BlogCategory blogCategory);
        void UpdateBlogCategory(BlogCategory dbBlogCategory, BlogCategory blogCategory);
        void DeleteBlogCategory(BlogCategory blogCategory);
    }
}
