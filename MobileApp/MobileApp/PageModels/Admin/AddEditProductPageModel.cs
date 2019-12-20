using System;
using System.Collections.Generic;
using System.IO;
using Acr.UserDialogs;
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
            Title = "Manage Your Product";
            ImageName = "Click here to choose an image";
            ButtonText = "Add Item";
            PickPhotoCommand = new Command(PickPhotoExcute);
            InsertNewItemCommand = new Command(InsertNewItemExecute);
            NewItem = new ShoppingItem();
            SelectedCategory = new Category();
            Visibility = false;
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
        public string ImageName { get; set; }
        public string ButtonText { get; set; }
        public bool Visibility { get; set; }
        public List<Category> Categories { get; set; }
        public Category SelectedCategory { get; set; }

        private void InsertNewItemExecute()
        {
            if (!IsItemValid())
                return;
            if (InsertNewItem())
            {

            }
            //
        }

        private bool InsertNewItem()
        {
            throw new NotImplementedException();
        }

        private bool IsItemValid()
        {
            if (NullOrEmptyStringChecker.HasNullOrEmptyStrings(NewItem))
            {
                UserDialogs.Instance.Alert("Please check your input", "Invalid Data");
                return false;
            }
            else if (NewItem.Price <= 0)
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
                        ImageName = selectedImageFile.Path;
                        NewItem.Logo = Convert.ToBase64String(data);
                        //var st = ImageName.Split('/');
                        //ImageName = st[st.Length - 1];
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