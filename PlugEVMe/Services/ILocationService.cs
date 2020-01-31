using System;
using System.Threading.Tasks;
using PlugEVMe.Models;

namespace PlugEVMe.Services
{
	public interface ILocationService
	{
		Task<GeoCoords> GetGeoCoordinatesAsync();
	}
}
