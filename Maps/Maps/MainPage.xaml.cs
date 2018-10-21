using System;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace Maps
{
    public partial class MainPage : ContentPage
    {
        Position mapPosition;
        Location userLocation;

        public MainPage()
        {
            InitializeComponent();
            FindUserLocation();

            mapPosition = new Position(userLocation.Latitude, userLocation.Longitude);

            MapView.MoveToRegion(
                MapSpan.FromCenterAndRadius(
                    mapPosition, Distance.FromMiles(1)));
        }

        async Task FindUserLocation()
        {
            //TimeSpan pepe = TimeSpan.FromMinutes(2);
            //CancellationTokenSource cts;
            //cts = new CancellationTokenSource();
            //cts.CancelAfter(20000);

            try
            {
                var request = new GeolocationRequest(GeolocationAccuracy.Medium);
                //userLocation = await Geolocation.GetLocationAsync(request);
                userLocation = await Geolocation.GetLastKnownLocationAsync();
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                // Handle not supported on device exception
            }
            catch (PermissionException pEx)
            {
                // Handle permission exception
            }
            catch (Exception ex)
            {
                // Unable to get location
            }
        }
    }
}
