using HotelBooking.Controllers;
using HotelBooking.Data.ViewModels;
using HotelBooking.Interfaces;
using HotelBooking.Models;
using HotelBooking.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using ILogger = Serilog.ILogger;

namespace HotelBooking.UnitTest;

public class LocationControllerIsExecuting
{
    private readonly Mock<ILogger<LocationController>> logger;
    private readonly Mock<ILocationService> locationService;

    public LocationControllerIsExecuting()
    {
        logger = new Mock<ILogger<LocationController>>();
        locationService = new Mock<ILocationService>();
    }

    [Fact]
    public void AddLocation()
    {
        //arrange
        var locationList = AddLocationData();

        locationService.Setup(x => x.CreateLocation(locationList[0]))
            .Returns(true);
        var locationController = new LocationController(locationService.Object, logger.Object);
        
        //act
        var locationResult = locationController.AddLocation(locationList[0]);
        var OkLocationResult = locationResult as OkObjectResult;
        
        //assert
        Assert.Equal("Location Added Successfully", OkLocationResult.Value);
    }

    [Fact]
    public void GetLocations()
    {
        //arrange
        var locationList = GetLocationsData();

        locationService.Setup(x => x.GetLocations())
            .Returns(locationList);
        var locationController = new LocationController(locationService.Object, logger.Object);
        
        //act
        var locationResult = locationController.GetLocations();
        var OkLocationResult = locationResult as OkObjectResult;
        
        //assert
        Assert.Equal(locationList, OkLocationResult.Value);
    }

    private List<LocationVM> AddLocationData()
    {
        var locationList = new List<LocationVM>
        {
            new LocationVM()
            {
                location = "Salem"
            },
        };
        return locationList;
    }

    private List<Location> GetLocationsData()
    {
        var locationList = new List<Location>
        {
            new Location()
            {
                location = "Salem",
                locationId = 1
            },
            new Location()
            {
                location = "Coimbatore",
                locationId = 2
            },
        };
        return locationList;
    }
}