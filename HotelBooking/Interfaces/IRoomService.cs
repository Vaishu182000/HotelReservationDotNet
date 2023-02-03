using HotelBooking.Data.ViewModels;
using HotelBooking.Models;

namespace HotelBooking.Interfaces;

public interface IRoomService
{
    public bool createRoom(RoomVM room);
    public IQueryable<Room> GetRoomsByHotelName(string hotelName);
    public IQueryable<Room> getLocationInList(IQueryable<Room> room);
    public Room GetRoomByRoomName(string roomName);
    public List<Room> CheckAvailability(CheckRoomAvailability availability);
}