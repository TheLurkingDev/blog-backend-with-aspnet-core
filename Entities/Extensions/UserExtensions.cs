﻿using System;

namespace Entities.Extensions
{
    public static class UserExtensions
    {
        public static (byte[] salt, byte[] hash) CreatePasswordHash(string password)
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

        public static bool VerifyPasswordHash(string password, byte[] salt, byte[] hash)
        {
            if (string.IsNullOrEmpty(password))
            {
                throw new ArgumentNullException("password", "'password' cannot be null or empty when validating password");
            }

            using (var hmac = new System.Security.Cryptography.HMACSHA512(salt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
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
