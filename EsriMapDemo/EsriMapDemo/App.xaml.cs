using System.Diagnostics;
using Esri.ArcGISRuntime.Mapping;
using Map = Esri.ArcGISRuntime.Mapping.Map;

namespace EsriMapDemo;

public partial class App : Application
{
    private const string Key = "AAPT3NKHt6i2urmWtqOuugvr9XiU-1OjzbvfWu5X7R0QGx4FmuR2MGKPGPx10M7NZ7fTuCxRFNH0ug55urS5V5Ry8SuOxY_xvIhrlHWMiTMFOwc2-f0Tlo_JDYFyZT52ljACwWr39esUeiXE-bNJ9tGA08Gvxj-Ipxvnuooo6ugeEWGMBbBHzm19r8GCAmWhLkGhkFRzRiwR90YLbugg2zg1gzpsTjLduh03XBiJj0KXZZWLc1NkINnJLWdJFIfa4qZs";

    public App()
	{
		InitializeComponent();
        Esri.ArcGISRuntime.ArcGISRuntimeEnvironment.ApiKey = Key;

        MainPage = new AppShell();
	}

    public static async Task<bool> CheckKeyValidity()
    {
        try
        {
            // Check that a key has been set.
            if (string.IsNullOrEmpty(Esri.ArcGISRuntime.ArcGISRuntimeEnvironment.ApiKey)) return false;

            // Check that key is valid for loading a basemap.
            await new Map(BasemapStyle.ArcGISTopographic).LoadAsync();
            return true;
        }
        // An exception will be thrown when a Map using a BasemapStyle is created with an invalid API key.
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return false;
        }
    }

}

