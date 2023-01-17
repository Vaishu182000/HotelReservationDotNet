using System;
using AutoMapper;
using HotelBooking.Data;
using HotelBooking.Data.ViewModels;
using HotelBooking.Models;

namespace HotelBooking.Services
{
	public class LocationService
	{
        private DbInitializer _dbContext;
        public readonly IMapper _mapper;
        public LocationService(DbInitializer dbContext, IMapper mapper)
		{
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public bool CreateLocation(LocationVM location)
        {
            if (location == null) return false;

            try
            {
                var _mappedLocation = _mapper.Map<Location>(location);
               
                _dbContext.Location.Add(_mappedLocation);
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

