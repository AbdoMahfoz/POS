using System.Linq;
using System.ServiceModel;
using Backend.DataContracts;
using Backend.Repository.ExtendedRepositories;
using Backend.Models;
using Backend.Shared;
using Backend.Repository;
using Backend.Security.Interfaces;

namespace Backend.Services
{
    [ServiceBehavior]
    public class UserService : ItemService, IUserService
    {
        private readonly IUserHistoryRepository UserHistoryRepository;
        private readonly IAuth Auth;
        public UserService(IUserHistoryRepository UserHistoryRepository, IAuth Auth,
            ICategoryRepository CategoryRepository, IRepository<Item> ItemRepository) : base(CategoryRepository, ItemRepository)
        {
            this.UserHistoryRepository = UserHistoryRepository;
            this.Auth = Auth;
        }
        public void AddToCart(string token, int ItemId)
        {
            UserHistoryRepository.AddToCart(Auth.GetUserId(token), ItemId);
        }
        public ItemResult[] GetCart(string token)
        {
            return UserHistoryRepository.GetUserCart(Auth.GetUserId(token)).Select(u => u.Item).ToArray()
                                                                           .Select(u => u.ToItemResult()).ToArray();
        }
        public ItemResult[] GetPurchaseHistory(string token)
        {
            return UserHistoryRepository.GetUserPurchases(Auth.GetUserId(token)).Select(u => u.Item).ToArray()
                                                                                .Select(u => u.ToItemResult()).ToArray();
        }
        public bool PurchaseCart(string token, bool IsCreditCard, int? pin = null)
        {
            return UserHistoryRepository.PerformPuchaseOnCart(Auth.GetUserId(token), IsCreditCard? PurchaseMethod.CreditCard : PurchaseMethod.Cash);
            //Take money from da incredibale userie
        }
        public void SetItemCountInCart(string token, int ItemId, int newCount)
        {
            UserHistoryRepository.SetItemCountInCart(Auth.GetUserId(token), ItemId, newCount);
        }
        public void DeleteItemFromCart(string token, int ItemId)
        {
            UserHistoryRepository.RemoveItemFromCart(Auth.GetUserId(token), ItemId);
        }
    }
}
