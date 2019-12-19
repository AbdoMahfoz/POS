using Backend.DataContracts;
using Backend.Models;
using Backend.Repository;
using Backend.Security.Implementations;
using Backend.Security.Interfaces;
using System.Linq;
using System.ServiceModel;

namespace Backend.Services
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerSession)]
    public class AdminService : AuthenticatedService, IAdminService
    {
        private readonly IRepository<Item> ItemRepository;
        public AdminService(IRepository<Item> ItemRepository, IAuth Auth) : base(Auth) 
        {
            this.ItemRepository = ItemRepository;
        }
        public void DeleteItem(int ItemId)
        {
            Item item = ItemRepository.Get(ItemId);
            if (item == null) throw new FaultException($"400 Id {ItemId} doesn't exist");
            ItemRepository.SoftDelete(item);
        }
        public ItemResult[] GetItems()
        {
            return ItemRepository.GetAll().ToArray()
                                          .Select(u => Helpers.MapTo<ItemResult>(u)).ToArray();
        }
        public void InsertItem(ItemRequest item)
        {
            ItemRepository.Insert(Helpers.MapTo<Item>(item)).Wait();
        }
        public void UpdateItem(ItemResult item)
        {
            Item res = ItemRepository.Get(item.Id);
            if (res == null) throw new FaultException($"400 Id {item.Id} doesn't exist");
            Helpers.UpdateObject(res, Helpers.MapTo<Item>(item));
            ItemRepository.Update(res);
        }
    }
}
