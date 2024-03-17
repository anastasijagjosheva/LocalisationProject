using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace LocalizationProject.Services.Location
{
    public class LocationService : ILocationService
    {
        public async Task<Xamarin.Essentials.Location> GetCurrentLocationCoordinates()
        {
            GeolocationRequest request = new GeolocationRequest(GeolocationAccuracy.Medium, TimeSpan.FromSeconds(10));
            try
            {
                var location = await Geolocation.GetLocationAsync(request);
                return location;
            }
            catch (Exception ex)
            {
                // Unable to get location
                Debug.WriteLine(ex);
                return null;
            }
        }
        
        
        /*public async Task<Xamarin.Essentials.Location> GetCurrentLocationCoordinates()
        {
            GeolocationRequest request = new GeolocationRequest(GeolocationAccuracy.Medium, TimeSpan.FromSeconds(10));
            TaskCompletionSource<Location> locationTaskCompletionSource = new TaskCompletionSource<Location>();

            Device.BeginInvokeOnMainThread(async () =>
            {
                try
                {
                    locationTaskCompletionSource.SetResult(await Geolocation.GetLocationAsync(request));
                }
                catch(Exception exception)
                {
                    locationTaskCompletionSource.SetException(exception);
                    locationTaskCompletionSource.SetResult(null);
                }
            });

            return locationTaskCompletionSource.Task;
        }*/

    }
}