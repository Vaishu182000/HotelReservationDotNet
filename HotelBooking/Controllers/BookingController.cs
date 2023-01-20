using HotelBooking.Data.Constants;
using HotelBooking.Data.ViewModels;
using HotelBooking.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HotelBooking.Controllers;

[Authorize]
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
            return Ok(SuccessResponse.AddBooking);
        }
        else
        {
            return NotFound(ErrorResponse.ErrorAddBooking);
        }
    }

    [Route("[Action]")]
    [HttpGet]
    public IActionResult BookingHistory(string userEmail)
    {
        try
        {
            var _history = _bookingService.BookingHistory(userEmail);
            return Ok(new
            {
                SuccessResponse.BookingHistoryOfUser,_history
            });
        }
        catch (Exception e)
        {
            return NotFound(ErrorResponse.ErrorBookingHistoryOfUser);
        }
    }

    [Route("[Action]")]
    [HttpDelete]
    public IActionResult CancelBooking(int id)
    {
        var _result = _bookingService.deleteBooking(id);
        if (_result) return Ok(SuccessResponse.CancelBooking);
        else return NotFound(ErrorResponse.ErrorCancelBooking);
    }
}