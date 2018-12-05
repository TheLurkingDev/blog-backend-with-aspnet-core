using Entities.DataModels;

namespace Entities.Extensions
{
    public static class BlogCategoryExtensions
    {
        public static void Map(this BlogCategory dbBlogCategory, BlogCategory blogCategory)
        {
            dbBlogCategory.Name = blogCategory.Name;
            dbBlogCategory.Information = blogCategory.Information;
        }
    }
}
