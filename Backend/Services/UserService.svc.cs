using System.Linq;
using System.ServiceModel;
using Backend.DataContracts;
using Backend.Repository.ExtendedRepositories;
using Backend.Security.Implementations;
using Backend.Security.Interfaces;
using Backend.Repository;
using Backend.Models;

namespace Backend.Services
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerSession)]
    public class UserService : AuthenticatedService, IUserService
    {
        private readonly IUserHistoryRepository UserHistoryRepository;
        private readonly IRepository<Item> ItemRepository;
        public UserService(IUserHistoryRepository UserHistoryRepository, IRepository<Item> ItemRepository, IAuth Auth) : base(Auth)
        {
            this.UserHistoryRepository = UserHistoryRepository;
            this.ItemRepository = ItemRepository;
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
                                                             .Select(u => Helpers.MapTo<ItemResult>(u)).ToArray();
        }
        public ItemResult[] GetItems()
        {
            AssertAuthentication();
            return ItemRepository.GetAll().ToArray()
                                          .Select(u => Helpers.MapTo<ItemResult>(u)).ToArray();
        }
        public ItemResult[] GetPurchaseHistory()
        {
            AssertAuthentication();
            return UserHistoryRepository.GetUserPurchases(User.Id).Select(u => u.Item).ToArray()
                                                                  .Select(u => Helpers.MapTo<ItemResult>(u)).ToArray();
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
    }
}
