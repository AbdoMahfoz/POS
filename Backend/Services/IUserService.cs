using Backend.DataContracts;
using Backend.Security.Interfaces;
using Backend.Shared;
using System.ServiceModel;

namespace Backend.Services
{
    [ServiceContract(SessionMode = SessionMode.Required)]
    public interface IUserService : IAuthenticatedService, IItemService
    {
        [OperationContract(IsOneWay = true)]
        void AddToCart(int ItemId);
        [OperationContract]
        void SetItemCountInCart(int ItemId, int newCount);
        [OperationContract]
        void DeleteItemFromCart(int ItemId);
        [OperationContract]
        ItemResult[] GetCart();
        [OperationContract]
        bool PurchaseCart(bool IsCreditCard, int? pin = null);
        [OperationContract]
        ItemResult[] GetPurchaseHistory();
    }
}
