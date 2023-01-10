using System;
using System.Collections;
using System.Collections.Generic;
using HotelBooking.Data;
using HotelBooking.Data.ViewModels;
using HotelBooking.Models;

namespace HotelBooking.Services
{
	public class HotelService
	{
        private DbInitializer _dbContext;
        private LocationService _locationService;
        public HotelService(DbInitializer dbContext, LocationService locationService)
		{
            _dbContext = dbContext;
            _locationService = locationService;
        }

        public bool AddHotel(HotelVM hotel)
        {
            if (hotel == null) return false;

            try
            {
                var _location = _locationService.GetLocationByLocationName(hotel.location);
                
                var _hotel = new Hotel()
                {
                    hotelName = hotel.hotelName,
                    noOfRooms = hotel.noOfRooms,
                    LocationId = _location.locationId
                };

                _dbContext.Hotel.Add(_hotel);
                _dbContext.SaveChanges();
                return true;
            }
            catch(Exception exception)
            {
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

