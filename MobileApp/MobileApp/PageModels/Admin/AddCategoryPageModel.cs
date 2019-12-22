using System;
using System.Threading.Tasks;
using Acr.UserDialogs;
using Xamarin.Forms;

namespace MobileApp.PageModels.Admin
{
    public class AddCategoryPageModel : BasePageModel
    {
        public AddCategoryPageModel()
        {
            Title = "Add Category";
            InsertCategoryCommand = new Command(async () => await InsertCategoryExecute());
        }

        public string CategoryName { get; set; }
        public Command InsertCategoryCommand { get; set; }

        public Command CloseCommand
        {
            get { return new Command(async () => { await CoreMethods.PopPageModel(null, true); }); }
        }

        private async Task InsertCategoryExecute()
        {
            if (string.IsNullOrWhiteSpace(CategoryName))
            {
                UserDialogs.Instance.Alert("Please Enter a Category Name", "Error");
                return;
            }
            if (await InsertCategory())
            {
                await CoreMethods.PopPageModel(null, true);
            }
        }

        private async Task<bool> InsertCategory()
        {
            UserDialogs.Instance.ShowLoading();
            try
            {
                await Task.Run(() => App.AdminBackendClient.AddCategory(App.Token, CategoryName));
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                UserDialogs.Instance.Alert(e.Message, "Error");
                return false;
            }
            finally
            {
                UserDialogs.Instance.HideLoading();
            }
        }
    }
}