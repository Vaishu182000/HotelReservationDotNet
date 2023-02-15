using HotelBooking.Controllers;
using HotelBooking.Data;
using HotelBooking.Data.Constants;
using HotelBooking.Data.ViewModels;
using HotelBooking.Interfaces;
using HotelBooking.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Serilog;

namespace HotelBooking.UnitTest;

public class RoomControllerIsExecuting
{
    private readonly Mock<IRoomService> roomService;
    private readonly Mock<ILogger<RoomController>> logger;

    public RoomControllerIsExecuting()
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
            .Returns(Task.FromResult(true));
        var roomController = new RoomController(roomService.Object, logger.Object);
        
        //act
        var roomResult = roomController.CreateRoom(roomList[0]);
        var okRoomResult = roomResult as OkObjectResult;
        
        //assert
        Assert.Equal("Created Room Successfully", okRoomResult.Value);
    }

    [Fact]
    public void ViewRoomDetails()
    {
        //arrange
        var roomsList = GetRoomsData();

        roomService.Setup(x => x.GetRoomsByHotelName("GRT")).Returns(roomsList);
        var roomController = new RoomController(roomService.Object, logger.Object);
        RoomResponseDTO responseDto = new RoomResponseDTO();
        responseDto.message = $"The Rooms Available Under GRT";
        responseDto.roomList = roomsList;
        
        //act
        var roomResult = roomController.ViewRoomDetails("GRT");
        var okRoomResult = roomResult as OkObjectResult;
        var result = okRoomResult.Value as RoomResponseDTO;
        
        //assert
        Assert.Equal(responseDto, result);
    }

    [Fact]
    public void CheckRoomAvailability()
    {
        //arrange
        var roomsList = GetRoomsData();

        roomService.Setup(x => x.CheckAvailability(new CheckRoomAvailability()
                { hotelName = "GRT", noOfPersons = 2, checkInTime = DateTime.Now, checkOutTime = DateTime.Now }))
            .Returns(roomsList);
        var roomController = new RoomController(roomService.Object, logger.Object);
        RoomResponseDTO responseDto = new RoomResponseDTO();
        responseDto.message = SuccessResponse.CheckRoomAvailability;
        responseDto.roomList = roomsList;
        
        //act
        var roomResult = roomController.CheckRoomAvailability(new CheckRoomAvailability()
            { hotelName = "GRT", noOfPersons = 2, checkInTime = DateTime.Now, checkOutTime = DateTime.Now });
        var okRoomResult = roomResult as OkObjectResult;
        var result = okRoomResult.Value as RoomResponseDTO;
        
        //assert
        Assert.Equal(responseDto.message, result.message);
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

    private IQueryable<Room> GetRoomsData()
    {
        return new List<Room>
        {
            new Room()
            {
                HotelId = 1,
                roomCapacity = 2,
                roomId = 1,
                roomImage = "https://dotnet-training-hotelreservation.s3.amazonaws.com/GRT1.jpeg",
                roomName = "GRT1",
                roomRate = 2000
            },
            new Room()
            {
                HotelId = 1,
                roomCapacity = 2,
                roomId = 2,
                roomImage = "https://dotnet-training-hotelreservation.s3.amazonaws.com/GRT1.jpeg",
                roomName = "GRT2",
                roomRate = 1500
            },
        }.AsQueryable();
    }
}