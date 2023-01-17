using System;
using System.Collections;
using System.Collections.Generic;
using AutoMapper;
using HotelBooking.Data;
using HotelBooking.Data.ViewModels;
using HotelBooking.Models;

namespace HotelBooking.Services
{
	public class HotelService
	{
        private DbInitializer _dbContext;
        private LocationService _locationService;
        public readonly IMapper _mapper;
        public HotelService(DbInitializer dbContext, LocationService locationService, IMapper mapper)
		{
            _dbContext = dbContext;
            _locationService = locationService;
            _mapper = mapper;
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
                return true;
            }
            catch(Exception exception)
            {
                throw;
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
                return hotels;
            }
            catch
            {
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
            return _dbContext.Hotel.ToList();
        }
    }
}

