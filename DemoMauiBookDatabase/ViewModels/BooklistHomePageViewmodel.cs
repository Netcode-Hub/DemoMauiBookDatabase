using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DemoMauiBookDatabase.DataServices;
using DemoMauiBookDatabase.Models;
using DemoMauiBookDatabase.Views;
using MvvmHelpers;

namespace DemoMauiBookDatabase.ViewModels
{
    public partial class BooklistHomePageViewmodel : AddBookBaseViewModel
    {
        [ObservableProperty]
        private bool _gridVisibility;

        private readonly IBookService bookService;
        public ObservableRangeCollection<Book> Books { get; set; } = new();
        public BooklistHomePageViewmodel(IBookService bookService)
        {
            this.bookService = bookService;
            Title = "Netcode Book List";
        }


        [RelayCommand]
        private async Task LoadBookFromDatabase()
        {
            GridVisibility = false;
            await Task.Delay(1000);
            var results = await bookService.GetBooksAsync();
            if (results.Count > 0)
            {
                Books.Clear();

                foreach (var book in results)
                {
                    string subString = book.Description.Length > 30 ? book.Description.Substring(0, 30) + "..." : book.Description;
                    Books.Add(new Book() { Id = book.Id, Title = book.Title, Description = subString, Image = book.Image });
                }
            }
            GridVisibility = true;
        }

        [RelayCommand]
        private async Task NavigateToAddBookPage() => await Shell.Current.GoToAsync(nameof(AddOrUpdateBookPage), true);

        [RelayCommand]
        private async Task NavigateToDetails(Book bookModel)
        {

            if (bookModel is null) return;

            //get full description
            var desc = await bookService.GetBookAsync(bookModel.Id);
            var navigationParameter = new Dictionary<string, object>();
            navigationParameter.Add("ViewBookDetails", desc);
            await Shell.Current.GoToAsync(nameof(BookDetailsPage), navigationParameter);
        }

        [RelayCommand]
        private async Task DeleteBookData(Book bookToBeDeleted)
        {
            bool answer = await Shell.Current.DisplayAlert("Confirm Delete?", $"Are you sure you wanna delete {bookToBeDeleted.Title}?", "Yes", "No");
            if (answer)
            {
                var result = await bookService.DeleteBookAsync(bookToBeDeleted);
                MakeToast(result.Message);
                await LoadBookFromDatabase();
            }
        }

        [RelayCommand]
        private async Task UpdateBookData(Book bookToBeUpdated)
        {
            bool answer = await Shell.Current.DisplayAlert("Confirm Update?", $"Are you sure you wanna update: {bookToBeUpdated.Title} ?", "Yes", "No");
            if (answer)
            {
                //get full description
                var desc = await bookService.GetBookAsync(bookToBeUpdated.Id);
                var navigationParameter = new Dictionary<string, object>();
                navigationParameter.Add("UpdateBookData", desc);
                await Shell.Current.GoToAsync(nameof(AddOrUpdateBookPage), navigationParameter);
            }
        }

        private static async void MakeToast(string message)
        {
            CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
            ToastDuration duration = ToastDuration.Long;
            double fontSize = 15;
            var toast = Toast.Make(message, duration, fontSize);
            await toast.Show(cancellationTokenSource.Token);
        }
    }
}

