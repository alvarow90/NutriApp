using Microsoft.Extensions.Logging;
using NutriFitApp.Mobile.Services;
using NutriFitApp.Shared.Interfaces;

namespace NutriFitApp.Mobile;

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

        // 🔹 Registra HttpClient y servicios
        builder.Services.AddSingleton(sp => new HttpClient
        {
            BaseAddress = new Uri("https://TU_API_URL/api/") // ⬅️ Cambia por la URL real de tu API
        });

        // 🔹 Servicio de autenticación
        builder.Services.AddSingleton<IAuthService, AuthService>();

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}
