using Backend.DataContracts;
using System.ServiceModel;

namespace Backend.Security.Interfaces
{
    [ServiceContract(SessionMode = SessionMode.Required)]
    public interface IAuthenticatedService
    {
        [OperationContract]
        bool Login(string Email, string Password);
        [OperationContract]
        bool Register(UserDataRequest request);
    }
}