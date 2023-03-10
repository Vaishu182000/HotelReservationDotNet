using System;
using AutoMapper;
using Azure.Storage.Blobs;
using HotelBooking.Data;
using HotelBooking.Data.Constants;
using HotelBooking.Data.ViewModels;
using HotelBooking.Helpers;
using HotelBooking.Interfaces;
using HotelBooking.Models;

namespace HotelBooking.Services
{
    public class RoomService : IRoomService
    {
        private DbInitializer _dbContext;
        private HotelService _hotelService;
        private readonly IMapper _mapper;
        private readonly string _storageConnectionString;
        private readonly string _storageContainerName;
        private readonly ILogger<RoomService> _logger;
        private StringSplitHelper _stringSplitHelper;
        public RoomService(DbInitializer dbContext,
            HotelService hotelService,
            IMapper mapper,
            IConfiguration configuration,
            ILogger<RoomService> logger,
            StringSplitHelper stringSplitHelper
            )
        {
            _dbContext = dbContext;
            _hotelService = hotelService;
            _mapper = mapper;
            _logger = logger;
            _storageConnectionString = configuration.GetValue<string>("BlobConnectionString");
            _storageContainerName = configuration.GetValue<string>("BlobContainerName");
            _stringSplitHelper = stringSplitHelper;
        }

        public bool createRoom(RoomVM room)
        {
            if (room == null) return false;

            BlobContainerClient container = new BlobContainerClient(_storageConnectionString, _storageContainerName);

            try
            {
                var fileName = room.roomName + Path.GetExtension(room.roomImage.FileName);
                BlobClient client = container.GetBlobClient(fileName);

                using (Stream? data = room.roomImage.OpenReadStream())
                {
                    client.Upload(data);
                }

                if (client.Uri.AbsoluteUri != null)
                {
                    _logger.LogInformation(SuccessResponse.RoomBlob);

                    var _mappedroom = _mapper.Map<Room>(room);
                    _mappedroom.roomImage = client.Uri.AbsoluteUri;
                    _dbContext.Room.Add(_mappedroom);
                    _dbContext.SaveChanges();

                    _logger.LogInformation(SuccessResponse.AddRoom);
                    return true;
                }
                else
                {
                    _logger.LogError(ErrorResponse.ErrorAddRoom);
                    return false;
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return false;
            }
        }

        public IQueryable<Room> GetRoomsByHotelName(string hotelName)
        {
            try
            {
                var _hotel = _hotelService.GetHotelByHotelName(hotelName);

                var _roomList = _dbContext.Room.Where(r => r.HotelId == _hotel.hotelId);
                _logger.LogInformation(SuccessResponse.RoomListByHotel);

                return _roomList;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return null;
            }
        }

        public IQueryable<Room> getLocationInList(IQueryable<Room> room)
        {
            foreach (var _room in room)
            {
                _room.hotel.location = _dbContext.Location.Find(_room.hotel.LocationId);
            }
            return room;
        }

        public Room GetRoomByRoomName(string roomName)
        {
            var _room = _dbContext.Room.FirstOrDefault(r => r.roomName == roomName);
            return _room;
        }

        public List<Room> CheckAvailability(CheckRoomAvailability availability)
        {
            var checkInTime = _stringSplitHelper.splitDate(availability.checkInTime);
            var checkOutTime = _stringSplitHelper.splitDate(availability.checkOutTime);
            var noOfDays = Int16.Parse(checkOutTime[0]) - Int16.Parse(checkInTime[0]);
            var _hotel = _hotelService.GetHotelByHotelName(availability.hotelName);
            var roomIds = new List<int>();

            var _booking = _dbContext.Booking.ToList();
            var roomList = new List<Room>();
            if (_booking == null)
            {
                var room = _dbContext.Room.Where(r => r.HotelId == _hotel.hotelId);
                foreach (var iterate in room)
                {
                    roomList.Add(iterate);
                }
            }
            else
            {
                foreach (var booking in _booking)
                {
                    var count = 0;
                    var bookCheckIn = _stringSplitHelper.splitDate(booking.checkInTime);

                    if (Int16.Parse(checkInTime[2]) - Int16.Parse(bookCheckIn[2]) > 0) count++;
                    else if (Int16.Parse(checkInTime[1]) - Int16.Parse(bookCheckIn[1]) != 0) count++;
                    else
                    {
                        var _diff = Int16.Parse(bookCheckIn[0]) - Int16.Parse(checkInTime[0]);
                        if (_diff < 0) _diff *= -1;

                        if (_diff > noOfDays)
                        {
                            count++;
                        }
                    }

                    if (count > 0)
                    {
                        var room = _dbContext.Room.FirstOrDefault(r => r.roomId == booking.RoomId);
                        if (room.HotelId == _hotel.hotelId && availability.noOfPersons <= room.roomCapacity)
                        {
                            roomIds.Add(room.roomId);
                            roomList.Add(room);
                        }
                    }
                }
            }

            foreach (var rooms in _dbContext.Room.Where(r => r.HotelId == _hotel.hotelId))
            {
                if (!roomIds.Contains(rooms.roomId) && availability.noOfPersons <= rooms.roomCapacity) roomList.Add(rooms);
            }
            return (roomList);
        }
    }
}

