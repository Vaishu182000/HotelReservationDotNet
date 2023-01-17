using HotelBooking.Data.ViewModels;
using HotelBooking.Services;
using Microsoft.AspNetCore.Mvc;

namespace HotelBooking.Controllers;

[Route("[Controller]")]
[ApiController]
public class BookingController : ControllerBase
{
    private BookingService _bookingService;
    private readonly ILogger<BookingController> _logger;

    public BookingController(BookingService bookingService, ILogger<BookingController> logger)
    {
        _bookingService = bookingService;
        _logger = logger;
    }

    [Route("[Action]")]
    [HttpPost]
    public IActionResult CreateBooking(BookingVM booking)
    {
        var _result = _bookingService.CreateBooking(booking);

        if (_result)
        {
            _logger.LogInformation("Created Booking Successfully");
            return Ok("Created Booking Successfully");
        }
        else
        {
            _logger.LogError("Error in Added Booking details to DB");
            return NotFound();
        }
    }

    [Route("[Action]")]
    [HttpGet]
    public IActionResult BookingHistory(string userEmail)
    {
        try
        {
            return Ok(_bookingService.BookingHistory(userEmail));
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            _logger.LogError(e.Message);
            throw;
        }
    }

    [Route("[Action]")]
    [HttpDelete]
    public IActionResult CancelBooking(int id)
    {
        try
        {
            var _result = _bookingService.deleteBooking(id);
            if (_result) return Ok("Cancellation Successful");
            else return NotFound();
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            Console.WriteLine(e);
            throw;
        }
    }
}