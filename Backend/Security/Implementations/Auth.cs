using Backend.DataContracts;
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
        public User Login(string email, string password)
        {
            User u = UserRepository.GetUser(email);
            if(u != null && Hash.Validate(password, u.Password))
            {
                return u;
            }
            return null;
        }
        public User Register(UserDataRequest request, bool IsAdmin = false)
        {
            if (UserRepository.CheckEmailExists(request.Email)) return null;
            User u = Helpers.MapTo<User>(request);
            u.IsAdmin = IsAdmin;
            u.Password = Hash.Hash(u.Password);
            UserRepository.Insert(u);
            return u;
        }
    }
}