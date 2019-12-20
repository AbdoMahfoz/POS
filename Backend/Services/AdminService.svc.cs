using Backend.DataContracts;
using Backend.Models;
using Backend.Repository;
using Backend.Repository.ExtendedRepositories;
using Backend.Security.Implementations;
using Backend.Security.Interfaces;
using Backend.Shared;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;

namespace Backend.Services
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerSession)]
    public class AdminService : AuthenticatedService, IAdminService
    {
        private readonly IRepository<Item> ItemRepository;
        private readonly IItemCategoryRepository ItemCategoryRepository;
        private readonly ICategoryRepository CategoryRepository;
        private readonly IItemService ItemService;
        public AdminService(IRepository<Item> ItemRepository, IAuth Auth, IItemService ItemService,
            IItemCategoryRepository ItemCategoryRepository, ICategoryRepository CategoryRepository) : base(Auth) 
        {
            this.ItemRepository = ItemRepository;
            this.ItemCategoryRepository = ItemCategoryRepository;
            this.CategoryRepository = CategoryRepository;
            this.ItemService = ItemService;
        }
        public override bool Login(string Email, string Password)
        {
            bool res = base.Login(Email, Password);
            if(res)
            {
                AssertAuthentication(true);
            }
            return res;
        }
        public override bool Register(UserDataRequest request)
        {
            AssertAuthentication(true);
            return Register(request, true);
        }
        public void AddCategory(string newCategory)
        {
            AssertAuthentication(true);
            CategoryRepository.Insert(new Category { Name = newCategory }).Wait();
        }
        public void AddItemToCategory(int ItemId, string Category)
        {
            AssertAuthentication(true);
            ItemCategoryRepository.AddItemToCateogry(ItemId, Category);
        }
        public void DeleteItem(int ItemId)
        {
            AssertAuthentication(true);
            Item item = ItemRepository.Get(ItemId);
            if (item == null) throw new FaultException($"400 Id {ItemId} doesn't exist");
            ItemRepository.SoftDelete(item);
        }
        public void InsertItem(ItemRequest item)
        {
            AssertAuthentication(true);
            Item x = Helpers.MapTo<Item>(item);
            ItemRepository.Insert(x).Wait();
            foreach(string category in item.Categories)
            {
                ItemCategoryRepository.AddItemToCateogry(x.Id, category);
            }
        }
        public void RemoveItemCateogry(int ItemId, string Category)
        {
            AssertAuthentication(true);
            ItemCategoryRepository.RemoveItemFromCategory(ItemId, Category);
        }
        public void UpdateItem(ItemResult item)
        {
            AssertAuthentication(true);
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
        public ItemResult[] GetItems()
        {
            AssertAuthentication(true);
            return ItemService.GetItems();
        }
        public string[] GetCategories()
        {
            AssertAuthentication(true);
            return ItemService.GetCategories();
        }
        public ItemResult[] GetItemsInCategry(string Category)
        {
            AssertAuthentication(true);
            return ItemService.GetItemsInCategry(Category);
        }
    }
}
