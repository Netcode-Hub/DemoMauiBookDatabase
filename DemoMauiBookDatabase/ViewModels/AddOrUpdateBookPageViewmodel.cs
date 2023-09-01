using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DemoMauiBookDatabase.CustomControls;
using DemoMauiBookDatabase.DataServices;
using DemoMauiBookDatabase.Models;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;

namespace DemoMauiBookDatabase.ViewModels
{
    [QueryProperty(nameof(AddBookModel), "UpdateBookData")]
    public partial class AddOrUpdateBookPageViewmodel : AddBookBaseViewModel
    {
        public ObservableCollection<Error> Errors { get; set; } = new();

        [ObservableProperty]
        private Book _addBookModel;

        [ObservableProperty]
        private bool _showErrors;

        [ObservableProperty]
        ImageSource _imageSourceFile;

        private readonly IBookService bookService;
        public AddOrUpdateBookPageViewmodel(IBookService bookService)
        {
            this.bookService = bookService;
            Title = "Add Book Data";
            AddBookModel = new Book();
        }

        [RelayCommand]
        private async Task SelectImage()
        {
            var image = await FilePicker.PickAsync(new PickOptions
            {
                PickerTitle = "Select Book Image",
                FileTypes = FilePickerFileType.Images
            });
            if (image == null) return;

            byte[] imageByte;
            var newFile = Path.Combine(FileSystem.CacheDirectory, image.FileName);
            var stream = await image.OpenReadAsync();
            using (MemoryStream memory = new())
            {
                stream.CopyTo(memory);
                imageByte = memory.ToArray();
            }
            //converting to base64string
            var convertedImage = Convert.ToBase64String(imageByte);
            AddBookModel.Image = convertedImage;

            // convert from base to image
            GetImage(convertedImage);
            

        }

        private void GetImage(string base64)
        {
            //converting from base64string to image
            var imgFromBase64 = Convert.FromBase64String(base64);
            MemoryStream memoryStream = new(imgFromBase64);
            ImageSourceFile = ImageSource.FromStream(() => memoryStream);
            // return ImageSourceFile;
        }

        [RelayCommand]
        private async Task SaveData()
        {
            Errors.Clear();
            if (!ValidateModel(AddBookModel)) return;

            if (Errors.Any() || Errors.Count > 0)
            {
                ShowErrors = true; return;
            }

            var result = await bookService.AddOrUpdateBookAsync(AddBookModel);
            if (result.Flag)
            {
                MakeToast(result.Message);
                AddBookModel = new Book();
                ImageSourceFile = null;
                return;
            }
            Errors.Clear();
            Errors.Add(new Error() { Property = "Alert: ", Value = result.Message });
            ShowErrors = true;
            return;
        }

        // Validate book model
        private bool ValidateModel(Book validateBook)
        {
            if (validateBook.Title is null)
                Errors.Add(new Error() { Property = "Title: ", Value = " Book Title cannot be empty" });

            if (validateBook.Description is null)
            {
                Errors.Add(new Error() { Property = "Description: ", Value = " Book Description cannot be empty" });
            }
            else
            {
                if (validateBook.Description.Length < 20)
                    Errors.Add(new Error() { Property = "Description: ", Value = " Minimun length of text must be 20" });
            }

            if (validateBook.Image is null)
                Errors.Add(new Error() { Property = "Image: ", Value = " Book Image cannot be empty" });

            return true;
        }

        private static async void MakeToast(string message)
        {
            CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
            ToastDuration duration = ToastDuration.Long;
            double fontSize = 15;
            var toast = Toast.Make(message, duration, fontSize);
            await toast.Show(cancellationTokenSource.Token);
        }

        [RelayCommand]
        private async Task NavigateToHome() => await Shell.Current.GoToAsync("..", true);
    }
}
