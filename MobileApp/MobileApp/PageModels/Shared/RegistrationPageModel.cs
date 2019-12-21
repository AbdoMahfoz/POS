﻿using System;
using System.Threading.Tasks;
using Acr.UserDialogs;
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

        private Task<bool> RegisterAccount()
        {
            UserDialogs.Instance.ShowLoading();
            try
            {
                return App.UserBackendClient.RegisterAsync(new UserDataRequest
                {
                    Address = Account.Address,
                    Area = Account.Area,
                    Email = Account.Email,
                    Name = Account.Name,
                    Password = Account.Password
                });
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