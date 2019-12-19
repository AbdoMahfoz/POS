using Backend.Models;
using System.Linq;
using System.ServiceModel;
using System.Threading.Tasks;

namespace Backend.Repository.ExtendedRepositories
{
    public interface ICategoryRepository : IRepository<Category>
    {
        Category Get(string name);
    }
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        public CategoryRepository(ApplicationDbContext context) : base(context) { }
        public Category Get(string name)
        {
            return GetAll().Where(u => u.Name == name).SingleOrDefault();
        }
        public override Task Insert(Category entity)
        {
            if (GetAll().Where(u => u.Name == entity.Name).Any()) throw new FaultException($"Category with name {entity.Name} already exists");
            return Insert(entity);
        }
    }
}