using HotelBooking.Data.ViewModels;
using HotelBooking.Services;
using Microsoft.AspNetCore.Mvc;

namespace HotelBooking.Controllers;

[Route("[Controller]")]
[ApiController]
public class AvailabilityController : ControllerBase
{
    private AvailabilityService _availabilityService;

    public AvailabilityController(AvailabilityService availabilityService)
    {
        _availabilityService = availabilityService;
    }

    [Route("[Action]")]
    [HttpPost]
    public IActionResult CreateAvailability(AvailabilityVM availabilityVm)
    {
        var _result = _availabilityService.CreateAvailability(availabilityVm);

        if (_result) return Ok("Availability Created Successfully");
        else return NotFound();
    }
}