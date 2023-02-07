using HotelBooking.Controllers;
using HotelBooking.Data.ViewModels;
using HotelBooking.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Serilog;

namespace HotelBooking.UnitTest;

public class BookingUnitTest
{
    public readonly Mock<IBookingService> bookingService;
    public readonly Mock<ILogger<BookingController>> logger;

    public BookingUnitTest()
    {
        bookingService = new Mock<IBookingService>();
        logger = new Mock<ILogger<BookingController>>();
    }

    [Fact]
    public void AddBooking()
    {
        //arrange
        var bookingList = AddBookingData();

        bookingService.Setup(x => x.CreateBooking(bookingList[0]))
            .Returns(true);
        var bookingController = new BookingController(bookingService.Object, logger.Object);
        
        //act
        var bookingResult = bookingController.CreateBooking(bookingList[0]);
        var okBookingResult = bookingResult as OkObjectResult;
        
        //assert
        Assert.Equal("Created Booking Successfully! An Email has been sent with Detailed Information", okBookingResult.Value);
    }

    private List<BookingVM> AddBookingData()
    {
        List<BookingVM> bookingData = new List<BookingVM>
        {
            new BookingVM()
            {
                noOfPersons = 1,
                paid = true,
                checkInTime = DateTime.Now,
                checkOutTime = DateTime.Now,
                roomId = 1,
                hotelId = 1,
                userId = 1
            },
        };
        return bookingData;
    }
}