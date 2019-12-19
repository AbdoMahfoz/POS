using Backend.DataContracts;
using Backend.Models;
using Backend.Security.Interfaces;
using System.ServiceModel;

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
        public virtual bool Login(string Email, string Password)
        {
            User u = Auth.Login(Email, Password);
            if(u != null)
            {
                User = u;
                return true;
            }
            return false;
        }
        public virtual bool Register(UserDataRequest request)
        {
            return Register(request);
        }
        public virtual bool Register(UserDataRequest request, bool IsAdmin = false)
        {
            User u = Auth.Register(request, IsAdmin);
            if (u != null)
            {
                User = u;
                return true;
            }
            return false;
        }
        public virtual UserDataResponse GetUserInfo()
        {
            if (User == null) throw new FaultException("400 User not logged in");
            return Helpers.MapTo<UserDataResponse>(User);
        }
        protected void AssertAuthentication(bool MustBeAdmin = false)
        {
            if (User == null || (MustBeAdmin && !User.IsAdmin))
            {
                User = null;
                throw new FaultException("401 Unauthorized");
            }
        }
        public void Logout()
        {
            User = null;
        }
    }
}