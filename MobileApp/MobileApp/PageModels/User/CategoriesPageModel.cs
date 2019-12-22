using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using MobileApp.Models.DataModels;
using MobileApp.PageModels.Admin;
using Xamarin.Forms;

namespace MobileApp.PageModels.User
{
    public class CategoriesPageModel : BasePageModel
    {
        public CategoriesPageModel()
        {
            Title = "Categories";
            Categories = new ObservableCollection<Category>();
        }

        public bool IsAdmin => App.IsAdmin;

        public ObservableCollection<Category> Categories { get; set; }

        public Command OpenAddModalCommand
        {
            get
            {
                return new Command(async () => { await CoreMethods.PushPageModel<AddCategoryPageModel>(null, true); });
            }
        }

        public override async Task Init(object initData)
        {
            try
            {
                string[] categories = null;
                await Task.Run(() => { categories = App.UserBackendClient.GetCategories(); });
                Categories =
                    new ObservableCollection<Category>(categories.Select(u => new Category
                    { Name = u, AddedDate = DateTime.Now.Date }));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            finally
            {
                await base.Init(initData);
            }
        }
    }
}