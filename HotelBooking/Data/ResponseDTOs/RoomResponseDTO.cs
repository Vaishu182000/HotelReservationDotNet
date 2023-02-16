using System;
using System.Linq;
using HotelBooking.Models;

namespace HotelBooking.Data;

public class RoomResponseDTO
{
    public string message { get; set; }
    public IQueryable<Room> roomList { get; set; }

    protected bool Equals(RoomResponseDTO other)
    {
        return message == other.message && roomList.Equals(other.roomList);
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((RoomResponseDTO)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(message, roomList);
    }
}