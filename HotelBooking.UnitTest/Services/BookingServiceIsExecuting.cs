using AutoMapper;
using Castle.Core.Logging;
using HotelBooking.Data;
using HotelBooking.Models;
using HotelBooking.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;

namespace HotelBooking.UnitTest.Services;

public class BookingServiceIsExecuting
{
    private readonly Mock<IMapper> _mapper;
    private readonly Mock<ILogger<BookingService>> _logger;
    private readonly Mock<DbInitializer> _mockContext;
    private readonly Mock<DbSet<Booking>> _mock;
    private readonly Mock<UserService> _userService;

    public BookingServiceIsExecuting()
    {
        _mapper = new Mock<IMapper>();
        _logger = new Mock<ILogger<BookingService>>();
        _mockContext = new Mock<DbInitializer>();
        _mock = new Mock<DbSet<Booking>>();
        _userService = new Mock<UserService>();
    }

    [Fact]
    public void deleteBooking()
    {
        //arrange
        var bookingList = BookingData();
        _mockContext.Setup(m => m.Booking).Returns(_mock.Object);
        
        //act
        var service = new BookingService(_mockContext.Object, _userService.Object, _logger.Object, _mapper.Object);
        var result = service.deleteBooking(1);
        
        //assert
        Assert.Equal(true, result);
    }

    private List<Booking> BookingData()
    {
        var bookingList = new List<Booking>
        {
            new Booking()
            {
                bookingId = 1,
                noOfPersons = 2,
                paid = true,
                RoomId = 1,
                UserId = 1
            },
        };
        return bookingList;
    }
}