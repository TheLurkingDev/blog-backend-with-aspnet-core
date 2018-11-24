using Contracts;
using Entities;
using Entities.Extensions;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Repository
{
    public class BlogCategoryRepository : RepositoryBase<BlogCategory>, IBlogCategoryRepository
    {
        public BlogCategoryRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {

        }

        public IEnumerable<BlogCategory> GetBlogCategoriesForWebsite(Guid websiteId)
        {
            return FindByCondition(categories => categories.WebsiteID == websiteId).OrderBy(categories => categories.Name);
        }

        public BlogCategory GetBlogCategoryById(Guid blogCategoryId)
        {
            return FindByCondition(category => category.Id == blogCategoryId).FirstOrDefault();
        }

        public void CreateBlogCategory(BlogCategory blogCategory)
        {
            blogCategory.Id = Guid.NewGuid();
            Create(blogCategory);
            Save();
        }

        public void UpdateBlogCategory(BlogCategory dbBlogCategory, BlogCategory blogCategory)
        {
            dbBlogCategory.Map(blogCategory);
            Update(dbBlogCategory);
            Save();
        }
    }
}
