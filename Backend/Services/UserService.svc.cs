using System.Linq;
using System.ServiceModel;
using Backend.DataContracts;
using Backend.Repository.ExtendedRepositories;
using Backend.Security.Implementations;
using Backend.Security.Interfaces;
using Backend.Models;
using Backend.Shared;

namespace Backend.Services
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerSession)]
    public class UserService : AuthenticatedService, IUserService
    {
        private readonly IUserHistoryRepository UserHistoryRepository;
        private readonly IItemService ItemService;
        public UserService(IUserHistoryRepository UserHistoryRepository, IAuth Auth, IItemService ItemService) : base(Auth)
        {
            this.UserHistoryRepository = UserHistoryRepository;
            this.ItemService = ItemService;
        }
        public void AddToCart(int ItemId)
        {
            AssertAuthentication();
            UserHistoryRepository.AddToCart(User.Id, ItemId);
        }
        public ItemResult[] GetCart()
        {
            AssertAuthentication();
            return UserHistoryRepository.GetUserCart(User.Id).Select(u => u.Item).ToArray()
                                                             .Select(u => u.ToItemResult()).ToArray();
        }
        public ItemResult[] GetPurchaseHistory()
        {
            AssertAuthentication();
            return UserHistoryRepository.GetUserPurchases(User.Id).Select(u => u.Item).ToArray()
                                                                  .Select(u => u.ToItemResult()).ToArray();
        }
        public bool PurchaseCart(bool IsCreditCard, int? pin = null)
        {
            AssertAuthentication();
            return UserHistoryRepository.PerformPuchaseOnCart(User.Id, IsCreditCard? PurchaseMethod.CreditCard : PurchaseMethod.Cash);
            //Take money from da incredibale userie
        }
        public void SetItemCountInCart(int ItemId, int newCount)
        {
            AssertAuthentication();
            UserHistoryRepository.SetItemCountInCart(User.Id, ItemId, newCount);
        }
        public void DeleteItemFromCart(int ItemId)
        {
            AssertAuthentication();
            UserHistoryRepository.RemoveItemFromCart(User.Id, ItemId);
        }
        public ItemResult[] GetItems()
        {
            AssertAuthentication();
            return ItemService.GetItems();
        }
        public string[] GetCategories()
        {
            AssertAuthentication();
            return ItemService.GetCategories();
        }
        public ItemResult[] GetItemsInCategry(string Category)
        {
            AssertAuthentication();
            return ItemService.GetItemsInCategry(Category);
        }
    }
}
