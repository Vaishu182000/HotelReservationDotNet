namespace HotelBooking.Data.ViewModels;

public class BookingVM
{
    public int noOfPersons { get; set; }
    public bool paid { get; set; }
    
    public string roomName { get; set; }
    
    public string userEmail { get; set; }
}