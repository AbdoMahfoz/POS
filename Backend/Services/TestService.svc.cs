using Backend.DataContracts;
using Backend.Security.Implementations;
using Backend.Security.Interfaces;
using System.ServiceModel;

namespace Backend.Services
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerSession)]
    public class TestService : AuthenticatedService, ITestService
    {
        public TestService(IAuth Auth) : base(Auth) { }
        public string DoWork()
        {
            if(User == null)
            {
                return "Unauthenticated";
            }
            return User.Email;
        }
        public RegisterRequest GetCurrentUser()
        {
            if (User == null) return null;
            return Helpers.MapTo<RegisterRequest>(User);
        }
    }
}
