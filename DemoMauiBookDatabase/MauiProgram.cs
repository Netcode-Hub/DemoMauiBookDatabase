using CommunityToolkit.Maui;
using DemoMauiBookDatabase.DataServices;
using DemoMauiBookDatabase.ViewModels;
using DemoMauiBookDatabase.Views;
using Microsoft.Extensions.Logging;
using Syncfusion.Maui.Core.Hosting;

namespace DemoMauiBookDatabase
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .ConfigureSyncfusionCore()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });
            builder.Services.AddTransient<IBookService, BookService>();

            builder.Services.AddTransient<AddOrUpdateBookPageViewmodel>();
            builder.Services.AddTransient<AddOrUpdateBookPage>();

            builder.Services.AddTransient<BooklistHomePageViewmodel>();
            builder.Services.AddTransient<BooklistHomePage>();

            builder.Services.AddTransient<BookDetailsPageViewmodel>();
            builder.Services.AddTransient<BookDetailsPage>();
            
#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
