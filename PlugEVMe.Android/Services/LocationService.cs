using Android.Content;
using Android.Locations;
using Android.OS;
using Android.Runtime;
using PlugEVMe.Models;
using PlugEVMe.Services;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PlugEVMe.Droid.Services
{
    public class LocationService : Java.Lang.Object, ILocationService, ILocationListener
	{
		TaskCompletionSource<Location> _tcs;

		public async Task<GeoCoords> GetGeoCoordinatesAsync()
		{
			var manager = (LocationManager)Forms.Context.GetSystemService(Context.LocationService);
            _tcs = new TaskCompletionSource<Location>();

			manager.RequestSingleUpdate("gps", this, null);

			var location = await _tcs.Task;
          
            return new GeoCoords
			{
				Latitude = location.Latitude,
				Longitude = location.Longitude
			};
		}

		public void OnLocationChanged(Location location)
		{
			_tcs.TrySetResult(location);
		}

		public void OnProviderDisabled(string provider)
		{
		}

		public void OnProviderEnabled(string provider)
		{
		}

		public void OnStatusChanged(string provider, [GeneratedEnum] Availability status, Bundle extras)
		{
		}
	}
}