using System;
using AutoMapper;
using Azure.Storage.Blobs;
using HotelBooking.Data;
using HotelBooking.Data.ViewModels;
using HotelBooking.Models;

namespace HotelBooking.Services
{
	public class RoomService
	{
        private DbInitializer _dbContext;
        private HotelService _hotelService;
        private readonly IMapper _mapper;
        private readonly string _storageConnectionString;
        private readonly string _storageContainerName;
        private readonly ILogger<RoomService> _logger;
        public RoomService(DbInitializer dbContext, HotelService hotelService, IMapper mapper, IConfiguration configuration, ILogger<RoomService> logger)
		{
            _dbContext = dbContext;
            _hotelService = hotelService;
            _mapper = mapper;
            _logger = logger;
            _storageConnectionString = configuration.GetValue<string>("BlobConnectionString");
            _storageContainerName = configuration.GetValue<string>("BlobContainerName");
        }

        public string UploadRoomImage(IFormFile blob)
        {
            BlobContainerClient container = new BlobContainerClient(_storageConnectionString, _storageContainerName);

            try
            {
                BlobClient client = container.GetBlobClient(blob.FileName);
                
                using (Stream? data = blob.OpenReadStream())
                {
                    client.Upload(data);
                }

                return client.Uri.AbsoluteUri;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                _logger.LogError(e.Message);
                throw;
            }
        }

        public void DownloadImage(string blobFilename)
        {
            BlobContainerClient client = new BlobContainerClient(_storageConnectionString, _storageContainerName);

            try
            {
                BlobClient file = client.GetBlobClient(blobFilename);
                    var data = file.OpenReadAsync();
                    Task<Stream> blobContent = data;

                    // Download the file details async
                    var content = file.DownloadContentAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                _logger.LogError(e.Message);
                throw;
            }
        }
        
        public bool createRoom(RoomVM room)
        {
            if (room == null) return false;
            
            BlobContainerClient container = new BlobContainerClient(_storageConnectionString, _storageContainerName);

            try
            {
                BlobClient client = container.GetBlobClient(room.roomImage.FileName);
                
                using (Stream? data = room.roomImage.OpenReadStream())
                {
                    client.Upload(data);
                }

                if (client.Uri.AbsoluteUri != null)
                {
                    var _mappedroom = _mapper.Map<Room>(room);
                    _mappedroom.roomImage = client.Uri.AbsoluteUri;
                    _dbContext.Room.Add(_mappedroom);
                    _dbContext.SaveChanges();
                    return true;   
                }

                return false;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                _logger.LogError(e.Message);
                throw;
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

        public List<Room> CheckAvailability(CheckRoomAvailability availability)
        {
            var checkInTime = availability.checkInTime.ToString("dd/MM/yyyy").Split('/');
            var checkOutTime = availability.checkOutTime.ToString("dd/MM/yyyy").Split('/');
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
                    var bookCheckIn = booking.checkInTime.ToString("dd/MM/yyyy").Split('/');

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
                if (!roomIds.Contains(rooms.roomId)) roomList.Add(rooms);
            }
            return (roomList);
        }
	}
}

