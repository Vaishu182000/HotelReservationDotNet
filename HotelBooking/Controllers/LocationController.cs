using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotelBooking.Data.ViewModels;
using HotelBooking.Models;
using HotelBooking.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HotelBooking.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class LocationController : ControllerBase
    {
        private LocationService _locationService;
        private readonly ILogger<LocationController> _logger;
        public LocationController(LocationService locationService, ILogger<LocationController> logger)
        {
            _locationService = locationService;
            _logger = logger;
        }

        [Route("[Action]")]
        [HttpPost]
        public IActionResult AddLocation(LocationVM location)
        {
            var _result = _locationService.CreateLocation(location);

            if (_result)
            {
                _logger.LogInformation("Location Added Successfully");
                return Ok("Location Added Successfully");
            }
            else
            {
                _logger.LogError("Error in Adding Location");
                return NotFound();
            }
        }

        [Route("[Action]")]
        [HttpGet]
        public IActionResult GetLocations ()
        {
            try
            {
                var _locations = _locationService.GetLocations();
                _logger.LogInformation("Extracted Locations from DB");
                return Ok(_locations);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                _logger.LogError(e.Message);
                throw;
            }
        }
    }
}

