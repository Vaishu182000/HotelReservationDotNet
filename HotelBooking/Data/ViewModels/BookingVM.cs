namespace HotelBooking.Data.ViewModels;

public class BookingVM
{
    public int noOfPersons { get; set; }
    public bool paid { get; set; }

    public DateTime checkInTime { get; set; }
    public DateTime checkOutTime { get; set; }

    public int roomId { get; set; }
    public int hotelId { get; set; }

    public int userId { get; set; }
}

public class CheckRoomAvailability
{
    public int noOfPersons { get; set; }
    public DateTime checkInTime { get; set; }
    public DateTime checkOutTime { get; set; }
    public string hotelName { get; set; }
}