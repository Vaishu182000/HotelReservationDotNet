using HotelBooking.Data.ViewModels;
using HotelBooking.Models;

namespace HotelBooking.Interfaces;

public interface IHotelService
{
    public bool AddHotel(HotelVM hotel);
    public List<string> HotelListByLocation(string location);
    public Hotel GetHotelByHotelName(string hotel);
    public List<Hotel> GetAllHotels();
}