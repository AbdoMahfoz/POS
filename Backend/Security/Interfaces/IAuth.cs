using Backend.Models;

namespace Backend.Security.Interfaces
{
    public interface IAuth
    {
        User Login(string userName, string password);
        User Register(string userName, string password);
    }
}