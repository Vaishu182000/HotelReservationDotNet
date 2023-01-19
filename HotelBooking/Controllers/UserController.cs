using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotelBooking.Data;
using HotelBooking.Data.Constants;
using HotelBooking.Data.ViewModels;
using HotelBooking.Models;
using HotelBooking.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HotelBooking.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class UserController : ControllerBase
    {
        private UserService _userService;
        private LocationService _locationService;
        private readonly ILogger<UserController> _logger;
        public UserController (UserService userService, LocationService locationService, ILogger<UserController> logger)
        {
            _userService = userService;
            _locationService = locationService;
            _logger = logger;
        }

        [Route("[Action]")]
        [HttpPost]
        public IActionResult UserSignUp(UserVM user)
        {
            bool _result = _userService.UserSignUp(user);

            if (_result)
            {
                return Ok(SuccessResponse.UserSignUp);
            }
            else
            {
                return NotFound(ErrorResponse.ErrorUserSignUp);
            }
        }

        [Route("[Action]")]
        [HttpGet]
        public IActionResult UserLogin(string userEmail, string userPassword)
        {
            bool _result = _userService.UserLogin(userEmail, userPassword);

            if (_result)
            {
                var _locations = _locationService.GetLocations();
                return Ok(new
                {
                    SuccessResponse.UserLogin, _locations
                });
            }
            else return NotFound(ErrorResponse.ErrorUserLogin);
        }
    }
}

