using Backend.DataContracts;
using Backend.Models;

namespace Backend.Security.Interfaces
{
    public interface IAuth
    {
        string Login(string email, string password);
        int GetUserId(string token, bool IsAdmin = false);
        void EnsureAuthorized(string token);
        void EnsureAuthorizedAsAdmin(string token);
        User Register(UserDataRequest request, bool IsAdmin = false);
    }
}