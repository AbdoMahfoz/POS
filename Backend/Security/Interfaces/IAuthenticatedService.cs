using System.ServiceModel;

namespace Backend.Security.Interfaces
{
    [ServiceContract(SessionMode = SessionMode.Required)]
    public interface IAuthenticatedService
    {
        [OperationContract]
        bool Login(string Username, string Password);
        [OperationContract]
        bool Register(string Username, string Password);
    }
}