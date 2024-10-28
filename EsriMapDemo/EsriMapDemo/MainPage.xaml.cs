using System.ComponentModel;
using Esri.ArcGISRuntime.Data;
using Esri.ArcGISRuntime.Geometry;
using Esri.ArcGISRuntime.Mapping;
using Esri.ArcGISRuntime.Symbology;
using Esri.ArcGISRuntime.Tasks.NetworkAnalysis;
using Esri.ArcGISRuntime.UI;

namespace EsriMapDemo;

[EsriMapDemo.Attributes.Sample(
     name: "Display device location with autopan modes",
     category: "Location",
     description: "Display your current position on the map, as well as switch between different types of auto pan modes.",
     instructions: "Select an autopan mode, then use the button to start and stop location display.",
     tags: new[] { "GPS", "compass", "location", "map", "mobile", "navigation" })]
public partial class MainPage : ContentPage
{
    public MainPage()
	{
		InitializeComponent();
        Initialize();

    }
    private async void Initialize()
    {
        try
        {

        // Create new Map with basemap
        Esri.ArcGISRuntime.Mapping.Map myMap = new Esri.ArcGISRuntime.Mapping.Map(BasemapStyle.ArcGISStreets);

        // Set the center point with desired latitude and longitude
        double latitude = 21.1893; 
        double longitude = 72.8637;  
        MapPoint centerPoint = new MapPoint(longitude, latitude, SpatialReferences.Wgs84);

        // Set the initial viewpoint
        myMap.InitialViewpoint = new Viewpoint(centerPoint, 10000); // Zoom level of 10,000 meters


        // Assign the map to the MapView
        MyMapView.Map = myMap;

        AddMarker(latitude, longitude);

            // Call method to display the route between two locations
            await ShowRoute(new MapPoint(21.1893, 72.8637, SpatialReferences.Wgs84), // Starting location (example)
                            new MapPoint(21.2049, 72.8411, SpatialReferences.Wgs84)); // Ending location (example)

            //MyMapView.LocationDisplay.AutoPanMode = LocationDisplayAutoPanMode.Navigation;

            // Keep listening to MapView property changed events until the location display has been initialized.
            //MyMapView.PropertyChanged += MyMapView_PropertyChanged;

        }
        catch (Exception ex)
        {

        }
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        var isKeyValid = await App.CheckKeyValidity();
        if (!isKeyValid)
        {
            DisplayAlert("Alert", "Invalid key", "ok");
        }
    }

    private void AddMarker(double latitude, double longitude)
    {
        // Create a simple marker symbol
        var markerSymbol = new SimpleMarkerSymbol(SimpleMarkerSymbolStyle.Circle, System.Drawing.Color.Red, 20);

        // Create a graphic at the specified location
        var location = new MapPoint(longitude, latitude, SpatialReferences.Wgs84);
        var markerGraphic = new Graphic(location, markerSymbol);

        // Create a graphics overlay and add the marker
        var graphicsOverlay = new GraphicsOverlay();
        graphicsOverlay.Graphics.Add(markerGraphic);

        // Add the overlay to the MapView
        MyMapView.GraphicsOverlays.Add(graphicsOverlay);
    }

    private async Task ShowRoute(MapPoint start, MapPoint end)
    {
        // Create a new route task using Esri's routing service
        var routeTask = await RouteTask.CreateAsync(new Uri("https://route.arcgis.com/arcgis/rest/services/World/Route/NAServer/Route_World"));

        // Get the default route parameters
        var routeParams = await routeTask.CreateDefaultParametersAsync();

        // Set the stops for the route
        routeParams.SetStops(new[] { new Stop(start), new Stop(end) });

        // Solve the route
        var routeResult = await routeTask.SolveRouteAsync(routeParams);

        // Get the first route from the result
        var route = routeResult.Routes.FirstOrDefault();
        if (route != null)
        {
            // Create a simple line symbol to display the route
            var routeSymbol = new SimpleLineSymbol(SimpleLineSymbolStyle.Solid, System.Drawing.Color.Blue, 5);

            // Create a graphic for the route and add it to the graphics overlay
            var routeGraphic = new Graphic(route.RouteGeometry, routeSymbol);
            var graphicsOverlay = new GraphicsOverlay();
            graphicsOverlay.Graphics.Add(routeGraphic);

            // Add the graphics overlay to the map view
            MyMapView.GraphicsOverlays.Add(graphicsOverlay);

            // Zoom to the route
            await MyMapView.SetViewpointGeometryAsync(route.RouteGeometry, 50);
        }
    }
    private void MyMapView_PropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        // The map view's location display is initially null, so check for a location display property
        // change before subscribing to auto pan mode change events.
        if (e.PropertyName == nameof(LocationDisplay))
        {
            //// Show in the UI that LocationDisplay.AutoPanMode is off by default.
            //AutoPanModePicker.SelectedItem = "AutoPan Off";

            //// Update the UI when the user pans the view, disabling the auto pan mode.
            //MyMapView.LocationDisplay.AutoPanModeChanged += (sender, args) =>
            //{
            //    if (MyMapView.LocationDisplay.AutoPanMode == LocationDisplayAutoPanMode.Off)
            //    {
            //        AutoPanModePicker.SelectedItem = "AutoPan Off";
            //    }
            //};

            // No longer a need to listen for MapView property changes, just listen for auto pan mode changes.
            //MyMapView.PropertyChanged -= MyMapView_PropertyChanged;
        }
    }
}


