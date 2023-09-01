using CommunityToolkit.Mvvm.ComponentModel;
using DemoMauiBookDatabase.Models;

namespace DemoMauiBookDatabase.ViewModels
{
    [QueryProperty(nameof(BookModel), "ViewBookDetails")]
    public partial class BookDetailsPageViewmodel : AddBookBaseViewModel
    {
        [ObservableProperty]
        private Book _bookModel;
    }
}
