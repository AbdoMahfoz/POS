using System.Linq;
using Backend.Models;

namespace Backend.Repository.ExtendedRepositories
{
    public interface IUserRepository : IRepository<User>
    {
        User GetUser(string email);
        bool CheckEmailExists(string email);
        bool CheckExists(int UserId);
    }
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(ApplicationDbContext context) : base(context) { }
        public User GetUser(string email)
        {
            return (from user in GetAll()
                    where user.Email == email
                    select user).SingleOrDefault();
        }
        public bool CheckEmailExists(string email)
        {
            return (from user in GetAll()
                    where user.Email == email
                    select user).Any();
        }
        public bool CheckExists(int UserId)
        {
            return (from user in GetAll()
                    where user.Id == UserId
                    select user).Any();
        }
    }
}
