using System;
using System.IO;
using Acr.UserDialogs;
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
        public ShoppingItem NewItem { get; set; }
        public Command PickPhotoCommand { get; set; }
        public Command InsertNewItemCommand { get; set; }
        public ImageSource SelectedImage { get; set; }
        public string ImageName { get; set; }
        public string ButtonText { get; set; }
        public bool Visibility { get; set; }

        public AddEditProductPageModel()
        {
            Title = "Manage Your Product";
            ImageName = "Click here to choose an image";
            ButtonText = "Add Item";
            PickPhotoCommand = new Command(PickPhotoExcute);
            InsertNewItemCommand = new Command(InsertNewItemExecute);
            NewItem = new ShoppingItem();
            Visibility = false;
        }

        private void InsertNewItemExecute()
        {
            throw new NotImplementedException();
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
                        ImageName = selectedImageFile.Path;
                        var image = await CrossImageEdit.Current.CreateImageAsync(selectedImageFile.GetStream());
                        var width = 700;
                        var height = 700;
                        var data = image.Resize(width, height).ToPng();
                        SelectedImage = ImageSource.FromStream(() => new MemoryStream(data));
                        NewItem.Logo = Convert.ToBase64String(data);
                        var st = ImageName.Split('/');
                        ImageName = st[st.Length - 1];
                        Visibility = true;
                        RaisePropertyChanged(nameof(ImageName));
                        RaisePropertyChanged(nameof(SelectedImage));
                        RaisePropertyChanged(nameof(Visibility));
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