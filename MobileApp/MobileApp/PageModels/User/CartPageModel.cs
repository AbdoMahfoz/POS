using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Acr.UserDialogs;
using MobileApp.Models.DataModels;
using UserService;
using Xamarin.Forms;

namespace MobileApp.PageModels.User
{
    public class CartPageModel : BasePageModel
    {
        public Command DeleteCommand { get; set; }
        public Command PulltoRefreshCommand { get; set; }

        private ObservableCollection<ShoppingItemModel> CartItems { get; }

        public CartPageModel()
        {
            CartItems = new ObservableCollection<ShoppingItemModel>();
            DeleteCommand = new Command(async o => await DeleteExecute(o));
            PulltoRefreshCommand = new Command(async () => await PulltoRefreshExecute());
            Title = "Your Cart";
        }

        private async Task DeleteExecute(object obj)
        {
            UserDialogs.Instance.ShowLoading();
            if (obj == null) return;
            var item = (ShoppingItemModel) obj;

            try
            {
                await Task.Run(() => { App.UserBackendClient.DeleteItemFromCart(App.Token, item.Id); });
                CartItems.Remove(item);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                throw;
            }
            finally
            {
                UserDialogs.Instance.HideLoading();
            }
        }

        private async Task PulltoRefreshExecute()
        {
            UserDialogs.Instance.ShowLoading();
            try
            {
                ItemResult[] allProducts = null;
                await Task.Run(() => { allProducts = App.UserBackendClient.GetCart(App.Token); });
                foreach (var item in allProducts) CartItems.Add(new ShoppingItemModel(item));
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                throw;
            }
            finally
            {
                UserDialogs.Instance.HideLoading();
            }
        }
    }
}