using HotelBooking.Data.ViewModels;
using HotelBooking.Services;
using Microsoft.AspNetCore.Mvc;

namespace HotelBooking.Controllers;

[Route("[Controller]")]
[ApiController]
public class BookingController : ControllerBase
{
    private BookingService _bookingService;

    public BookingController(BookingService bookingService)
    {
        _bookingService = bookingService;
    }

    [Route("[Action]")]
    [HttpPost]
    public IActionResult CreateBooking(BookingVM booking)
    {
        var _result = _bookingService.CreateBooking(booking);

        if (_result) return Ok("Created Booking Successfully");
        else return NotFound();
    }
}