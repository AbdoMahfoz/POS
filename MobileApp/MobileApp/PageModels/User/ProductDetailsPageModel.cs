using System;
using System.Threading.Tasks;
using Acr.UserDialogs;
using MobileApp.Models.DataModels;
using MobileApp.PageModels.Admin;
using Xamarin.Forms;

namespace MobileApp.PageModels.User
{
    public class ProductDetailsPageModel : BasePageModel
    {
        public ProductDetailsPageModel()
        {
            Item = new ShoppingItemModel();
            Title = Item.Name;
            AddToCartCommand = new Command(async () => await AddToCartExecute());
            DeleteItemCommand = new Command(async () => await DeleteItemExecute());

        }

        private async Task DeleteItemExecute()
        {

            UserDialogs.Instance.ShowLoading();
            try
            {
                await Task.Run(() => { App.AdminBackendClient.DeleteItem(App.Token, Item.Id); });
                UserDialogs.Instance.Alert("Item deleted", "Success");
                await CoreMethods.PopPageModel();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                UserDialogs.Instance.Alert(e.Message, "Error");
            }
            finally
            {
                UserDialogs.Instance.HideLoading();
            }
        }

        public ShoppingItemModel Item { get; set; }
        public Command AddToCartCommand { get; set; }
        public Command OpenEditPageCommand
        {
            get
            {
                return new Command(
                    async () => { await CoreMethods.PushPageModel<AddEditProductPageModel>(Item, true); });
            }
        }

        public Command DeleteItemCommand { get; set; }

        private async Task AddToCartExecute()
        {
            UserDialogs.Instance.ShowLoading();
            try
            {
                await Task.Run(() => { App.UserBackendClient.AddToCart(App.Token, Item.Id); });
                UserDialogs.Instance.Alert("Item Added to Cart", "Success");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                UserDialogs.Instance.Alert(e.Message, "Error");
            }
            finally
            {
                UserDialogs.Instance.HideLoading();
            }
        }

        public override Task Init(object initData)
        {
            var shoppingItem = (ShoppingItemModel)initData;
            Item = shoppingItem;
            return base.Init(initData);
        }
    }
}