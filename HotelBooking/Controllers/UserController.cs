using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotelBooking.Data;
using HotelBooking.Data.Constants;
using HotelBooking.Data.ViewModels;
using HotelBooking.Interfaces;
using HotelBooking.Models;
using HotelBooking.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HotelBooking.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class UserController : ControllerBase
    {
        private IUserService _userService;
        private ILocationService _locationService;
        private readonly ILogger<UserController> _logger;
        public UserController(IUserService userService, ILocationService locationService, ILogger<UserController> logger)
        {
            _userService = userService;
            _locationService = locationService;
            _logger = logger;
        }

        [Route("[Action]")]
        [HttpPost]
        public ActionResult UserSignUp(UserVM user)
        {
            try
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
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return NotFound(ErrorResponse.ErrorUserSignUp);
            }
        }

        [Route("[Action]")]
        [HttpPost]
        public IActionResult UserLogin(UserLoginVM userLoginVm)
        {
            try
            {
                string jwt = _userService.UserLogin(userLoginVm);

                if (jwt != null)
                {
                    _logger.LogInformation(jwt);

                    var _locations = _locationService.GetLocations();
                    UserResponseDTO responseDto = new UserResponseDTO()
                        { UserLogin = SuccessResponse.UserLogin, jwt = jwt, Locations = _locations };

                    return Ok(responseDto);
                }
                else return NotFound(ErrorResponse.ErrorUserLogin);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return NotFound(ErrorResponse.ErrorUserLogin);
            }
        }

        [Route("[Action]")]
        [HttpPut]
        public IActionResult ForgotPassword(UserPasswordVM userPasswordVm)
        {
            try
            {
                var _result = _userService.changePassword(userPasswordVm);

                if (_result) return Ok(SuccessResponse.UserForgotPassword);
                else return NotFound(ErrorResponse.ErrorUserForgotPassword);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return NotFound(ErrorResponse.ErrorUserForgotPassword);
            }
        }
    }
}

