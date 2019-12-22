using System;
using System.Threading.Tasks;
using Acr.UserDialogs;
using FreshMvvm;
using MobileApp.Helpers;
using MobileApp.PageModels.Admin;
using MobileApp.PageModels.User;
using Xamarin.Forms;

namespace MobileApp.PageModels.Shared

{
    public class LoginPageModel : BasePageModel
    {
        public LoginPageModel()
        {
            NavigateToLogin = new Command(async () => await LoginExecute());
        }

        public bool IsAdmin => App.IsAdmin;



        public string Email { get; set; }
        public string Password { get; set; }
        public Command NavigateToLogin { get; set; }

        public Command NavigateToRegistration
        {
            get { return new Command(async () => { await CoreMethods.PushPageModel<RegistrationPageModel>(); }); }
        }

        public Command NavigateToGPlus { get; set; }

        private async Task LoginExecute()
        {
            if (!IsLoginDataValid()) return;
            if (await Login())
            {
                LoadMasterDetail();
                UserDialogs.Instance.Alert(App.Token, "HEY ABDO");
            }
            else
                UserDialogs.Instance.Alert("Email or password is incorrect", "Something went wrong");

        }

        public void LoadMasterDetail()
        {
            var masterDetailNav = new FreshMasterDetailNavigationContainer();
            masterDetailNav.Init("Hello", "Menu.png");
            masterDetailNav.AddPage<CategoriesPageModel>("Categories");
            masterDetailNav.AddPage<ProductsPageModel>("Products");
            Application.Current.MainPage = masterDetailNav;
        }

        private async Task<bool> Login()
        {
            UserDialogs.Instance.ShowLoading();
            try
            {
                string token = "";
                await Task.Run(() =>
                {
                    token = App.AuthenticationClient.Login(Email, Password);
                });
                if (string.IsNullOrWhiteSpace(token)) return false;
                App.Token = token;
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                //UserDialogs.Instance.HideLoading();
                UserDialogs.Instance.Alert(e.Message, "Error");
                return false;
            }
            finally
            {
                UserDialogs.Instance.HideLoading();
            }
        }

        private bool IsLoginDataValid()
        {
            if (string.IsNullOrWhiteSpace(Email) || string.IsNullOrWhiteSpace(Password))
            {
                UserDialogs.Instance.Alert("Please fill in the required data", "Invalid or Missing Data");
                return false;
            }

            if (!EmailValidator.IsEmailValid(Email))
            {
                UserDialogs.Instance.Alert("Email or Password is incorrect", "Invalid or Missing Data");
                return false;
            }

            return true;
        }
    }
}