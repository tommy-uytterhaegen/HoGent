using HoGentMaui.Interfaces;
using HoGentMaui.Services;
using HoGentMaui.ViewModels;
using HoGentMauiBL.Interfaces;
using HoGentMauiBL.Services;
using HoGentMauiDL;
using MauiJokesBL.Interfaces;
using MauiJokesDL;
using Microsoft.Extensions.Logging;

namespace HoGentMaui
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            // Als we willen kunnen navigeren moeten we de route geregistreerd hebben met een naam, deze naam wordt dan gebruikt om naar die pagina te gaan.
            // Beschouw dit als een soort Dictionary, waar de key de naam van de route is, en de value het type van de pagina die moet gemaakt worden
            Routing.RegisterRoute(nameof(DetailPage), typeof(DetailPage));

            // Onze ViewModels moeten ook geregistreerd worden bij de services, alleen dan kan het systeem deze aan de pagina meegeven
            builder.Services.AddTransient<DetailJokeViewModel>();
            builder.Services.AddTransient<MainViewModel>();

            // Onze services & repositories
            builder.Services.AddTransient<INavigationService, NavigationService>();

            builder.Services.AddTransient<IJokeService, JokeService>();
            builder.Services.AddSingleton<IJokeRespository, LiteDbJokeRepository>();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
