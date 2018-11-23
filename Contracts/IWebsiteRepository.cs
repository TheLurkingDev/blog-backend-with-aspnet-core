using Entities.Models;
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
    }
}
