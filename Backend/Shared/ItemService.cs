using System.Linq;
using System.ServiceModel;
using Backend.DataContracts;
using Backend.Models;
using Backend.Repository;
using Backend.Repository.ExtendedRepositories;

namespace Backend.Shared
{
    [ServiceContract]
    public interface IItemService
    {
        [OperationContract]
        ItemResult[] GetItems();
        [OperationContract]
        string[] GetCategories();
        [OperationContract]
        ItemResult[] GetItemsInCategry(string Category);
    }
    [ServiceBehavior]
    public class ItemService : IItemService
    {
        private readonly ICategoryRepository CategoryRepository;
        private readonly IRepository<Item> ItemRepository;
        public ItemService(ICategoryRepository CategoryRepository, IRepository<Item> ItemRepository)
        {
            this.CategoryRepository = CategoryRepository;
            this.ItemRepository = ItemRepository;
        }
        public string[] GetCategories()
        {
            return CategoryRepository.GetAll().Select(x => x.Name).ToArray();
        }
        public ItemResult[] GetItems()
        {
            return ItemRepository.GetAll().ToArray()
                                          .Select(u => u.ToItemResult()).ToArray();
        }
        public ItemResult[] GetItemsInCategry(string Category)
        {
            Category res = CategoryRepository.Get(Category);
            if (res == null) throw new FaultException($"Category {Category} doesn't exist");
            return res.Items.Select(u => u.Item.ToItemResult()).ToArray();
        }
    }
}