using HotelBooking.Controllers;
using HotelBooking.Data.ViewModels;
using HotelBooking.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Serilog;

namespace HotelBooking.UnitTest;

public class RoomUnitTest
{
    private readonly Mock<IRoomService> roomService;
    private readonly Mock<ILogger<RoomController>> logger;

    public RoomUnitTest()
    {
        roomService = new Mock<IRoomService>();
        logger = new Mock<ILogger<RoomController>>();
    }

    [Fact]
    public void AddRoom()
    {
        //arrange
        var roomList = AddRoomData();

        roomService.Setup(x => x.createRoom(roomList[0]))
            .Returns(true);
        var roomController = new RoomController(roomService.Object, logger.Object);
        
        //act
        var roomResult = roomController.CreateRoom(roomList[0]);
        var okRoomResult = roomResult as OkObjectResult;
        
        //assert
        Assert.Equal("Created Room Successfully", okRoomResult.Value);
    }

    private List<RoomVM> AddRoomData()
    {
        List<RoomVM> roomData = new List<RoomVM>
        {
            new RoomVM()
            {
                roomName = "GRT1",
                roomRate = 2000,
                roomCapacity = 2,
                hotelId = 1
            },
        };
        return roomData;
    }
}