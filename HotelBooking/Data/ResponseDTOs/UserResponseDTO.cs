using HotelBooking.Models;

namespace HotelBooking.Data;

public class UserResponseDTO
{
    public string UserLogin { get; set; }
    public List<Location> Locations { get; set; }
    public string jwt { get; set; }

    protected bool Equals(UserResponseDTO other)
    {
        return UserLogin == other.UserLogin && Locations.Equals(other.Locations) && jwt == other.jwt;
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((UserResponseDTO)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(UserLogin, Locations, jwt);
    }
}