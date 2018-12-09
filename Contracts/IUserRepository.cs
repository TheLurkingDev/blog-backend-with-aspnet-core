using Entities.DataModels;
using System;
using System.Collections.Generic;

namespace Contracts
{
    public interface IUserRepository : IRepositoryBase<User>
    {
        IEnumerable<User> GetUsersForWebsite(Guid websiteId);
        User GetUserById(Guid id);
        User GetUserByUserName(string Name);
        void CreateUser(User user);
        void DeleteUser(User user);
    }
}
