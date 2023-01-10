using System;
using HotelBooking.Data;
using HotelBooking.Models;

namespace HotelBooking.Services
{
	public class LocationService
	{
        private DbInitializer _dbContext;
        public LocationService(DbInitializer dbContext)
		{
            _dbContext = dbContext;
        }

        public bool CreateLocation(Location location)
        {
            if (location == null) return false;

            try
            {
                _dbContext.Location.Add(location);
                _dbContext.SaveChanges();
                return true;
            }
            catch (Exception exception)
            {
                return false;
            }
        }

        public List<Location> GetLocations()
        {
            return _dbContext.Location.ToList();
        }

        public Location GetLocationByLocationName(string location)
        {
            var _location = _dbContext.Location.SingleOrDefault(loc => loc.location == location);
            return _location;
        }
	}
}

