using Backend.Models;
using Backend.Repository.ExtendedRepositories;
using Backend.Security.Interfaces;

namespace Backend.Security.Implementations
{
    public class Auth : IAuth
    {
        private readonly IHash Hash;
        private readonly IUserRepository UserRepository;
        public Auth(IHash Hash, IUserRepository UserRepository)
        {
            this.Hash = Hash;
            this.UserRepository = UserRepository;
        }
        public User Login(string userName, string password)
        {
            User u = UserRepository.GetUser(userName);
            if(u != null && Hash.Validate(password, u.Password))
            {
                return u;
            }
            return null;
        }
        public User Register(string userName, string password)
        {
            if (UserRepository.CheckUsernameExists(userName)) return null;
            User u = new User
            {
                UserName = userName,
                Password = Hash.Hash(password)
            };
            UserRepository.Insert(u);
            return u;
        }
    }
}