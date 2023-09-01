using DemoMauiBookDatabase.ViewModels;

namespace DemoMauiBookDatabase.Views;

public partial class BooklistHomePage : ContentPage
{
    private readonly BooklistHomePageViewmodel booklistHomePageViewmodel;

    public BooklistHomePage(BooklistHomePageViewmodel booklistHomePageViewmodel)
    {
        InitializeComponent();
        BindingContext = booklistHomePageViewmodel;
        this.booklistHomePageViewmodel = booklistHomePageViewmodel;
    }

    protected override void OnAppearing()
    {
        booklistHomePageViewmodel.LoadBookFromDatabaseCommand.Execute(this);
    }
}