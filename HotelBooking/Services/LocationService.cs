using System;
using AutoMapper;
using HotelBooking.Data;
using HotelBooking.Data.Constants;
using HotelBooking.Data.ViewModels;
using HotelBooking.Interfaces;
using HotelBooking.Models;

namespace HotelBooking.Services
{
    public class LocationService : ILocationService
    {
        private DbInitializer _dbContext;
        public readonly IMapper _mapper;
        private readonly ILogger<LocationService> _logger;
        public LocationService(DbInitializer dbContext, IMapper mapper, ILogger<LocationService> logger)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _logger = logger;
        }
        
        public LocationService(){}

        public virtual bool CreateLocation(LocationVM location)
        {
            if (location == null) return false;

            try
            {
                var _mappedLocation = _mapper.Map<Location>(location);

                _dbContext.Location.Add(_mappedLocation);
                _dbContext.SaveChanges();

                _logger.LogInformation(SuccessResponse.AddLocation);
                return true;
            }
            catch (Exception exception)
            {
                _logger.LogError(exception.Message);
                return false;
            }
        }

        public virtual List<Location> GetLocations()
        {
            try
            {
                return _dbContext.Location.ToList();
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return null;
            }
        }

        public virtual Location GetLocationByLocationName(string location)
        {
            var _location = _dbContext.Location.SingleOrDefault(loc => loc.location == location);
            return _location;
        }
    }
}

