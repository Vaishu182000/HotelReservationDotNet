using System;
using HotelBooking.Data;
using HotelBooking.Data.ViewModels;
using HotelBooking.Models;

namespace HotelBooking.Services
{
	public class RoomService
	{
        private DbInitializer _dbContext;
        private HotelService _hotelService;
        public RoomService(DbInitializer dbContext, HotelService hotelService)
		{
            _dbContext = dbContext;
            _hotelService = hotelService;
        }

        public bool createRoom(RoomVM room)
        {
            if (room == null) return false;

            try
            {
                var _hotel = _hotelService.GetHotelByHotelName(room.hotelName);

                var _room = new Room()
                {
                    roomName = room.roomName,
                    roomCapacity = room.roomCapacity,
                    roomRate = room.roomRate,
                    HotelId = _hotel.hotelId
                };
                
                _dbContext.Room.Add(_room);
                _dbContext.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public IQueryable<Room> GetRoomsByHotelName(string hotelName)
        {
            try
            {
                var _hotel = _hotelService.GetHotelByHotelName(hotelName);

                var _roomList = _dbContext.Room.Where(r => r.HotelId == _hotel.hotelId);
                return _roomList;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public Room GetRoomByRoomName(string roomName)
        {
            var _room = _dbContext.Room.FirstOrDefault(r => r.roomName == roomName);
            return _room;
        }
	}
}

