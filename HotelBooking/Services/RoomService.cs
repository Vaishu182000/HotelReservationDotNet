using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Amazon.S3;
using Amazon.S3.Model;
using AutoMapper;
using Azure.Storage.Blobs;
using HotelBooking.Data;
using HotelBooking.Data.Constants;
using HotelBooking.Data.ViewModels;
using HotelBooking.Helpers;
using HotelBooking.Interfaces;
using HotelBooking.Models;
using HotelBooking.S3;
using HotelBooking.SecretManager.Interface;
using Microsoft.Extensions.Logging;

namespace HotelBooking.Services
{
    public class RoomService : IRoomService
    {
        private DbInitializer _dbContext;
        private IHotelService _hotelService;
        private readonly IMapper _mapper;
        private readonly string _storageConnectionString;
        private readonly string _storageContainerName;
        private readonly ILogger<RoomService> _logger;
        private StringSplitHelper _stringSplitHelper;
        private IUploadToS3 _s3;
        private readonly IConfigSettings _configSettings;
        public RoomService(DbInitializer dbContext,
            IHotelService hotelService,
            IMapper mapper,
            ILogger<RoomService> logger,
            StringSplitHelper stringSplitHelper,
            IUploadToS3 s3, IConfigSettings configSettings
            )
        {
            _dbContext = dbContext;
            _hotelService = hotelService;
            _mapper = mapper;
            _logger = logger;
            _stringSplitHelper = stringSplitHelper;
            _s3 = s3;
            _configSettings = configSettings;
        }

        public async Task<bool> createRoom(RoomVM room)
        {
            if (room == null) return false;

            try
            {
                var fileName = room.roomName + Path.GetExtension(room.roomImage.FileName);

                _logger.LogInformation(SuccessResponse.RoomBlob);

                var _mappedroom = _mapper.Map<Room>(room);

                var image = await _s3.Upload(room.roomImage, fileName, _configSettings.AwsAccessKey, _configSettings.AwsSecretKey, 
                    _configSettings.AwsSessionToken);
                
                _mappedroom.roomImage = image;
                _dbContext.Room.Add(_mappedroom);
                _dbContext.SaveChanges();
                
                _logger.LogInformation(SuccessResponse.AddRoom);
                return true;
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

        public IQueryable<Room> CheckAvailability(CheckRoomAvailability availability)
        {
            var _hotel = _hotelService.GetHotelByHotelName(availability.hotelName);
            
            var notAvailableRoomsList = from r in _dbContext.Room
                where (r.HotelId == _hotel.hotelId)
                from b in _dbContext.Booking
                where (r.roomId == b.RoomId) &&
                      (availability.checkInTime >= b.checkInTime && availability.checkInTime <= b.checkOutTime)
                select r;
            
            if (notAvailableRoomsList.Count() != 0)
            {
                var roomLeft = from rf in _dbContext.Room
                    from r in notAvailableRoomsList
                    where 0 != r.roomId.CompareTo(rf.roomId)
                    select rf;
                return roomLeft;
            }
            else
            {
                var roomLeft = from rf in _dbContext.Room
                    where rf.HotelId == _hotel.hotelId
                    select rf;
                return roomLeft;
            }
        }
    }
}

