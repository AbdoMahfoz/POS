using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Acr.UserDialogs;
using AdminService;
using MobileApp.Helpers;
using MobileApp.Models.DataModels;
using Plugin.ImageEdit;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace MobileApp.PageModels.Admin
{
    public class AddEditProductPageModel : BasePageModel
    {
        public AddEditProductPageModel()
        {
            PickPhotoCommand = new Command(PickPhotoExcute);
            InsertNewItemCommand = new Command(async () => await CommandExecute());
            Categories = new ObservableCollection<Category>();
        }

        public ShoppingItem NewItem { get; set; }
        public Command PickPhotoCommand { get; set; }
        public Command InsertNewItemCommand { get; set; }

        public Command CloseCommand
        {
            get { return new Command(async () => { await CoreMethods.PopPageModel(null, true); }); }
        }

        public ImageSource SelectedImage { get; set; }
        public string ButtonText { get; set; }
        public bool Visibility { get; set; }
        public bool IsEdit { get; set; }
        public ObservableCollection<Category> Categories { get; set; }
        public Category SelectedCategory { get; set; }

        private async Task CommandExecute()
        {
            if (!IsItemValid())
                return;
            if (IsEdit)
            {
                if (await UpdateItem())
                {
                    UserDialogs.Instance.Alert("Updated Successfully :D", "success");
                    await CoreMethods.PopPageModel(null, true);
                }
            }
            else
            {
                if (await InsertNewItem())
                {
                    UserDialogs.Instance.Alert("Inserted Successfully :D", "success");
                    await CoreMethods.PopPageModel(null, true);
                }
            }
        }

        private async Task<bool> InsertNewItem()
        {
            UserDialogs.Instance.ShowLoading();
            try
            {
                await Task.Run(() =>
                {
                    App.AdminBackendClient.InsertItem(App.Token, new ItemRequest
                    {
                        Name = NewItem.Name,
                        Base64Image = NewItem.Logo,
                        Description = NewItem.Description,
                        Price = NewItem.Price.ToString(CultureInfo.InvariantCulture),
                        Categories = new[] { NewItem.ItemCategory.Name }
                    });
                });
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                //UserDialogs.Instance.HideLoading();
                UserDialogs.Instance.Alert(e.Message, "Error");
                return false;
            }
            finally
            {
                UserDialogs.Instance.HideLoading();
            }
        }

        private async Task<bool> UpdateItem()
        {
            UserDialogs.Instance.ShowLoading();
            try
            {
                await Task.Run(() =>
                {
                    App.AdminBackendClient.UpdateItem(App.Token, new ItemResult
                    {
                        Id = NewItem.Id,
                        Name = NewItem.Name,
                        Base64Image = NewItem.Logo,
                        Description = NewItem.Description,
                        Price = NewItem.Description,
                        Categories = new[] { NewItem.ItemCategory.Name }
                    });
                });
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                //UserDialogs.Instance.HideLoading();
                UserDialogs.Instance.Alert(e.Message, "Error");
                return false;
            }
            finally
            {
                UserDialogs.Instance.HideLoading();
            }
        }

        public override async Task Init(object initData)
        {
            if (initData == null)
            {
                IsEdit = false;
                Title = "Add a Product";
                ButtonText = "Add Item";
                NewItem = new ShoppingItem();
                SelectedCategory = new Category();
                Visibility = false;
            }
            else
            {
                IsEdit = true;
                Title = "Edit a Product";
                ButtonText = "Edit Item";
                var item = (ShoppingItem)initData;
                NewItem = item;
                SelectedCategory = item.ItemCategory;
                Visibility = true;
            }

            await PopulateCategories();
            await base.Init(initData);
        }

        async Task PopulateCategories()
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

        }

        protected override void ViewIsAppearing(object sender, EventArgs e)
        {
            if (!IsEdit) return;
            UserDialogs.Instance.ShowLoading();
            base.ViewIsAppearing(sender, e);
            SelectedImage = ImageSource.FromStream(() => new MemoryStream(Convert.FromBase64String(NewItem.Logo)));
            UserDialogs.Instance.HideLoading();
        }

        private bool IsItemValid()
        {
            if (NullOrEmptyStringChecker.HasNullOrEmptyStrings(NewItem))
            {
                UserDialogs.Instance.Alert("Please check your input", "Invalid Data");
                return false;
            }

            if (NewItem.Price <= 0)
            {
                UserDialogs.Instance.Alert("Please check your price", "Invalid Data");
                return false;
            }

            if (SelectedCategory == null)
            {
                UserDialogs.Instance.Alert("Please select a Category", "Invalid Data");
                return false;
            }

            NewItem.ItemCategory = SelectedCategory;
            return true;
        }

        private void PickPhotoExcute()
        {
            if (!CrossMedia.Current.IsPickPhotoSupported)
            {
                UserDialogs.Instance.Alert("Please Grant The App Access to Your Photos", "Permission Not Granted ",
                    "OK");
                return;
            }

            UserDialogs.Instance.ShowLoading();
            var mediaOption = new PickMediaOptions
            {
                PhotoSize = PhotoSize.Small,
                CompressionQuality = 50
            };

            MediaFile selectedImageFile = null;

            MainThread.BeginInvokeOnMainThread(async () =>
            {
                try
                {
                    selectedImageFile = await CrossMedia.Current.PickPhotoAsync(mediaOption);
                    if (selectedImageFile != null)
                    {
                        var image = await CrossImageEdit.Current.CreateImageAsync(selectedImageFile.GetStream());
                        var width = 600;
                        var height = 600;
                        var data = image.Resize(width, height).ToPng();
                        SelectedImage = ImageSource.FromStream(() => new MemoryStream(data));
                        NewItem.Logo = Convert.ToBase64String(data);
                        Visibility = true;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    UserDialogs.Instance.HideLoading();
                }
            });
        }
    }
}