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
            return User.UserName;
        }
    }
}
