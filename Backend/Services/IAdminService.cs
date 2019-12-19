using Backend.DataContracts;
using Backend.Security.Interfaces;
using System.ServiceModel;

namespace Backend.Services
{
    [ServiceContract(SessionMode = SessionMode.Required)]
    public interface IAdminService : IAuthenticatedService
    {
        [OperationContract]
        void InsertItem(ItemRequest item);
        [OperationContract]
        void UpdateItem(ItemResult item);
        [OperationContract]
        void DeleteItem(int ItemId);
        [OperationContract]
        ItemResult[] GetItems();
    }
}
