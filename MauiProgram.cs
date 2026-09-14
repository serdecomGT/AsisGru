using AsisGru.Data;
using AsisGru.Services;
using Microsoft.Extensions.Logging;

namespace AsisGru
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();

            builder
                .UseMauiApp<App>();

            builder.Services.AddMauiBlazorWebView();

            builder.Services.AddSingleton<DatabaseService>();
            builder.Services.AddSingleton<GrupoService>();
            builder.Services.AddSingleton<ParticipanteService>();
            builder.Services.AddSingleton<CategoriaService>();
            builder.Services.AddSingleton<ActividadService>();
            builder.Services.AddSingleton<AsistenciaService>();
            builder.Services.AddSingleton<ReporteService>();
            builder.Services.AddScoped<ThemeService>();

#if DEBUG
            builder.Services.AddBlazorWebViewDeveloperTools();
#endif

            return builder.Build();
        }
    }
}
