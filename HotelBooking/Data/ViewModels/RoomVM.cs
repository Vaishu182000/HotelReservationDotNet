namespace HotelBooking.Data.ViewModels;

public class RoomVM
{
    public string roomName { get; set; }
    public float roomRate { get; set; }
    public int roomCapacity { get; set; }
    public IFormFile roomImage { get; set; }

    public int hotelId { get; set; }

}