using System;
using System.Collections;
using System.Collections.Generic;
using AutoMapper;
using HotelBooking.Data;
using HotelBooking.Data.Constants;
using HotelBooking.Data.ViewModels;
using HotelBooking.Models;

namespace HotelBooking.Services
{
    public class HotelService
    {
        private DbInitializer _dbContext;
        private LocationService _locationService;
        public readonly IMapper _mapper;
        private readonly ILogger<HotelService> _logger;
        public HotelService(DbInitializer dbContext, LocationService locationService, IMapper mapper, ILogger<HotelService> logger)
        {
            _dbContext = dbContext;
            _locationService = locationService;
            _mapper = mapper;
            _logger = logger;
        }

        public bool AddHotel(HotelVM hotel)
        {
            if (hotel == null) return false;

            try
            {

                var _mappedHotel = _mapper.Map<Hotel>(hotel);
                Console.WriteLine(_mappedHotel);

                _dbContext.Hotel.Add(_mappedHotel);
                _dbContext.SaveChanges();

                _logger.LogInformation(SuccessResponse.AddHotel);
                return true;
            }
            catch (Exception exception)
            {
                _logger.LogError(exception.Message);
                return false;
            }
        }

        public List<string> HotelListByLocation(string location)
        {
            var _location = _locationService.GetLocationByLocationName(location);

            try
            {
                var hotels = new List<string>();
                var _hotelListByLocation = _dbContext.Hotel.Where(l => l.LocationId == _location.locationId);
                hotels = _hotelListByLocation.Select(l => l.hotelName).ToList();

                _logger.LogInformation(SuccessResponse.HotelListBasedOnLocation);
                return hotels;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return null;
            }
        }

        public Hotel GetHotelByHotelName(string hotel)
        {
            var _hotel = _dbContext.Hotel.SingleOrDefault(h => h.hotelName == hotel);
            return _hotel;
        }

        public List<Hotel> GetAllHotels()
        {
            try
            {
                var _hotels = _dbContext.Hotel.ToList();
                foreach (var hotel in _hotels)
                {
                    hotel.location = _dbContext.Location.Find(hotel.LocationId);
                }
                return _hotels;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return null;
            }
        }
    }
}

