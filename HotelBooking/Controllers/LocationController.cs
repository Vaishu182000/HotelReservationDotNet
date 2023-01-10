using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        public LocationController(LocationService locationService)
        {
            _locationService = locationService;
        }

        [Route("[Action]")]
        [HttpPost]
        public IActionResult AddLocation(Location location)
        {
            var _result = _locationService.CreateLocation(location);

            if (_result) return Ok("Location Added Successfully");
            else return NotFound();
        }

        [Route("[Action]")]
        [HttpGet]
        public IActionResult GetLocations ()
        {
            return Ok(_locationService.GetLocations());
        }
    }
}

