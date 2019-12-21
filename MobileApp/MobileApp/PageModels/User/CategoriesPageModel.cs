using System;
using System.Collections.ObjectModel;
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
            Categories = new ObservableCollection<Category>
            {
                new Category {Name = "Men", AddedDate = DateTime.Now.Date},
                new Category {Name = "Women", AddedDate = DateTime.Now.Date},
                new Category {Name = "Kids", AddedDate = DateTime.Now.Date},
                new Category {Name = "Dogs", AddedDate = DateTime.Now.Date},
                new Category {Name = "Cats", AddedDate = DateTime.Now.Date}
            };
        }

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