using System;
using System.Linq;
using HotelBooking.Models;

namespace HotelBooking.Data;

public class BookingResponseDTO
{
    public string message { get; set; }
    public IQueryable<Booking> bookingList { get; set; }

    protected bool Equals(BookingResponseDTO other)
    {
        return message == other.message && bookingList.Equals(other.bookingList);
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((BookingResponseDTO)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(message, bookingList);
    }
}