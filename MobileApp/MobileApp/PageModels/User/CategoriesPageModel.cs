using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using MobileApp.Models.DataModels;
using MobileApp.PageModels.Admin;
using Xamarin.Forms;
using System.Linq;

namespace MobileApp.PageModels.User
{
    public class CategoriesPageModel : BasePageModel
    {
        public CategoriesPageModel()
        {
            Title = "Categories";
            Categories = new ObservableCollection<Category>
            {
                new Category {Name = "Men", AddedDate = DateTime.Now.Date},
                new Category {Name = "Women", AddedDate = DateTime.Now.Date},
                new Category {Name = "Kids", AddedDate = DateTime.Now.Date},
                new Category {Name = "Dogs", AddedDate = DateTime.Now.Date},
                new Category {Name = "Cats", AddedDate = DateTime.Now.Date}
            };
        }

        public override async Task Init(object initData)
        {
            string[] categories = null;
            await Task.Run(() =>
            {
                categories = App.UserBackendClient.GetCategories();
            });
            Categories = new ObservableCollection<Category>(categories.Select(u => new Category { Name = u, AddedDate = DateTime.Now.Date }));
            await base.Init(initData);
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
    }
}