﻿using System;
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
            RefreshCommand = new Command(async () => await RefreshExecute());
        }
        public Category SelectedCategory { get; set; }

        public bool IsRefreshing { get; set; }

        public bool IsAdmin => App.IsAdmin;

        public Command RefreshCommand { get; set; }
        public ObservableCollection<Category> Categories { get; set; }

        public Command OpenAddModalCommand
        {
            get
            {
                return new Command(async () => { await CoreMethods.PushPageModel<AddCategoryPageModel>(null, true); });
            }
        }

        public Command OpenCartCommand
        {
            get
            {
                return new Command(async () => { await CoreMethods.PushPageModel<CartPageModel>(null, true); });
            }
        }

        public Command ItemSelectedCommand
        {
            get
            {
                return new Command(async () =>
                {
                    await CoreMethods.PushPageModel<ProductsPageModel>(SelectedCategory);
                });
            }
        }

        private async Task RefreshExecute()
        {
            IsRefreshing = true;
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
                IsRefreshing = false;
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