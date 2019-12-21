using Backend.DataContracts;
using Backend.Shared;
using System.ServiceModel;

namespace Backend.Services
{
    [ServiceContract]
    public interface IUserService : IItemService
    {
        [OperationContract(IsOneWay = true)]
        void AddToCart(string token, int ItemId);
        [OperationContract]
        void SetItemCountInCart(string token, int ItemId, int newCount);
        [OperationContract]
        void DeleteItemFromCart(string token, int ItemId);
        [OperationContract]
        ItemResult[] GetCart(string token);
        [OperationContract]
        bool PurchaseCart(string token, bool IsCreditCard, int? pin = null);
        [OperationContract]
        ItemResult[] GetPurchaseHistory(string token);
    }
}
