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
        public Command CloseCommand
        {
            get { return new Command(async () => { await CoreMethods.PopPageModel(null, true); }); }
        }

        public Command CheckoutCommand
        {
            get { return new Command(async () => { await CoreMethods.PushPageModel<CheckoutPageModel>(FinalCost); }); }
        }

        public bool IsRefreshing { get; set; }
        public double FinalCost { get; set; } = 0.0;
        public ObservableCollection<ShoppingItemModel> CartItems { get; set; }

        public CartPageModel()
        {
            Title = "Your Cart";
            CartItems = new ObservableCollection<ShoppingItemModel>();
            DeleteCommand = new Command(async o => await DeleteExecute(o));
            PulltoRefreshCommand = new Command(async () => await PulltoRefreshExecute());
            CartItems.CollectionChanged += CartItems_CollectionChanged;
        }

        private void CartItems_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            FinalCost = 0.0;
            foreach (var item in CartItems)
            {
                FinalCost += item.Price;
            }
        }

        private async Task DeleteExecute(object obj)
        {
            UserDialogs.Instance.ShowLoading();
            if (obj == null) return;
            var item = (ShoppingItemModel)obj;

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
            IsRefreshing = true;
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
                IsRefreshing = false;
            }
        }

        public override async Task Init(object initData)
        {
            UserDialogs.Instance.ShowLoading();
            try
            {
                ItemResult[] allProducts = null;
                await Task.Run(() => { allProducts = App.UserBackendClient.GetCart(App.Token); });
                CartItems.Clear();
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
            await base.Init(initData);
        }
    }
}