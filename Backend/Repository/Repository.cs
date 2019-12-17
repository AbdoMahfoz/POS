using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Backend.Models;

namespace Backend.Repository
{
    public class Repository<T> : IRepository<T> where T : BaseModel
    {
        protected readonly ApplicationDbContext Context;
        protected DbSet<T> Entities;
        public Repository(ApplicationDbContext context)
        {
            Context = context;
            Entities = context.Set<T>();
        }
        public virtual IQueryable<T> GetAll()
        {
            return from row in Entities
                   where !row.IsDeleted
                   select row;
        }
        public virtual T Get(int id)
        {
            var result = GetAll().Where(e => e.Id == id).FirstOrDefault();
            return result;
        }
        public virtual async Task Insert(T entity)
        {
            entity.AddedDate = DateTime.UtcNow;
            await Task.Run(() => Entities.Add(entity));
            SaveChanges();
        }
        public virtual async Task InsertRange(IEnumerable<T> Entities)
        {
            foreach (var item in Entities) item.AddedDate = DateTime.UtcNow;
            await Task.Run(() => this.Entities.AddRange(Entities.ToArray()));
            SaveChanges();
        }
        public virtual void Update(T entity)
        {
            Entities.Attach(entity);
            entity.ModifiedDate = DateTime.UtcNow;
            SaveChanges();
        }
        public virtual void SoftDelete(T entity)
        {
            entity.IsDeleted = true;
            entity.DeletedDate = DateTime.UtcNow;
            Entities.Attach(entity);
            SaveChanges();
        }
        public virtual void SoftDeleteRange(IEnumerable<T> Entities)
        {
            foreach (var item in Entities)
            {
                this.Entities.Attach(item);
                item.IsDeleted = true;
                item.DeletedDate = DateTime.UtcNow;
            }
            SaveChanges();
        }
        public virtual void HardDelete(T entity)
        {
            Entities.Remove(entity);
            SaveChanges();
        }
        public virtual void HardDeleteRange(IEnumerable<T> Entities)
        {
            this.Entities.RemoveRange(Entities);
            SaveChanges();
        }
        public void SaveChanges()
        {
            Context.SaveChanges();
        }
    }
}