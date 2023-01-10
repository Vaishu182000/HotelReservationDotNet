using HotelBooking.Data;
using HotelBooking.Data.ViewModels;
using HotelBooking.Models;

namespace HotelBooking.Services;

public class BookingService
{
    private DbInitializer _dbContext;
    private RoomService _roomService;
    private UserService _userService;

    public BookingService(DbInitializer dbContext, RoomService roomService, UserService userService)
    {
        _dbContext = dbContext;
        _roomService = roomService;
        _userService = userService;
    }

    public bool CreateBooking(BookingVM booking)
    {
        try
        {
            var _room = _roomService.GetRoomByRoomName(booking.roomName);
            var _user = _userService.GetUserByUserEmail(booking.userEmail);

            var _booking = new Booking()
            {
                noOfPersons = booking.noOfPersons,
                paid = booking.paid,
                RoomId = _room.roomId,
                UserId = _user.userId
            };

            _dbContext.Booking.Add(_booking);
            _dbContext.SaveChanges();
            return true;
        }
        catch
        {
            return false;
        }
    }
}