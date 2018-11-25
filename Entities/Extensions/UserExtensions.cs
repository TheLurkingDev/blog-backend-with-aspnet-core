using System;

namespace Entities.Extensions
{
    public static class UserExtensions
    {
        public static (byte[] hash, byte[] salt) CreatePasswordHash(string password)
        {
            if(string.IsNullOrEmpty(password))
            {
                throw new ArgumentNullException("password", "'password' cannot be null or empty when creating a password hash");
            }

            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                return (hmac.Key, hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password)));
            }
        }
    }
}
