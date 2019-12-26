using System;
using System.Threading.Tasks;
using Acr.UserDialogs;
using Xamarin.Forms;

namespace MobileApp.PageModels.User
{
    public class CheckoutPageModel : BasePageModel
    {
        public string NameOnCard { get; set; }
        public string CardNumber { get; set; }
        public string CVVNumber { get; set; }
        public bool IsCreditCard { get; set; }
        public double Amount { get; set; }
        public int? Pin { get; set; }
        public Command CheckoutCommand { get; set; }

        public CheckoutPageModel()
        {
            Title = "Checkout";
            CheckoutCommand = new Command(async () => await CheckoutExecute());
        }

        private async Task CheckoutExecute()
        {
            UserDialogs.Instance.ShowLoading();
            try
            {
                await Task.Run(() => { App.UserBackendClient.PurchaseCart(App.Token, IsCreditCard, Pin); });
                UserDialogs.Instance.Alert("Cart Purchased!", "Success");
                await CoreMethods.SwitchSelectedRootPageModel<ProductsPageModel>();
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
            if (initData != null)
            {
                Amount = Convert.ToDouble(initData);
            }
            return base.Init(initData);
        }

    }
}
