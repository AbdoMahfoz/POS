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

        public bool IsAdmin
        {
            get => App.IsAdmin;
            set => App.IsAdmin = value;
        }

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
            //if (await Login()) 
            LoadMasterDetail();
        }

        public void LoadMasterDetail()
        {
            var masterDetailNav = new FreshMasterDetailNavigationContainer();
            masterDetailNav.Init("Hello", "Menu.png");
            masterDetailNav.AddPage<AddEditProductPageModel>("Manage Your Products");
            masterDetailNav.AddPage<CategoriesPageModel>("Categories");
            masterDetailNav.AddPage<AddCategoryPageModel>("Add Categories");
            masterDetailNav.AddPage<ProductsPageModel>("Products");
            Application.Current.MainPage = masterDetailNav;
        }

        private Task<bool> Login()
        {
            UserDialogs.Instance.ShowLoading();
            try
            {
                if (App.IsAdmin)
                    return App.AdminBackendClient.LoginAsync(Email, Password);
                else
                    return App.UserBackendClient.LoginAsync(Email, Password);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                //UserDialogs.Instance.HideLoading();
                UserDialogs.Instance.Alert(e.Message, "Error");
                return Task.FromResult(false);
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