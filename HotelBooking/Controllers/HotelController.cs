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
        public HotelController(HotelService hotelService)
        {
            _hotelService = hotelService;
        }

        [Route("[Action]")]
        [HttpPost]
        public IActionResult CreateHotel(HotelVM hotel)
        {
            var _result = _hotelService.AddHotel(hotel);

            if (_result) return Ok("Hotel Added Successfully");
            else return NotFound();
        }

        [Route("[Action]")]
        [HttpGet]
        public IActionResult HotelList(string location)
        {
            List<string> hotel = _hotelService.HotelListByLocation(location);
            return Ok(hotel);
        }

        [Route("[Action]")]
        [HttpGet]
        public IActionResult ListOfAllHotels()
        {
            var _hotelList = _hotelService.GetAllHotels();

            if (_hotelList != null) return Ok(_hotelList);
            else return NotFound();
        }
    }
}

