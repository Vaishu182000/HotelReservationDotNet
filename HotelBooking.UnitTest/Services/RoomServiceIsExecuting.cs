using AutoMapper;
using Castle.Core.Logging;
using HotelBooking.Data;
using HotelBooking.Helpers;
using HotelBooking.Models;
using HotelBooking.S3;
using HotelBooking.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;

namespace HotelBooking.UnitTest.Services;

public class RoomServiceIsExecuting
{
    private readonly Mock<IMapper> _mapper;
    private readonly Mock<ILogger<RoomService>> _logger;
    private readonly Mock<DbSet<Room>> _mock;
    private readonly Mock<DbInitializer> _mockContext;
    private readonly Mock<HotelService> _hotelService;
    private readonly Mock<StringSplitHelper> _stringSplitHelper;
    private readonly Mock<IUploadToS3> _s3;

    public RoomServiceIsExecuting()
    {
        _mapper = new Mock<IMapper>();
        _logger = new Mock<ILogger<RoomService>>();
        _mock = new Mock<DbSet<Room>>();
        _mockContext = new Mock<DbInitializer>();
        _hotelService = new Mock<HotelService>();
        _stringSplitHelper = new Mock<StringSplitHelper>();
        _s3 = new Mock<IUploadToS3>();
    }

    [Fact]
    public void GetRoomByRoomName()
    {
        //arrange
        var roomsList = GetRoomsData();

        _mock.As<IQueryable<Room>>().Setup(m => m.Provider).Returns(roomsList.Provider);
        _mock.As<IQueryable<Room>>().Setup(m => m.Expression).Returns(roomsList.Expression);
        _mock.As<IQueryable<Room>>().Setup(m => m.ElementType).Returns(roomsList.ElementType);
        _mock.As<IQueryable<Room>>().Setup(m => m.GetEnumerator()).Returns(() => roomsList.GetEnumerator());
        
        _mockContext.Setup(m => m.Room).Returns(_mock.Object);
        
        //act
        var service = new RoomService(_mockContext.Object, _hotelService.Object, _mapper.Object, _logger.Object,
            _stringSplitHelper.Object, _s3.Object);
        var rooms = service.GetRoomByRoomName("GRT1");
        
        //assert
        Assert.Equal(1, rooms.roomId);
    }

    private IQueryable<Room> GetRoomsData()
    {
        var roomsList = new List<Room>
        {
            new Room()
            {
                roomId = 1,
                roomName = "GRT1",
                roomImage = "https://dotnet-training-hotelreservation.s3.amazonaws.com/GRT1.jpeg",
                roomRate = 2000,
                roomCapacity = 2,
                HotelId = 1
            },
        }.AsQueryable();
        return roomsList;
    }
}