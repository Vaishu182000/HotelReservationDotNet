using System.Collections.Generic;
using System.Linq;
using HotelBooking.Controllers;
using HotelBooking.Data;
using HotelBooking.Data.Constants;
using HotelBooking.Data.ViewModels;
using HotelBooking.Interfaces;
using HotelBooking.Models;
using HotelBooking.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Serilog;

namespace HotelBooking.UnitTest;

public class HotelControllerIsExecuting
{
    private readonly Mock<ILogger<HotelController>> logger;
    private readonly Mock<IHotelService> hotelService;

    public HotelControllerIsExecuting()
    {
        logger = new Mock<ILogger<HotelController>>();
        hotelService = new Mock<IHotelService>();
    }

    [Fact]
    public void AddHotel()
    {
        //arrange
        var hotelList = AddHotelData();

        hotelService.Setup(x => x.AddHotel(hotelList[0]))
            .Returns(true);
        var hotelController = new HotelController(hotelService.Object, logger.Object);
        
        //act
        var hotelResult = hotelController.CreateHotel(hotelList[0]);
        var okHotelResult = hotelResult as OkObjectResult;
        
        //assert
        Assert.Equal("Hotel Added Successfully", okHotelResult.Value);
    }

    [Fact]
    public void GetHotelList()
    {
        //act
        var hotels = new List<string>();
        var hotelList = AddHotelData();
        hotels = hotelList.Select(h => h.hotelName).ToList();
        HotelResponseDTO expected = new HotelResponseDTO();
        expected.HotelListBasedOnLocation = "Retreived Hotel List based on Location";
        expected.hotels = hotels;
        
        hotelService.Setup(x => x.HotelListByLocation("Salem"))
            .Returns(hotels);
        var hotelController = new HotelController(hotelService.Object, logger.Object);
        
        //act
        var hotelResult = hotelController.HotelList("Salem");
        var OkHotelResult = hotelResult as OkObjectResult;
        var result = OkHotelResult.Value as HotelResponseDTO;
    
        //assert
        Assert.Equal(expected, result);
    }

    [Fact]
    public void ListOfAllHotels()
    {
        //arrange
        var hotelsList = GetHotels();
        GetAllHotelsResponseDTO responseDto = new GetAllHotelsResponseDTO();
        responseDto.GetHotel = SuccessResponse.GetHotel;
        responseDto.hotelList = hotelsList;

        hotelService.Setup(x => x.GetAllHotels()).Returns(hotelsList);
        var hotelController = new HotelController(hotelService.Object, logger.Object);
        
        //act
        var hotelResult = hotelController.ListOfAllHotels();
        var OkHotelResult = hotelResult as OkObjectResult;
        var result = OkHotelResult.Value as GetAllHotelsResponseDTO;
        
        //assert
        Assert.Equal(responseDto, result);
    }

    private List<HotelVM> AddHotelData()
    {
        List<HotelVM> hotelList = new List<HotelVM>
        {
            new HotelVM()
            {
                hotelName = "GRT",
                noOfRooms = 2,
                locationId = 1
            },
            new HotelVM()
            {
                hotelName = "ABCD",
                noOfRooms = 2,
                locationId = 1
            },
        };
        return hotelList;
    }

    private List<Hotel> GetHotels()
    {
        return new List<Hotel>
        {
            new Hotel()
            {
                hotelId = 1,
                hotelName = "GRT",
                noOfRooms = 2,
                LocationId = 1
            },
            new Hotel()
            {
                hotelId = 2,
                hotelName = "ABCD",
                noOfRooms = 2,
                LocationId = 1
            },
        };
    }
}