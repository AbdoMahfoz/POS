using Backend.Models;
using System.Linq;
using System.Threading.Tasks;
using Backend.DataContracts;
using System.ServiceModel;

namespace Backend.Repository.ExtendedRepositories
{
    public interface IUserHistoryRepository : IRepository<UserHistory>
    {
        Task AddToCart(int UserId, int ItemId);
        void SetItemCountInCart(int UserId, int ItemId, int newCount);
        IQueryable<UserHistory> GetUserCart(int UserId);
        IQueryable<UserHistory> GetUserPurchases(int UserId);
        bool PerformPuchaseOnCart(int UserId, PurchaseMethod method);
    }
    public class UserHistoryRepository : Repository<UserHistory>, IUserHistoryRepository
    {
        public UserHistoryRepository(ApplicationDbContext context) : base(context) { }
        public async Task AddToCart(int UserId, int ItemId)
        {
            UserHistory prevInstance = (from item in GetAll()
                                        where item.UserId == UserId && item.ItemId == ItemId
                                        select item).SingleOrDefault();
            if (prevInstance == null)
            {
                await Insert(new UserHistory
                {
                    UserId = UserId,
                    ItemId = ItemId
                });
                return;
            }
            else
            {
                prevInstance.Count++;
                Update(prevInstance);
                return;
            }
        }
        public IQueryable<UserHistory> GetUserCart(int UserId)
        {
            return from item in GetAll()
                   where item.UserId == UserId && !item.IsPurchased
                   select item;
        }
        public IQueryable<UserHistory> GetUserPurchases(int UserId)
        {
            return from item in GetAll()
                   where item.UserId == UserId && item.IsPurchased
                   select item;
        }
        public bool PerformPuchaseOnCart(int UserId, PurchaseMethod method)
        {
            bool cartIsEmpty = true;
            foreach(var item in GetUserCart(UserId))
            {
                cartIsEmpty = false;
                item.IsPurchased = true;
                item.PurchaseMethod = method;
                Update(item);
            }
            return !cartIsEmpty;
        }
        public void SetItemCountInCart(int UserId, int ItemId, int newCount)
        {
            UserHistory x = (from item in GetAll()
                             where item.UserId == UserId && item.ItemId == ItemId
                             select item).SingleOrDefault();
            if (x == null) throw new FaultException($"400 User doesn't have an item with id {ItemId} in the cart");
            x.Count = newCount;
            Update(x);
        }
    }
}