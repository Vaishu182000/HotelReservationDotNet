using System;
using System.Linq;
using AutoMapper;
using Azure.Communication.Email;
using Azure.Communication.Email.Models;
using HotelBooking.Data;
using HotelBooking.Data.Constants;
using HotelBooking.Data.ViewModels;
using HotelBooking.Email;
using HotelBooking.Interfaces;
using HotelBooking.Models;
using HotelBooking.SecretManager.Interface;
using Microsoft.Extensions.Logging;

namespace HotelBooking.Services;

public class BookingService : IBookingService
{
    private DbInitializer _dbContext;
    private IUserService _userService;
    private readonly ILogger<BookingService> _logger;
    public readonly IMapper _mapper;
    private readonly IConfigSettings _configSettings;
    private ISendEmail _email;

    public BookingService(DbInitializer dbContext, 
        IUserService userService, 
        ILogger<BookingService> logger, 
        IMapper mapper, IConfigSettings configSettings, ISendEmail email
        )
    {
        _dbContext = dbContext;
        _userService = userService;
        _logger = logger;
        _mapper = mapper;
        _configSettings = configSettings;
        _email = email;
    }

    public bool CreateBooking(BookingVM booking)
    {
        try
        {
            var _room = _dbContext.Room.FirstOrDefault(r => r.roomId == booking.roomId);

            if (_room.HotelId == booking.hotelId)
            {
                var _mappedBooking = _mapper.Map<Booking>(booking);

                _dbContext.Booking.Add(_mappedBooking);
                _dbContext.SaveChanges();
                
                _email.Send(_configSettings.AwsAccessKey, _configSettings.AwsSecretKey,_configSettings.AwsSessionToken);
                return true;
            }
            else
            {
                _logger.LogError(ErrorResponse.ErrorAddBooking);
                return false;
            }
        }
        catch
        {
            return false;
        }
    }

    public IQueryable<Booking> BookingHistory(string userEmail)
    {
        try
        {
            var _user = _userService.GetUserByUserEmail(userEmail);

            var _booking = _dbContext.Booking.Where(b => b.UserId == _user.userId);
            _logger.LogInformation($"Retrieved Booking Information of {userEmail}");
            return _booking;
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return null;
        }
    }

    public bool deleteBooking(int id)
    {
        try
        {
            var _booking = _dbContext.Booking.Find(id);

            _dbContext.Booking.Remove(_booking);
            _dbContext.SaveChanges();

            _logger.LogInformation($"Deleted Information of Booking with {id}");
            return true;
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return false;
        }
    }
}