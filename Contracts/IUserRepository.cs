using Entities.DataModels;
using System;

namespace Contracts
{
    public interface IUserRepository : IRepositoryBase<User>
    {        
        User GetUserById(Guid id);
        User GetUserByUserName(string name);
        User GetUserByEmail(string email);
        void CreateUser(User user);
        void UpdateUser(User dbUser, User user);
        void DeleteUser(User user);
    }
}
