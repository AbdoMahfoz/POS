using System;
using System.Collections.Generic;
using System.Text;

namespace MobileApp.Models.DataModels
{
    public class ShoppingItem : BaseDataModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Logo { get; set; }
        public double Price { get; set; }
        public Category ItemCategory { get; set; }
    }
}
