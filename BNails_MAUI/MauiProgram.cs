using BNails_MAUI.Interfaces.Repositories;
using BNails_MAUI.Interfaces.Services;
using BNails_MAUI.Repositories;
using BNails_MAUI.Services;
using FFImageLoading.Maui;
using Microsoft.Extensions.Logging;

namespace BNails_MAUI
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseFFImageLoading()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            builder.Services.AddSingleton<IDialogService,DialogService>();
            builder.Services.AddSingleton<IUsuarioRepository,UsuarioRepository>();
            builder.Services.AddSingleton<IConfiguracionRepository,ConfiguracionRepository>();
            builder.Services.AddSingleton<UsuarioService>();
            builder.Services.AddSingleton<IEmailService,EmailService>();

            builder.Services.AddSingleton<App>();

#if DEBUG
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
