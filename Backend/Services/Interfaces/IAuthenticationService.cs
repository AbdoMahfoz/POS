using Backend.DataContracts;
using System.ServiceModel;

namespace Backend.Services
{
    [ServiceContract]
    public interface IAuthenticationService
    {
        [OperationContract]
        string Login(string email, string password);
        [OperationContract]
        string Register(UserDataRequest userData);
        [OperationContract]
        string RegisterAdmin(string adminToken, UserDataRequest adminData);
        [OperationContract]
        string RefreshToken(string token);
        [OperationContract]
        bool IsAdmin(string token);
    }
}
