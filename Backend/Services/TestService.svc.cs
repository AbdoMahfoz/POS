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
        public string TestAuthentication()
        {
            if(User == null)
            {
                return "Unauthenticated";
            }
            return $"Authenticated as {User.Email}";
        }
        public UserDataResponse GetCurrentUser()
        {
            if (User == null) return null;
            return Helpers.MapTo<UserDataResponse>(User);
        }
    }
}
