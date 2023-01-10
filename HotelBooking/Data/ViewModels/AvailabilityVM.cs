namespace HotelBooking.Data.ViewModels;

public class AvailabilityVM
{
    public DateTime checkInTime { get; set; }
    public DateTime checkOutTime { get; set; }

    public int BookingId { get; set; }
}