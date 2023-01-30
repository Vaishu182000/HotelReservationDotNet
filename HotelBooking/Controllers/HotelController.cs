using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotelBooking.Data.Constants;
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
                return Ok(SuccessResponse.AddHotel);
            }
            else
            {
                return NotFound(ErrorResponse.ErrorAddHotel);
            }
        }

        [Route("[Action]")]
        [HttpGet]
        public IActionResult HotelList(string location)
        {
            List<string> hotel = _hotelService.HotelListByLocation(location);

            if (hotel != null)
            {
                return Ok(new
                {
                    SuccessResponse.HotelListBasedOnLocation,
                    hotel
                });
            }
            else
            {
                return NotFound(ErrorResponse.ErrorInGetHotelBasedOnLocation);
            }
        }

        [Route("[Action]")]
        [HttpGet]
        public IActionResult ListOfAllHotels()
        {
            var _hotelList = _hotelService.GetAllHotels();

            if (_hotelList != null)
            {
                return Ok(new
                {
                    SuccessResponse.GetHotel,
                    _hotelList
                });
            }
            else
            {
                return NotFound(ErrorResponse.ErrorGetHotels);
            }
        }
    }
}

