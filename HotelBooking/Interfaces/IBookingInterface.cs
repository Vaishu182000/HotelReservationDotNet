using HotelBooking.Data.ViewModels;
using HotelBooking.Models;

namespace HotelBooking.Interfaces;

public interface IBookingInterface
{
    public bool CreateBooking(BookingVM booking);
    public IQueryable<Booking> BookingHistory(string userEmail);
    public bool deleteBooking(int id);
}