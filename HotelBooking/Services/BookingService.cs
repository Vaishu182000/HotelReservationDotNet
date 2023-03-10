using AutoMapper;
using Azure.Communication.Email;
using Azure.Communication.Email.Models;
using HotelBooking.Data;
using HotelBooking.Data.Constants;
using HotelBooking.Data.ViewModels;
using HotelBooking.Interfaces;
using HotelBooking.Models;

namespace HotelBooking.Services;

public class BookingService : IBookingService
{
    private DbInitializer _dbContext;
    private RoomService _roomService;
    private UserService _userService;
    private readonly ILogger<BookingService> _logger;
    public readonly IMapper _mapper;
    public IConfiguration _configuration;
    private ServiceBusService _serviceBusService;

    public BookingService(DbInitializer dbContext, 
        RoomService roomService, 
        UserService userService, 
        ILogger<BookingService> logger, 
        IMapper mapper, 
        IConfiguration configuration, 
        ServiceBusService serviceBusService
        )
    {
        _dbContext = dbContext;
        _roomService = roomService;
        _userService = userService;
        _logger = logger;
        _mapper = mapper;
        _configuration = configuration;
        _serviceBusService = serviceBusService;
    }

    public bool CreateBooking(BookingVM booking)
    {
        try
        {
            var _room = _dbContext.Room.FirstOrDefault(r => r.roomId == booking.roomId);

            if (_room.HotelId == booking.hotelId)
            {
                var _mappedBooking = _mapper.Map<Booking>(booking);
                var user = _dbContext.User.Find(booking.userId);
                var hotel = _dbContext.Hotel.Find(booking.hotelId);

                _dbContext.Booking.Add(_mappedBooking);
                _dbContext.SaveChanges();

                _serviceBusService.SendMessageAsync(_mappedBooking);
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