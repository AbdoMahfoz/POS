using Backend.DataContracts;
using Backend.Security.Interfaces;
using System.ServiceModel;

namespace Backend.Services
{
    [ServiceContract(SessionMode = SessionMode.Required)]
    public interface IUserService : IAuthenticatedService
    {
        [OperationContract]
        ItemResult[] GetItems();
        [OperationContract(IsOneWay = true)]
        void AddToCart(int ItemId);
        [OperationContract]
        void SetItemCountInCart(int ItemId, int newCount);
        [OperationContract]
        ItemResult[] GetCart();
        [OperationContract]
        bool PurchaseCart(bool IsCreditCard, int? pin = null);
        [OperationContract]
        ItemResult[] GetPurchaseHistory();
    }
}
