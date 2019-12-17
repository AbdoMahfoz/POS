using System;
using Backend.Models;

namespace Backend.Security
{
    public class JWTAuth : IAuth
    {
        public User GetUser(string token)
        {
            throw new NotImplementedException();
        }
        public string Login(string userName, string password)
        {
            throw new NotImplementedException();
        }
        public bool Register(string userName, string password)
        {
            throw new NotImplementedException();
        }
    }
}