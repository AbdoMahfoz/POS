using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Models;

namespace Backend.Repository
{
    public interface IRepository<T> where T : BaseModel
    {
        IQueryable<T> GetAll();
        T Get(int id);
        bool Exists(int id);
        Task Insert(T entity);
        Task InsertRange(IEnumerable<T> entities);
        void Update(T entity);
        void SoftDelete(T entity);
        void SoftDeleteRange(IEnumerable<T> entities);
        void HardDelete(T entity);
        void HardDeleteRange(IEnumerable<T> entities);
        void SaveChanges();
    }
}
