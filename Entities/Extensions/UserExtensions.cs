using Entities.ClientDTOs;
using Entities.DataModels;
using System;

namespace Entities.Extensions
{
    public static class UserExtensions
    {
        public static void Map(this User dbUser, User user)
        {
            dbUser.UserName = user.UserName;
            dbUser.Email = user.Email;
            dbUser.FirstName = user.FirstName;
            dbUser.LastName = user.LastName;
            dbUser.Phone = user.Phone;
            dbUser.Role = user.Role;
        }

        public static (byte[] salt, byte[] hash) CreatePasswordHash(this User user)
        {
            if(string.IsNullOrEmpty(user.Password))
            {
                throw new ArgumentNullException("password", "'password' cannot be null or empty when creating a password hash");
            }

            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                return (hmac.Key, hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(user.Password)));
            }
        }

        public static bool VerifyPasswordHash(this UserCredentials userCredentials, byte[] salt, byte[] hash)
        {
            if (string.IsNullOrEmpty(userCredentials.Password))
            {
                throw new ArgumentNullException("password", "'password' cannot be null or empty when validating password");
            }

            using (var hmac = new System.Security.Cryptography.HMACSHA512(salt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(userCredentials.Password));
                for(int i = 0; i < computedHash.Length; i++)
                {
                    if(computedHash[i] != hash[i])
                    {
                        return false;
                    }
                }
            }

            return true;
        }
    }
}
