using HotelBooking.Data;
using HotelBooking.Data.ViewModels;
using HotelBooking.Models;

namespace HotelBooking.Services;

public class AvailabilityService
{
    private DbInitializer _dbContext;

    public AvailabilityService(DbInitializer dbContext)
    {
        _dbContext = dbContext;
    }

    public bool CreateAvailability(AvailabilityVM availability)
    {
        try
        {
            var _availabilty = new Availability()
            {
                checkInTime = availability.checkInTime,
                checkOutTime = availability.checkOutTime,
                BookingId = availability.BookingId
            };

            _dbContext.Availability.Add(_availabilty);
            _dbContext.SaveChanges();
            return true;
        }
        catch
        {
            return false;
        }
    }
}