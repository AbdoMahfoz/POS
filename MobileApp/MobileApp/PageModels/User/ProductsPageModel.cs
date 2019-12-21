using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Acr.UserDialogs;
using MobileApp.Models.DataModels;

namespace MobileApp.PageModels.User
{
    public class ProductsPageModel : BasePageModel
    {
        public ProductsPageModel()
        {
            Title = "Products";
            Products = new ObservableCollection<ShoppingItemModel>()
            {
                new ShoppingItemModel(),
            };
        }

        public ObservableCollection<ShoppingItemModel> Products { get; set; }

        public override async Task Init(object initData)
        {
            //UserDialogs.Instance.ShowLoading();
            //if (initData == null)
            //{
            //    try
            //    {
            //        var allProducts = await App.UserBackendClient.GetItemsAsync();
            //        foreach (var item in allProducts) Products.Add(new ShoppingItemModel(item));
            //    }
            //    catch (Exception e)
            //    {
            //        Console.WriteLine(e);
            //        throw;
            //    }
            //}
            //else
            //{
            //    var category = (Category)initData;

            //    try
            //    {
            //        var allProducts = await App.UserBackendClient.GetItemsInCategryAsync(category.Name);
            //        foreach (var item in allProducts) Products.Add(new ShoppingItemModel(item));
            //    }
            //    catch (Exception e)
            //    {
            //        Console.WriteLine(e);
            //        throw;
            //    }
            //}

            //UserDialogs.Instance.HideLoading();
            await base.Init(initData);
        }
    }
}