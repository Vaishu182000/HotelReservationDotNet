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

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HotelBooking.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class RoomController : ControllerBase
    {
        private IRoomService _roomService;
        private readonly ILogger<RoomController> _logger;
        public RoomController(IRoomService roomService, ILogger<RoomController> logger)
        {
            _roomService = roomService;
            _logger = logger;
        }

        [Route("[Action]")]
        [HttpPost]
        public IActionResult CreateRoom([FromForm] RoomVM room)
        {
            try
            {
                var _result = _roomService.createRoom(room);

                if (_result)
                {
                    return Ok(SuccessResponse.AddRoom);
                }
                else
                {
                    return NotFound(ErrorResponse.ErrorAddRoom);
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return NotFound(ErrorResponse.ErrorAddRoom);
            }
        }

        [Route("[Action]")]
        [HttpGet]
        public IActionResult ViewRoomDetails(string hotelName)
        {
            try
            {
                var _roomList = _roomService.GetRoomsByHotelName(hotelName);

                if (_roomList != null)
                {
                    // _roomList = _roomService.getLocationInList(_roomList);
                    var message = $"The Rooms Available Under {hotelName}";
                    return Ok(new
                    {
                        message,
                        _roomList
                    });
                }
                else
                {
                    return NotFound(ErrorResponse.ErrorViewRoomDetails);
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return NotFound(ErrorResponse.ErrorViewRoomDetails);
            }
        }

        [Route("[Action]")]
        [HttpPost]
        public IActionResult CheckRoomAvailability(CheckRoomAvailability availability)
        {
            try
            {
                var _check = _roomService.CheckAvailability(availability);
                _logger.LogInformation("Availability Checked Successfully");
                return Ok(new
                {
                    SuccessResponse.CheckRoomAvailability,
                    _check
                });
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return NotFound(ErrorResponse.ErrorAvailabilityCheck);
            }
        }
    }
}

