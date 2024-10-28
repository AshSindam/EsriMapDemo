using Esri.ArcGISRuntime.Maui;
using Esri.ArcGISRuntime.Toolkit.Maui;
using Microsoft.Extensions.Logging;

namespace EsriMapDemo;

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
			})
			.UseArcGISRuntime().UseArcGISToolkit();

#if DEBUG
		builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}

