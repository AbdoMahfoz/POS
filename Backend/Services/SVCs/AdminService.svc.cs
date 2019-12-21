using Backend.DataContracts;
using Backend.Models;
using Backend.Repository;
using Backend.Repository.ExtendedRepositories;
using Backend.Security.Interfaces;
using Backend.Shared;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;

namespace Backend.Services
{
    [ServiceBehavior]
    public class AdminService : ItemService, IAdminService
    {
        private readonly IRepository<Item> ItemRepository;
        private readonly IItemCategoryRepository ItemCategoryRepository;
        private readonly ICategoryRepository CategoryRepository;
        private readonly IAuth Auth;
        public AdminService(IRepository<Item> ItemRepository, IAuth Auth, IItemCategoryRepository ItemCategoryRepository,
                            ICategoryRepository CategoryRepository) :base(CategoryRepository, ItemRepository) 
        {
            this.ItemRepository = ItemRepository;
            this.ItemCategoryRepository = ItemCategoryRepository;
            this.CategoryRepository = CategoryRepository;
            this.Auth = Auth;
        }
        public void AddCategory(string token, string newCategory)
        {
            Auth.EnsureAuthorizedAsAdmin(token);
            CategoryRepository.Insert(new Category { Name = newCategory }).Wait();
        }
        public void AddItemToCategory(string token, int ItemId, string Category)
        {
            Auth.EnsureAuthorizedAsAdmin(token);
            ItemCategoryRepository.AddItemToCateogry(ItemId, Category);
        }
        public void DeleteItem(string token, int ItemId)
        {
            Auth.EnsureAuthorizedAsAdmin(token);
            Item item = ItemRepository.Get(ItemId);
            if (item == null) throw new FaultException($"400 Id {ItemId} doesn't exist");
            ItemRepository.SoftDelete(item);
        }
        public void InsertItem(string token, ItemRequest item)
        {
            Auth.EnsureAuthorizedAsAdmin(token);
            Item x = Helpers.MapTo<Item>(item);
            ItemRepository.Insert(x).Wait();
            foreach(string category in item.Categories)
            {
                ItemCategoryRepository.AddItemToCateogry(x.Id, category);
            }
        }
        public void RemoveItemCateogry(string token, int ItemId, string Category)
        {
            Auth.EnsureAuthorizedAsAdmin(token);
            ItemCategoryRepository.RemoveItemFromCategory(ItemId, Category);
        }
        public void UpdateItem(string token, ItemResult item)
        {
            Auth.EnsureAuthorizedAsAdmin(token);
            Item res = ItemRepository.Get(item.Id);
            if (res == null) throw new FaultException($"400 Id {item.Id} doesn't exist");
            IEnumerable<string> ExisitingCategories = res.Categories.Select(x => x.Category.Name);
            foreach (string category in (from category in ExisitingCategories
                                         where !item.Categories.Contains(category)
                                         select category))
            {
                ItemCategoryRepository.RemoveItemFromCategory(res.Id, category);
            }
            foreach (string category in (from category in item.Categories
                                         where !ExisitingCategories.Contains(category)
                                         select category))
            {
                ItemCategoryRepository.AddItemToCateogry(res.Id, category);
            }
            Helpers.UpdateObject(res, Helpers.MapTo<Item>(item));
            ItemRepository.Update(res);
        }
    }
}
