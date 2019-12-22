using System;
using System.IO;
using UserService;
using Xamarin.Forms;

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

    public class ShoppingItemModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ImageSource Logo { get; set; }
        public double Price { get; set; }
        public string ItemCategory { get; set; }

        public ShoppingItemModel()
        {

        }

        public ShoppingItemModel(ItemResult item)
        {
            Id = item.Id;
            Name = item.Name;
            Description = item.Description;
            ItemCategory = item.Categories[0];
            Price = Convert.ToDouble(item.Price);
            Logo = ImageSource.FromStream(() => new MemoryStream(Convert.FromBase64String(item.Base64Image)));
        }

        public ShoppingItemModel(ShoppingItem item)
        {
            Name = item.Name;
            Id = item.Id;
            Description = item.Description;
            ItemCategory = item.ItemCategory.Name;
            Price = item.Price;
            Logo = ImageSource.FromStream(() => new MemoryStream(Convert.FromBase64String(item.Logo)));
        }
    }
}