using System;
using System.Diagnostics;
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



        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await FindUserLocation();

            if (userLocation == null)
                return;

            mapPosition = new Position(userLocation.Latitude, userLocation.Longitude);


            MapView.MoveToRegion(
                MapSpan.FromCenterAndRadius(
                    mapPosition, Distance.FromMiles(0.2)));
        }

        async Task FindUserLocation()
        {
            try
            {
                var request = new GeolocationRequest(GeolocationAccuracy.Best);
                //userLocation = await Geolocation.GetLastKnownLocationAsync();
                //Debug.WriteLine(userLocation?.ToString() ?? "no location");
                userLocation = await Geolocation.GetLocationAsync(request);
                //Debug.WriteLine(userLocation?.ToString() ?? "no location");
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                // Handle not supported on device exception
                Debug.WriteLine(fnsEx);
            }
            catch (PermissionException pEx)
            {
                // Handle permission exception
                Debug.WriteLine(pEx);
            }
            catch (Exception ex)
            {
                // Unable to get location
                Debug.WriteLine(ex);
            }
        }
    }
}