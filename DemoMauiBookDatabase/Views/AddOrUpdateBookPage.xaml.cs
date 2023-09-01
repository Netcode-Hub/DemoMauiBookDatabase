using DemoMauiBookDatabase.ViewModels;

namespace DemoMauiBookDatabase.Views;

public partial class AddOrUpdateBookPage : ContentPage
{
    public AddOrUpdateBookPage(AddOrUpdateBookPageViewmodel addOrUpdateBookPageViewmodel)
    {
        InitializeComponent();
        BindingContext = addOrUpdateBookPageViewmodel;
    }
}