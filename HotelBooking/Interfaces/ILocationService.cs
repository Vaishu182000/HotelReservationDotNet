using System.Collections.Generic;
using HotelBooking.Data.ViewModels;
using HotelBooking.Models;

namespace HotelBooking.Interfaces;

public interface ILocationService
{
    public bool CreateLocation(LocationVM location);
    public List<Location> GetLocations();
    public Location GetLocationByLocationName(string location);
}