using HotelBooking.Data;
using HotelBooking.Data.Constants;
using HotelBooking.Data.ViewModels;
using HotelBooking.Interfaces;
using HotelBooking.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HotelBooking.Controllers;

[Authorize]
[Route("[Controller]")]
[ApiController]
public class BookingController : ControllerBase
{
    private IBookingService _bookingService;
    private readonly ILogger<BookingController> _logger;

    public BookingController(IBookingService bookingService, ILogger<BookingController> logger)
    {
        _bookingService = bookingService;
        _logger = logger;
    }

    [Route("[Action]")]
    [HttpPost]
    public IActionResult CreateBooking(BookingVM booking)
    {
        try
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
        catch (Exception e)
        {
            _logger.LogError(e.Message);
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
            BookingResponseDTO responseDto = new BookingResponseDTO(){message = SuccessResponse.BookingHistoryOfUser, bookingList = _history};
            return Ok(responseDto);
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
        try
        {
            var _result = _bookingService.deleteBooking(id);
            if (_result) return Ok(SuccessResponse.CancelBooking);
            else return NotFound(ErrorResponse.ErrorCancelBooking);
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return NotFound(ErrorResponse.ErrorCancelBooking);
        }
    }
}