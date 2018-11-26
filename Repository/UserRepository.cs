using Contracts;
using Entities;
using Entities.Extensions;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Repository
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {

        }

        public IEnumerable<User> GetUsersForWebsite(Guid websiteId)
        {
            return FindByCondition(user => user.WebsiteID == websiteId);
        }

        public User GetUserById(Guid id)
        {
            return FindByCondition(user => user.Id == id).FirstOrDefault();
        }

        public void CreateUser(User user)
        {
            user.Id = Guid.NewGuid();            
            (user.HashedPassword, user.Salt) = UserExtensions.CreatePasswordHash(user.Password);
            Create(user);
            Save();
        }
    }
}
