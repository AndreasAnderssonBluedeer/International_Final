using System;
using System.Threading.Tasks;
using CatchUp.Core.Models;

namespace CatchUp.Core.Interfaces
{
	public interface IGeoCoder
	{
		Task<string> GetCityFromLocation(GeoLocation location);

	}
}

