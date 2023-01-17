using AutoMapper;
using HotelBooking.Data;
using HotelBooking.Data.ViewModels;
using HotelBooking.Models;

namespace HotelBooking.Services;

public class BookingService
{
    private DbInitializer _dbContext;
    private RoomService _roomService;
    private UserService _userService;
    private readonly ILogger<BookingService> _logger;
    public readonly IMapper _mapper;

    public BookingService(DbInitializer dbContext, RoomService roomService, UserService userService, ILogger<BookingService> logger, IMapper mapper)
    {
        _dbContext = dbContext;
        _roomService = roomService;
        _userService = userService;
        _logger = logger;
        _mapper = mapper;
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
                return true;   
            }
            else
            {
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
            Console.WriteLine(e);
            _logger.LogError(e.Message);
            throw;
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
            Console.WriteLine(e);
            throw;
        }
    }
}