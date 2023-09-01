using DemoMauiBookDatabase.Views;

namespace DemoMauiBookDatabase
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(AddOrUpdateBookPage), typeof(AddOrUpdateBookPage));
            Routing.RegisterRoute(nameof(BookDetailsPage), typeof(BookDetailsPage));
          
            
        }
    }
}
