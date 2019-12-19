using Backend.DataContracts;
using Backend.Models;
using Backend.Security.Interfaces;

namespace Backend.Security.Implementations
{
    public abstract class AuthenticatedService : IAuthenticatedService
    {
        private readonly IAuth Auth;
        protected User User { get; private set; }
        public AuthenticatedService(IAuth Auth)
        {
            this.Auth = Auth;
        }
        public bool Login(string Email, string Password)
        {
            User u = Auth.Login(Email, Password);
            if(u != null)
            {
                User = u;
                return true;
            }
            return false;
        }
        public bool Register(UserDataRequest request)
        {
            User u = Auth.Register(request);
            if (u != null)
            {
                User = u;
                return true;
            }
            return false;
        }
    }
}