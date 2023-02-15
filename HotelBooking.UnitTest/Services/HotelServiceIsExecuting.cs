using AutoMapper;
using HotelBooking.Data;
using HotelBooking.Data.ViewModels;
using HotelBooking.Interfaces;
using HotelBooking.Models;
using HotelBooking.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using Serilog;
using ILogger = Serilog.ILogger;

namespace HotelBooking.UnitTest.Services;

public class HotelServiceIsExecuting
{
    private readonly Mock<IMapper> _mapper;
    private readonly Mock<ILogger<HotelService>> _logger;
    private readonly Mock<ILogger<LocationService>> _loggerLocation;
    private readonly Mock<DbSet<Hotel>> _mock;
    private readonly Mock<DbSet<Location>> _mockLocation;
    private readonly Mock<DbInitializer> _mockContext;
    private readonly Mock<ILocationService> _locationService;

    public HotelServiceIsExecuting()
    {
        _mapper = new Mock<IMapper>();
        _logger = new Mock<ILogger<HotelService>>();
        _mock = new Mock<DbSet<Hotel>>();
        _mockContext = new Mock<DbInitializer>();
        _locationService = new Mock<ILocationService>();
        _mockLocation = new Mock<DbSet<Location>>();
        _loggerLocation = new Mock<ILogger<LocationService>>();
    }

    [Fact]
    public void AddHotel()
    {
        //arrange
        var hotelList = AddHotelData();

        _mockContext.Setup(m => m.Hotel).Returns(_mock.Object);

        //act
        var service = new HotelService(_mockContext.Object, _locationService.Object, _mapper.Object, _logger.Object);
        service.AddHotel(hotelList[0]);
        
        //assert
        _mock.Verify(m => m.Add(It.IsAny<Hotel>()), Times.Once());
        _mockContext.Verify(m => m.SaveChanges(), Times.Once());
    }

    [Fact]
    public void GetHotelByHotelName()
    {
        //arrange
        var hotelList = GetHotelData();
        
        _mock.As<IQueryable<Hotel>>().Setup(m => m.Provider).Returns(hotelList.Provider);
        _mock.As<IQueryable<Hotel>>().Setup(m => m.Expression).Returns(hotelList.Expression);
        _mock.As<IQueryable<Hotel>>().Setup(m => m.ElementType).Returns(hotelList.ElementType);
        _mock.As<IQueryable<Hotel>>().Setup(m => m.GetEnumerator()).Returns(() => hotelList.GetEnumerator());
        _mockContext.Setup(m => m.Hotel).Returns(_mock.Object);
        
        //act
        var service = new HotelService(_mockContext.Object, _locationService.Object, _mapper.Object, _logger.Object);
        var hotel = service.GetHotelByHotelName("GRT");
        
        //assert
        Assert.Equal(1, hotel.hotelId);
    }

    [Fact]
    public void GetAllHotels()
    {
        //act
        var hotelList = GetHotelData();
        
        _mock.As<IQueryable<Hotel>>().Setup(m => m.Provider).Returns(hotelList.Provider);
        _mock.As<IQueryable<Hotel>>().Setup(m => m.Expression).Returns(hotelList.Expression);
        _mock.As<IQueryable<Hotel>>().Setup(m => m.ElementType).Returns(hotelList.ElementType);
        _mock.As<IQueryable<Hotel>>().Setup(m => m.GetEnumerator()).Returns(() => hotelList.GetEnumerator());
        
        _mockContext.Setup(m => m.Hotel).Returns(_mock.Object);
        
        
        //act
        var service = new HotelService(_mockContext.Object, _locationService.Object, _mapper.Object, _logger.Object);
        var hotels = service.GetAllHotels();
        
        //assert
        Assert.Equal(2, hotels.Count);
    }

    [Fact]
    public void HotelListByLocation()
    {
        //arrange
        IQueryable<string> hotelList = new List<string> { "GRT", "ABCD" }.AsQueryable();
        var hotelsListData = GetHotelData();
        var locationList = LocationList();

        _mock.As<IQueryable<Hotel>>().Setup(m => m.Provider).Returns(hotelsListData.Provider);
        _mock.As<IQueryable<Hotel>>().Setup(m => m.Expression).Returns(hotelsListData.Expression);
        _mock.As<IQueryable<Hotel>>().Setup(m => m.ElementType).Returns(hotelsListData.ElementType);
        _mock.As<IQueryable<Hotel>>().Setup(m => m.GetEnumerator()).Returns(() => hotelsListData.GetEnumerator());
        _mockContext.Setup(m => m.Hotel).Returns(_mock.Object);
        
        _mockLocation.As<IQueryable<Location>>().Setup(m => m.Provider).Returns(locationList.Provider);
        _mockLocation.As<IQueryable<Location>>().Setup(m => m.Expression).Returns(locationList.Expression);
        _mockLocation.As<IQueryable<Location>>().Setup(m => m.ElementType).Returns(locationList.ElementType);
        _mockLocation.As<IQueryable<Location>>().Setup(m => m.GetEnumerator()).Returns(() => locationList.GetEnumerator());
        _mockContext.Setup(m => m.Location).Returns(_mockLocation.Object);
        var _locationService = new LocationService(_mockContext.Object, _mapper.Object, _loggerLocation.Object);
        //act
        var service = new HotelService(_mockContext.Object, _locationService, _mapper.Object, _logger.Object);
        var hotelsList = service.HotelListByLocation("Salem");
        
        //assert
        Assert.Equal(2, hotelsList.Count);
    }

    private List<HotelVM> AddHotelData()
    {
        var hotelList = new List<HotelVM>
        {
            new HotelVM()
            {
                hotelName = "GRT",
                locationId = 1,
                noOfRooms = 10
            },
        };
        return hotelList;
    }

    private IQueryable<Hotel> GetHotelData()
    {
        var hotelList = new List<Hotel>
        {
            new Hotel()
            {
                hotelId = 1,
                hotelName = "GRT",
                LocationId = 1,
                noOfRooms = 10
            },
            new Hotel()
            {
                hotelId = 2,
                hotelName = "ABCD",
                LocationId = 1,
                noOfRooms = 10
            },
        }.AsQueryable();
        return hotelList;
    }

    private IQueryable<Location> LocationList()
    {
        return new List<Location>
        {
            new Location()
            {
                locationId = 1,
                location = "Salem"
            },
        }.AsQueryable();
    }
}