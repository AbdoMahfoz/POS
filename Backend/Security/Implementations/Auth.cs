using Backend.DataContracts;
using Backend.Models;
using Backend.Repository.ExtendedRepositories;
using Backend.Security.Interfaces;
using System.Collections.Generic;
using System.ServiceModel;

namespace Backend.Security.Implementations
{
    public class Auth : IAuth
    {
        private readonly IHash Hash;
        private readonly IUserRepository UserRepository;
        private readonly ITokenizer Tokenizer;
        public Auth(IHash Hash, IUserRepository UserRepository, ITokenizer Tokenizer)
        {
            this.Hash = Hash;
            this.UserRepository = UserRepository;
            this.Tokenizer = Tokenizer;
        }
        public void EnsureAuthorized(string token)
        {
            if (token == null) throw new FaultException("Null Token");
            if (Tokenizer.DeTokenize(token) != null) throw new FaultException("Unauthorized");
        }
        public void EnsureAuthorizedAsAdmin(string token)
        {
            if (token == null) throw new FaultException("Null Token");
            var res = Tokenizer.DeTokenize(token);
            if (res == null) throw new FaultException("Unauthorized");
            if (res["Role"] != "Admin") throw new FaultException("Forbidden Access"); 
        }
        public int GetUserId(string token, bool IsAdmin = false)
        {
            if (token == null) throw new FaultException("Null Token");
            var res = Tokenizer.DeTokenize(token);
            if (res == null) throw new FaultException("Unauthorized");
            if (IsAdmin && res["Role"] != "Admin") throw new FaultException("Forbidden Access");
            return int.Parse(res["Id"]);
        }
        public string Login(string email, string password)
        {
            User u = UserRepository.GetUser(email);
            if(u != null && Hash.Validate(password, u.Password))
            {
                return Tokenizer.Tokenize(new Dictionary<string, string>
                {
                    { "Id", u.Id.ToString() } ,
                    { "Name", u.Name ??"Anonymous"},
                    { "Role", u.IsAdmin? "Admin" : "User" }
                });
            }
            return null;
        }
        public User Register(UserDataRequest request, bool IsAdmin = false)
        {
            if (UserRepository.CheckEmailExists(request.Email)) return null;
            User u = Helpers.MapTo<User>(request);
            u.IsAdmin = IsAdmin;
            u.Password = Hash.Hash(u.Password);
            UserRepository.Insert(u).Wait();
            return u;
        }
    }
}