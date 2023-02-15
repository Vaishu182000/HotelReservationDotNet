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

public class BookingControllerIsExecuting
{
    public readonly Mock<IBookingService> bookingService;
    public readonly Mock<ILogger<BookingController>> logger;

    public BookingControllerIsExecuting()
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

    [Fact]
    public void BookingHistory()
    {
        //arrange
        var bookingList = GetBooking();

        bookingService.Setup(x => x.BookingHistory("vaishnavis@presidio.com")).Returns(bookingList);
        var bookingController = new BookingController(bookingService.Object, logger.Object);
        BookingResponseDTO responseDto = new BookingResponseDTO()
            { message = SuccessResponse.BookingHistoryOfUser, bookingList = bookingList };
        
        //act
        var bookingResult = bookingController.BookingHistory("vaishnavis@presidio.com");
        var okBookingResult = bookingResult as OkObjectResult;
        var result = okBookingResult.Value as BookingResponseDTO;

        //assert
        Assert.Equal(responseDto, result);
    }

    [Fact]
    public void CancelBooking()
    {
        //arrange
        bookingService.Setup(x => x.deleteBooking(1)).Returns(true);
        var bookingController = new BookingController(bookingService.Object, logger.Object);
        
        //act
        var bookingResult = bookingController.CancelBooking(1);
        var okBookingResult = bookingResult as OkObjectResult;
        
        //assert
        Assert.Equal("Cancellation Successfull", okBookingResult.Value);
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

    private IQueryable<Booking> GetBooking()
    {
        return new List<Booking>
        {
            new Booking()
            {
                bookingId = 1,
                checkInTime = DateTime.Now,
                checkOutTime = DateTime.Now,
                noOfPersons = 2,
                paid = true,
                RoomId = 1,
                UserId = 1
            },
            new Booking()
            {
                bookingId = 2,
                checkInTime = DateTime.Now,
                checkOutTime = DateTime.Now,
                noOfPersons = 2,
                paid = true,
                RoomId = 2,
                UserId = 1
            },
        }.AsQueryable();
    }
}