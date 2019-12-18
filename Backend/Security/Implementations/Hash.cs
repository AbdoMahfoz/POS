using Backend.Security.Interfaces;
using System;
using System.Security.Cryptography;

namespace Backend.Security.Implementations
{
    public class Hash : IHash
    {
        public bool Validate(string target, string hashed)
        {
            byte[] hashBytes = Convert.FromBase64String(hashed);
            byte[] salt = new byte[16];
            Array.Copy(hashBytes, 0, salt, 0, 16);
            var pbkdf2 = new Rfc2898DeriveBytes(target, salt, 10000);
            byte[] hash = pbkdf2.GetBytes(20);
            for (int i = 0; i < 20; i++)
            {
                if (hashBytes[i + 16] != hash[i])
                {
                    return false;
                }
            }
            return true;
        }
        string IHash.Hash(string target)
        {
            byte[] salt;
            new RNGCryptoServiceProvider().GetBytes(salt = new byte[16]);
            var DerivedBytes = new Rfc2898DeriveBytes(target, salt, 10000);
            byte[] hash = DerivedBytes.GetBytes(20);
            byte[] hashBytes = new byte[36];
            Array.Copy(salt, 0, hashBytes, 0, 16);
            Array.Copy(hash, 0, hashBytes, 16, 20);
            return Convert.ToBase64String(hashBytes);
        }
    }
}