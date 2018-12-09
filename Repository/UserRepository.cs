using Contracts;
using Entities;
using Entities.Extensions;
using Entities.DataModels;
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

        public User GetUserByUserName(string name)
        {
            return FindByCondition(user => user.UserName == name).FirstOrDefault();
        }

        public void CreateUser(User user)
        {
            user.Id = Guid.NewGuid();            
            (user.Salt, user.HashedPassword) = user.CreatePasswordHash();
            Create(user);
            Save();
        }

        public void UpdateUser(User dbUser, User user)
        {
            dbUser.Map(user);
            Update(dbUser);
            Save();
        }

        public void DeleteUser(User user)
        {
            Delete(user);
            Save();
        }
    }
}
