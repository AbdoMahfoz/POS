
namespace Backend.Security.Interfaces
{
    public interface IHash
    {
        string Hash(string target);
        bool Validate(string target, string hashed);
    }
}