using System;
using System.Collections.ObjectModel;
using System.Reactive.Linq;
using MobileApp.Models.DataModels;
using MobileApp.Pages.User;

namespace MobileApp.PageModels.User
{
    public class CategoriesPageModel : BasePageModel
    {
        public ObservableCollection<Category> Categories { get; set; }

        public CategoriesPageModel()
        {
            Title = "Categories";
            Categories = new ObservableCollection<Category>()
            {
                new Category() {Name = "Men", AddedDate = DateTime.Now.Date},
                new Category() {Name = "Women", AddedDate = DateTime.Now.Date},
                new Category() {Name = "Kids", AddedDate = DateTime.Now.Date},
                new Category() {Name = "Dogs", AddedDate = DateTime.Now.Date},
                new Category() {Name = "Cats", AddedDate = DateTime.Now.Date},
            };
        }
    }
}