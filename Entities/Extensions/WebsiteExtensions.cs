using Entities.DataModels;

namespace Entities.Extensions
{
    public static class WebsiteExtensions
    {
        public static void Map(this Website dbWebsite, Website website)
        {
            dbWebsite.Name = website.Name;
            dbWebsite.Url = website.Url;
        }
    }
}
