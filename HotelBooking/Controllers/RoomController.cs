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
    public class RoomController : ControllerBase
    {
        private RoomService _roomService;
        public RoomController(RoomService roomService)
        {
            _roomService = roomService;
        }

        [Route("[Action]")]
        [HttpPost]
        public IActionResult CreateRoom(RoomVM room)
        {
            var _result = _roomService.createRoom(room);

            if (_result) return Ok("Created Room Successfully");
            else return NotFound();
        }

        [Route("[Action]")]
        [HttpGet]
        public IActionResult ViewRoomDetails(string hotelName)
        {
            var _roomList = _roomService.GetRoomsByHotelName(hotelName);
            var message = $"The Rooms Available Under {hotelName}";
            return Ok(new {
                message,_roomList
            });
        }
    }
}

