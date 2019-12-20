using AdminService;
using FreshMvvm;
using MobileApp.PageModels;
using MobileApp.PageModels.Admin;
using UserService;
using Xamarin.Forms;

namespace MobileApp
{
    public partial class App : Application
    {
        public static bool IsAdmin { get; set; }
        public static UserServiceClient UserBackendClient { get; set; } = new UserServiceClient();
        public static AdminServiceClient AdminBackendClient { get; set; } = new AdminServiceClient();
        public App()
        {
            InitializeComponent();
            LoadBasicNav();
        }

        public void LoadBasicNav()
        {
            var page = FreshPageModelResolver.ResolvePageModel<AddEditProductPageModel>();
            var basicNavContainer = new FreshNavigationContainer(page);
            MainPage = basicNavContainer;
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}