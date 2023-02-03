using HotelBooking.Controllers;
using HotelBooking.Data.ViewModels;
using HotelBooking.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Serilog;

namespace HotelBooking.UnitTest;

public class HotelListDTO
{
    public string HotelListBasedOnLocation;
    public List<string> hotels;
}

public class HotelUnitTest
{
    private readonly Mock<ILogger<HotelController>> logger;

    public HotelUnitTest()
    {
        logger = new Mock<ILogger<HotelController>>();
    }

    [Fact]
    public void AddHotel()
    {
        //arrange
        var hotelList = AddHotelData();
        
        Mock<HotelService> hotelServiceSubject = new Mock<HotelService>();
        hotelServiceSubject.CallBase = true;

        hotelServiceSubject.Setup(x => x.AddHotel(hotelList[0]))
            .Returns(true);
        var hotelController = new HotelController(hotelServiceSubject.Object, logger.Object);
        
        //act
        var hotelResult = hotelController.CreateHotel(hotelList[0]);
        var okHotelResult = hotelResult as OkObjectResult;
        
        //assert
        Assert.Equal("Hotel Added Successfully", okHotelResult.Value);
    }

    // [Fact]
    // public void GetHotelList()
    // {
    //     //act
    //     var hotels = new List<string>();
    //     var hotelList = AddHotelData();
    //     hotels = hotelList.Select(h => h.hotelName).ToList();
    //     List<HotelListDTO> hotelListDtos = new List<HotelListDTO>
    //     {
    //         new HotelListDTO()
    //         {
    //             HotelListBasedOnLocation = "Retreived Hotel List based on Location",
    //             hotels = hotels
    //         },
    //     };
    //
    //     Mock<HotelService> hotelServiceSubject = new Mock<HotelService>();
    //     hotelServiceSubject.CallBase = true;
    //     
    //     hotelServiceSubject.Setup(x => x.HotelListByLocation("Salem"))
    //         .Returns(hotels);
    //     var hotelController = new HotelController(hotelServiceSubject.Object, logger.Object);
    //     
    //     //act
    //     var hotelResult = hotelController.HotelList("Salem");
    //     var OkHotelResult = hotelResult as OkObjectResult;
    //     var result = OkHotelResult.Value as HotelListDTO;
    //
    //     //assert
    //     Assert.Equal(hotelListDtos[0], OkHotelResult.Value);
    // }

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
}