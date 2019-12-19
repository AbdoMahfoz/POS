using Backend.DataContracts;
using Backend.Models;

namespace Backend.Security.Interfaces
{
    public interface IAuth
    {
        User Login(string email, string password);
        User Register(UserDataRequest request);
    }
}