using Backend.DataContracts;
using Backend.Shared;
using System.ServiceModel;

namespace Backend.Services
{
    [ServiceContract]
    public interface IAdminService : IItemService
    {
        [OperationContract]
        void InsertItem(string token, ItemRequest item);
        [OperationContract]
        void UpdateItem(string token, ItemResult item);
        [OperationContract]
        void DeleteItem(string token, int ItemId);
        [OperationContract]
        void AddItemToCategory(string token, int ItemId, string Category);
        [OperationContract]
        void RemoveItemCateogry(string token, int ItemId, string Category);
        [OperationContract]
        void AddCategory(string token, string newCategory);
    }
}
