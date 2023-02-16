using System.Linq;
using HotelBooking.Data.ViewModels;
using HotelBooking.Models;

namespace HotelBooking.Interfaces;

public interface IBookingService
{
    public bool CreateBooking(BookingVM booking);
    public IQueryable<Booking> BookingHistory(string userEmail);
    public bool deleteBooking(int id);
}