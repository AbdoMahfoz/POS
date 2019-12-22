using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Acr.UserDialogs;
using MobileApp.Models.DataModels;
using MobileApp.PageModels.Admin;
using UserService;
using Xamarin.Forms;

namespace MobileApp.PageModels.User
{
    public class ProductsPageModel : BasePageModel
    {
        private Category _category;
        public Command RefreshCommand { get; set; }
        public bool IsRefreshing { get; set; }

        public ProductsPageModel()
        {
            Title = "Products";
            Products = new ObservableCollection<ShoppingItemModel>
            {
                new ShoppingItemModel()
            };
            RefreshCommand = new Command(async () => await RefreshExecute());
        }

        private async Task RefreshExecute()
        {

            IsRefreshing = true;
            if (_category == null)
                try
                {
                    ItemResult[] allProducts = null;
                    await Task.Run(() =>
                    {
                        allProducts = App.UserBackendClient.GetItems();

                    });
                    Products.Clear();
                    foreach (var item in allProducts) Products.Add(new ShoppingItemModel(item));
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception);
                    throw;
                }
            else
                try
                {
                    ItemResult[] allProducts = null;
                    await Task.Run(() =>
                     {
                         allProducts = App.UserBackendClient.GetItemsInCategry(_category.Name);

                     });
                    Products.Clear();
                    foreach (var item in allProducts) Products.Add(new ShoppingItemModel(item));

                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception);
                    throw;
                }

            IsRefreshing = false;
        }

        public bool IsAdmin => App.IsAdmin;
        public ShoppingItemModel SelectedItem { get; set; }

        public Command OpenAddModalCommand
        {
            get
            {
                return new Command(
                    async () => { await CoreMethods.PushPageModel<AddEditProductPageModel>(null, true); });
            }
        }

        public Command ItemSelectedCommand
        {
            get
            {
                return new Command(async () =>
                {
                    await CoreMethods.PushPageModel<ProductDetailsPageModel>(SelectedItem);
                });
            }
        }

        public ObservableCollection<ShoppingItemModel> Products { get; set; }


        public override async Task Init(object initData)
        {
            if (initData == null)
            {
                _category = null;
            }
            else
            {
                var category = (Category)initData;
            }

            await base.Init(initData);
        }
    }
}