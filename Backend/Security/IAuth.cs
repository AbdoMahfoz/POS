using Backend.Models;

namespace Backend.Security
{
    public interface IAuth
    {
        User GetUser(string token);
        string Login(string userName, string password);
        bool Register(string userName, string password);
    }
}