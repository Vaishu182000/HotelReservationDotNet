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
        private readonly ILogger<RoomController> _logger;
        public RoomController(RoomService roomService, ILogger<RoomController> logger)
        {
            _roomService = roomService;
            _logger = logger;
        }

        [Route("[Action]")]
        [HttpPost]
        public IActionResult CreateRoom([FromForm]RoomVM room)
        {
            var _result = _roomService.createRoom(room);

            if (_result)
            {
                _logger.LogInformation("Created Room Successfully");
                return Ok("Created Room Successfully");
            }
            else
            {
                _logger.LogError("Error in Added Rooming to the DB");
                return NotFound();
            }
        }

        [Route("[Action]")]
        [HttpPost]
        public IActionResult UploadRoomImage(IFormFile blob)
        {
            try
            {
                return Ok(_roomService.UploadRoomImage(blob));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                _logger.LogError(e.Message);
                throw;
            }
        }

        [Route("[Action]")]
        [HttpGet]
        public IActionResult ViewRoomDetails(string hotelName)
        {
            try
            {
                var _roomList = _roomService.GetRoomsByHotelName(hotelName);
                var message = $"The Rooms Available Under {hotelName}";
                _logger.LogInformation("Viewing Room Details");
                return Ok(new {
                    message,_roomList
                });
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                _logger.LogError(e.Message);
                throw;
            }
        }

        [Route("[Action]")]
        [HttpGet]
        public IActionResult DownloadImage(string fileName)
        {
            _roomService.DownloadImage(fileName);
            return Ok();
        }

        [Route("[Action]")]
        [HttpPost]
        public IActionResult CheckRoomAvailability(CheckRoomAvailability availability)
        {
            try
            {
                var _check = _roomService.CheckAvailability(availability);
                _logger.LogInformation("Availability Checked Succesfully");
                return Ok(_check);
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

