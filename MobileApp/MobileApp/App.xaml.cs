using AdminService;
using AuthenticationService;
using FreshMvvm;
using MobileApp.PageModels.Shared;
using System.ServiceModel;
using UserService;
using Xamarin.Forms;

namespace MobileApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            LoadBasicNav();
            IsAdmin = true;
        }

        public static bool IsAdmin { get; set; }
        public static string Token { get; set; }
        public static UserServiceClient UserBackendClient { get; set; } = new UserServiceClient();
        public static AdminServiceClient AdminBackendClient { get; set; } = new AdminServiceClient();
        public static AuthenticationServiceClient AuthenticationClient { get; set; } =
            new AuthenticationServiceClient(AuthenticationServiceClient.EndpointConfiguration.BasicHttpBinding_IAuthenticationService,
                                            new EndpointAddress("http://10.0.2.2:8082/svc"));

        public void LoadBasicNav()
        {
            var page = FreshPageModelResolver.ResolvePageModel<LoginPageModel>();
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