using DemoMauiBookDatabase.ViewModels;

namespace DemoMauiBookDatabase.Views;

public partial class BookDetailsPage : ContentPage
{
	public BookDetailsPage(BookDetailsPageViewmodel bookDetailsPageViewmodel)
	{
		InitializeComponent();
		BindingContext = bookDetailsPageViewmodel;
	}
}