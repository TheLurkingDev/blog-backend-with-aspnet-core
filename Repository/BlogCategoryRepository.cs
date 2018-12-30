using Contracts;
using Entities;
using Entities.Extensions;
using Entities.DataModels;
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

        public void DeleteBlogCategory(BlogCategory blogCategory)
        {
            Delete(blogCategory);
            Save();
        }
    }
}
