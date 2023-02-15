using AutoMapper;
using HotelBooking.Data;
using HotelBooking.Data.ViewModels;
using HotelBooking.Models;
using HotelBooking.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using ILogger = Castle.Core.Logging.ILogger;

namespace HotelBooking.UnitTest.Services;

public class LocationServiceIsExecuting
{
    private readonly Mock<IMapper> _mapper;
    private readonly Mock<ILogger<LocationService>> _logger;
    private readonly Mock<DbSet<Location>> _mock;
    private readonly Mock<DbInitializer> _mockContext;

    public LocationServiceIsExecuting()
    {
        _mapper = new Mock<IMapper>();
        _logger = new Mock<ILogger<LocationService>>();
        _mock = new Mock<DbSet<Location>>();
        _mockContext = new Mock<DbInitializer>();
    }
    
    [Fact]
    public void createLocation()
    {
        //arrange
        var locationList = AddLocationData();
        
        _mockContext.Setup(m => m.Location).Returns(_mock.Object);
        
        //act
        var service = new LocationService(_mockContext.Object, _mapper.Object, _logger.Object);
        service.CreateLocation(locationList[0]);
        
        //assert
        _mock.Verify(m => m.Add(It.IsAny<Location>()), Times.Once());
        _mockContext.Verify(m => m.SaveChanges(), Times.Once());
    }

    [Fact]
    public void GetLocations()
    {
        var locationList = GetLocationsData();

        _mock.As<IQueryable<Location>>().Setup(m => m.Provider).Returns(locationList.Provider);
        _mock.As<IQueryable<Location>>().Setup(m => m.Expression).Returns(locationList.Expression);
        _mock.As<IQueryable<Location>>().Setup(m => m.ElementType).Returns(locationList.ElementType);
        _mock.As<IQueryable<Location>>().Setup(m => m.GetEnumerator()).Returns(() => locationList.GetEnumerator());
        
        _mockContext.Setup(c => c.Location).Returns(_mock.Object);

        var service = new LocationService(_mockContext.Object, _mapper.Object, _logger.Object);
        var locations = service.GetLocations();
        
        Assert.Equal(2, locations.Count);
        Assert.Equal("Salem", locations[0].location);
    }

    [Fact]
    public void GetLocationByLocationName()
    {
        var locationList = GetLocationsData();
        
        _mock.As<IQueryable<Location>>().Setup(m => m.Provider).Returns(locationList.Provider);
        _mock.As<IQueryable<Location>>().Setup(m => m.Expression).Returns(locationList.Expression);
        _mock.As<IQueryable<Location>>().Setup(m => m.ElementType).Returns(locationList.ElementType);
        _mock.As<IQueryable<Location>>().Setup(m => m.GetEnumerator()).Returns(() => locationList.GetEnumerator());
        
        _mockContext.Setup(c => c.Location).Returns(_mock.Object);
        
        var service = new LocationService(_mockContext.Object, _mapper.Object, _logger.Object);
        var location = service.GetLocationByLocationName("Salem");
        
        Assert.Equal(1, location.locationId);
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

    private IQueryable<Location> GetLocationsData()
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
        }.AsQueryable();
        return locationList;
    }
}