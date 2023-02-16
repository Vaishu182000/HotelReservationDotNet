﻿using System;
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
using Microsoft.Extensions.Logging;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HotelBooking.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class HotelController : ControllerBase
    {
        private IHotelService _hotelService;
        private readonly ILogger<HotelController> _logger;
        public HotelController(IHotelService hotelService, ILogger<HotelController> logger)
        {
            _hotelService = hotelService;
            _logger = logger;
        }

        [Route("[Action]")]
        [HttpPost]
        public IActionResult CreateHotel(HotelVM hotel)
        {
            try
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
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return NotFound(ErrorResponse.ErrorAddHotel);
            }
        }

        [Route("[Action]")]
        [HttpGet]
        public IActionResult HotelList(string location)
        {
            try
            {
                List<string> hotel = _hotelService.HotelListByLocation(location);

                if (hotel != null)
                {
                    HotelResponseDTO responseDto = new HotelResponseDTO()
                        { HotelListBasedOnLocation = SuccessResponse.HotelListBasedOnLocation, hotels = hotel };
                    
                    return Ok(responseDto);
                }
                else
                {
                    return NotFound(ErrorResponse.ErrorInGetHotelBasedOnLocation);
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return NotFound(ErrorResponse.ErrorInGetHotelBasedOnLocation);
            }
        }

        [Route("[Action]")]
        [HttpGet]
        public IActionResult ListOfAllHotels()
        {
            try
            {
                var _hotelList = _hotelService.GetAllHotels();

                GetAllHotelsResponseDTO responseDto = new GetAllHotelsResponseDTO()
                    { GetHotel = SuccessResponse.GetHotel, hotelList = _hotelList };

                if (_hotelList != null)
                {
                    return Ok(responseDto);
                }
                else
                {
                    return NotFound(ErrorResponse.ErrorGetHotels);
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return NotFound(ErrorResponse.ErrorGetHotels);
            }
        }
    }
}

