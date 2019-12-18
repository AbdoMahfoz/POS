using Backend.Models;
using Backend.Security.Interfaces;
using System.ServiceModel;

namespace Backend.Security.Implementations
{
    //[ServiceBehavior(InstanceContextMode = InstanceContextMode.PerSession)]
    public abstract class AuthenticatedService : IAuthenticatedService
    {
        private readonly IAuth Auth;
        protected User User { get; private set; }
        public AuthenticatedService(IAuth Auth)
        {
            this.Auth = Auth;
        }
        public bool Login(string Username, string Password)
        {
            User u = Auth.Login(Username, Password);
            if(u != null)
            {
                User = u;
                return true;
            }
            return false;
        }
        public bool Register(string Username, string Password)
        {
            User u = Auth.Register(Username, Password);
            if (u != null)
            {
                User = u;
                return true;
            }
            return false;
        }
    }
}