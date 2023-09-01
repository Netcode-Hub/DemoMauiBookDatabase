using CommunityToolkit.Mvvm.ComponentModel;
namespace DemoMauiBookDatabase.ViewModels
{
    public partial class AddBookBaseViewModel : ObservableObject
    {
        [ObservableProperty]
        private string _title;
    }
}
