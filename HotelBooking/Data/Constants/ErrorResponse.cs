namespace HotelBooking.Data.Constants;

public static class ErrorResponse
{
    public const string ErrorUserSignUp = "Error in Signing Up the User";
    public const string ErrorUserLogin = "Error in Login in User";
    
    public const string ErrorAddLocation = "Error in Adding Location";
    public const string ErrorGetLocations = "Error in retrieving the Location";
    
    public const string ErrorAddHotel = "Error in Adding Hotel";
    public const string ErrorInGetHotelBasedOnLocation = "Error in Retriving Hotel List based on Location";
    public const string ErrorGetHotels = "Error in Retriving Hotel List Successfully";

    public const string ErrorAddRoom = "Error in Creating Room";
    public const string ErrorViewRoomDetails = "Error in viewing the Room details";
    public const string ErrorAvailabilityCheck = "Error in checking Availability of Room";

    public const string ErrorAddBooking = "Error in Creating Booking";
    public const string ErrorBookingHistoryOfUser = "Error in Retriving Booking History of User";
    public const string ErrorCancelBooking = "Error in Cancelling the Booking";
}