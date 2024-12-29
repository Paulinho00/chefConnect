using ChefConnectMobileApp.DI;
using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using Plugin.ContextMenuContainer;

namespace ChefConnectMobileApp;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.UseMauiCommunityToolkit()
            .ConfigureContextMenuContainer()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			})
			.Services
			.RegisterServices();

        ServiceHelper.Initialize(builder.Services.BuildServiceProvider());

#if DEBUG
		builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}
