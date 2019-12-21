using System;
using System.Collections.Generic;
using System.IO;
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
            Categories = new List<Category>
            {
                new Category {Id = 1, Name = "Food"},
                new Category {Id = 2, Name = "Water"},
                new Category {Id = 3, Name = "Air"}
            };
        }

        public ShoppingItem NewItem { get; set; }
        public Command PickPhotoCommand { get; set; }
        public Command InsertNewItemCommand { get; set; }
        public ImageSource SelectedImage { get; set; }
        public string ButtonText { get; set; }
        public bool Visibility { get; set; }
        public bool IsEdit { get; set; }
        public List<Category> Categories { get; set; }
        public Category SelectedCategory { get; set; }

        private async Task CommandExecute()
        {
            if (!IsItemValid())
                return;
            if (IsEdit)
            {
                if (await UpdateItem())
                {
                    //navigate
                }
            }
            else
            {
                if (await InsertNewItem())
                {
                    //navigate 
                }
            }
        }

        private async Task<bool> InsertNewItem()
        {
            UserDialogs.Instance.ShowLoading();
            try
            {
                await App.AdminBackendClient.InsertItemAsync(new ItemRequest
                {
                    Name = NewItem.Name,
                    Base64Image = NewItem.Logo,
                    Description = NewItem.Description,
                    Price = NewItem.Description,
                    Categories = new[] {NewItem.ItemCategory.Name}
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
                await App.AdminBackendClient.UpdateItemAsync(new ItemResult
                {
                    Id = NewItem.Id,
                    Name = NewItem.Name,
                    Base64Image = NewItem.Logo,
                    Description = NewItem.Description,
                    Price = NewItem.Description,
                    Categories = new[] {NewItem.ItemCategory.Name}
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

        public override void Init(object initData)
        {
            base.Init(initData);

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
                var item = (ShoppingItem) initData;
                NewItem = item;
                SelectedCategory = item.ItemCategory;
                Visibility = true;
            }
        }

        protected override void ViewIsAppearing(object sender, EventArgs e)
        {
            if (!IsEdit) return;
            UserDialogs.Instance.ShowLoading();
            base.ViewIsAppearing(sender, e);
            var bytes = Convert.FromBase64String(NewItem.Logo);
            SelectedImage = ImageSource.FromStream(() => new MemoryStream(bytes));
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
                PhotoSize = PhotoSize.Medium
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
                        var width = 700;
                        var height = 700;
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