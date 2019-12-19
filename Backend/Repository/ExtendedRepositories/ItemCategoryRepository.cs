using Backend.Models;
using System.Linq;
using System.ServiceModel;

namespace Backend.Repository.ExtendedRepositories
{
    public interface IItemCategoryRepository : IRepository<ItemCategory>
    {
        void AddItemToCateogry(int ItemId, string CategoryName);
        void RemoveItemFromCategory(int ItemId, string CategoryName);
    }
    public class ItemCategoryRepository : Repository<ItemCategory>, IItemCategoryRepository
    {
        private readonly ICategoryRepository CategoryRepository;
        public ItemCategoryRepository(ApplicationDbContext context, ICategoryRepository CategoryRepository) : base(context) 
        {
            this.CategoryRepository = CategoryRepository;
        }
        public void AddItemToCateogry(int ItemId, string CategoryName)
        {
            bool exists = (from item in GetAll()
                           where item.ItemId == ItemId && item.Category.Name == CategoryName
                           select item).Any();
            if (exists) throw new FaultException($"Item #{ItemId} exists in Category {CategoryName}");
            Insert(new ItemCategory
            {
                ItemId = ItemId,
                CategoryId = CategoryRepository.Get(CategoryName).Id
            }).Wait();
        }
        public void RemoveItemFromCategory(int ItemId, string CategoryName)
        {
            ItemCategory res = (from item in GetAll()
                                where item.ItemId == ItemId && item.Category.Name == CategoryName
                                select item).SingleOrDefault();
            if (res == null) throw new FaultException($"Item #{ItemId} doesn't belong to category {CategoryName}");
            SoftDelete(res);
        }
    }
}