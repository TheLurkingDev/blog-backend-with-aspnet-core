using Entities.Models;
using System;
using System.Collections.Generic;

namespace Contracts
{
    public interface IUserRepository : IRepositoryBase<User>
    {
        IEnumerable<User> GetUsersForWebsite(Guid websiteId);
        User GetUserById(Guid id);
        void CreateUser(User user);
    }
}
