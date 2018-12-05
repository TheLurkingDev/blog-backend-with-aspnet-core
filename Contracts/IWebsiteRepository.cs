using Entities.DataModels;
using System;
using System.Collections.Generic;

namespace Contracts
{
    public interface IWebsiteRepository : IRepositoryBase<Website>
    {
        IEnumerable<Website> GetAllWebsites();
        Website GetWebsiteById(Guid websiteId);
        void CreateWebsite(Website website);
        void UpdateWebsite(Website dbWebsite, Website website);
        void DeleteWebsite(Website website);
    }
}
