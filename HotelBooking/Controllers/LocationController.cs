using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotelBooking.Data.Constants;
using HotelBooking.Data.ViewModels;
using HotelBooking.Interfaces;
using HotelBooking.Models;
using HotelBooking.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HotelBooking.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class LocationController : ControllerBase
    {
        private ILocationService _locationService;
        private readonly ILogger<LocationController> _logger;
        public LocationController(ILocationService locationService, ILogger<LocationController> logger)
        {
            _locationService = locationService;
            _logger = logger;
        }

        [Route("[Action]")]
        [HttpPost]
        public IActionResult AddLocation(LocationVM location)
        {
            try
            {
                var _result = _locationService.CreateLocation(location);

                if (_result)
                {
                    return Ok(SuccessResponse.AddLocation);
                }
                else
                {
                    return NotFound(ErrorResponse.ErrorAddLocation);
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return NotFound(ErrorResponse.ErrorAddLocation);
            }
        }

        [Route("[Action]")]
        [HttpGet]
        public IActionResult GetLocations()
        {
            try
            {
                var _locations = _locationService.GetLocations();

                if (_locations != null)
                {
                    return Ok(_locations);
                }
                else
                {
                    return NotFound(ErrorResponse.ErrorGetLocations);
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return NotFound(ErrorResponse.ErrorGetLocations);
            }
        }
    }
}

