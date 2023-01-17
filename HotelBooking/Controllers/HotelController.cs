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
    public class HotelController : ControllerBase
    {
        private HotelService _hotelService;
        private readonly ILogger<HotelController> _logger;
        public HotelController(HotelService hotelService, ILogger<HotelController> logger)
        {
            _hotelService = hotelService;
            _logger = logger;
        }

        [Route("[Action]")]
        [HttpPost]
        public IActionResult CreateHotel(HotelVM hotel)
        {
            var _result = _hotelService.AddHotel(hotel);

            if (_result)
            {
                _logger.LogInformation("Hotel Added Successfully");
                return Ok("Hotel Added Successfully");
            }
            else
            {
                _logger.LogError("Error in Added the Hotel to DB");
                return NotFound();
            }
        }

        [Route("[Action]")]
        [HttpGet]
        public IActionResult HotelList(string location)
        {
            try
            {
                List<string> hotel = _hotelService.HotelListByLocation(location);
                _logger.LogInformation("Retreived Hotel List based on Location");
                return Ok(hotel);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                Console.WriteLine(e);
                throw;
            }
        }

        [Route("[Action]")]
        [HttpGet]
        public IActionResult ListOfAllHotels()
        {
            try
            {
                var _hotelList = _hotelService.GetAllHotels();
                _logger.LogInformation("Retreived Hotel List");
                return Ok(_hotelList);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                _logger.LogError(e.Message);
                return NotFound();
                throw;
            }
        }
    }
}

