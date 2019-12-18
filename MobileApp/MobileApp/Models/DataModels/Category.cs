using System;
using System.Collections.Generic;
using System.Text;

namespace MobileApp.Models.DataModels
{
    public class Category : BaseDataModel
    {
        public string Name { get; set; }
        public List<ShoppingItem> Items { get; set; }
    }
}
