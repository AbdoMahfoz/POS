using System.Collections.Generic;

namespace MobileApp.Models.DataModels
{
    public class Category : BaseDataModel
    {
        public string Name { get; set; }
        public List<ShoppingItem> Items { get; set; }
    }
}