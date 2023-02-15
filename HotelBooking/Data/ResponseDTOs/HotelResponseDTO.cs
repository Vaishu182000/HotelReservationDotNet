using HotelBooking.Models;

namespace HotelBooking.Data;

public class HotelResponseDTO
{
    public string HotelListBasedOnLocation { get; set; }
    public List<string> hotels { get; set; }


    protected bool Equals(HotelResponseDTO other)
    {
        return HotelListBasedOnLocation == other.HotelListBasedOnLocation && hotels.Equals(other.hotels);
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((HotelResponseDTO)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(HotelListBasedOnLocation, hotels);
    }
}

public class GetAllHotelsResponseDTO
{
    public string GetHotel{ get; set; }
    public List<Hotel> hotelList { get; set; }

    protected bool Equals(GetAllHotelsResponseDTO other)
    {
        return GetHotel == other.GetHotel && hotelList.Equals(other.hotelList);
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((GetAllHotelsResponseDTO)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(GetHotel, hotelList);
    }
}