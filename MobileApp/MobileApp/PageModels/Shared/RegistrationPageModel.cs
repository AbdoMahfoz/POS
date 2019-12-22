using System;
using System.Threading.Tasks;
using Acr.UserDialogs;
using AuthenticationService;
using MobileApp.Helpers;
using MobileApp.Models.DataModels;
using MobileApp.PageModels.Admin;
using UserService;
using Xamarin.Forms;

namespace MobileApp.PageModels.Shared
{
    public class RegistrationPageModel : BasePageModel
    {
        public RegistrationPageModel()
        {
            Title = "Registration Page";
            Account = new Account();
            RegisterCommand = new Command(async () => await RegisterExecute());
        }

        public string ConfirmPassword { get; set; }
        public Account Account { get; set; }
        public Command RegisterCommand { get; set; }

        private async Task RegisterExecute()
        {
            //if (!IsAccountValid()) return;
            //if (await RegisterAccount())
            //    await CoreMethods.PushPageModel<AddEditProductPageModel>();
        }

        private async Task<bool> RegisterAccount()
        {
            UserDialogs.Instance.ShowLoading();
            try
            {
                string token = "";
                await Task.Run(() => 
                {
                    token = App.AuthenticationClient.Register(new UserDataRequest
                    {
                        Address = Account.Address,
                        Area = Account.Area,
                        Email = Account.Email,
                        Name = Account.Name,
                        Password = Account.Password
                    });
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

        private bool IsAccountValid()
        {
            if (NullOrEmptyStringChecker.HasNullOrEmptyStrings(Account))
            {
                UserDialogs.Instance.Alert("Please fill in all the fields", "Invalid Data");
                return false;
            }

            if (Account.Password != ConfirmPassword)
            {
                UserDialogs.Instance.Alert("Password Mismatch", "Invalid Data");
                return false;
            }

            Account.Email = Account.Email.ToLower();
            return true;
        }
    }
}