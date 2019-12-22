using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MobileApp.Models.DataModels;

namespace MobileApp.PageModels.User
{
    public class ProductDetailsPageModel : BasePageModel
    {
        public ShoppingItemModel Item { get; set; }
        public ProductDetailsPageModel()
        {
            Title = Item.Name;
        }

        public override Task Init(object initData)
        {
            ShoppingItemModel shoppingItem = (ShoppingItemModel)initData;
            Item = shoppingItem;
            return base.Init(initData);
        }
    }
}
