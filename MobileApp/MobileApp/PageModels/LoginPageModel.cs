using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace MobileApp.PageModels
{
    public class LoginPageModel : BasePageModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public Command NavigateToLogin { get; set; }
        public Command NavigateToRegistration { get; set; }
        public Command NavigateToGPlus { get; set; }

    }
}
