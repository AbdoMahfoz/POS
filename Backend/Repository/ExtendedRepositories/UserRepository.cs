using System.Linq;
using Backend.Models;

namespace Backend.Repository.ExtendedRepositories
{
    public interface IUserRepository : IRepository<User>
    {
        User GetUser(string username);
        bool CheckUsernameExists(string username);
        bool CheckUserExists(int UserId);
    }
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(ApplicationDbContext context) : base(context) { }
        public User GetUser(string username)
        {
            return (from user in GetAll()
                    where user.UserName == username
                    select user).SingleOrDefault();
        }
        public bool CheckUsernameExists(string username)
        {
            return (from user in GetAll()
                    where user.UserName == username
                    select user).Any();
        }
        public bool CheckUserExists(int UserId)
        {
            return (from user in GetAll()
                    where user.Id == UserId
                    select user).Any();
        }
    }
}
