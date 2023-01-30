namespace HotelBooking.Data.Constants;

public static class SuccessResponse
{
    public const string UserSignUp = "User Signed Up Successfully";
    public const string UserLogin = "User Login Successfull! You can find the Available Locations";
    public const string UserForgotPassword = "User Password Updated Successfully";

    public const string AddLocation = "Location Added Successfully";
    public const string GetLocations = "The Available Locations";

    public const string AddHotel = "Hotel Added Successfully";
    public const string HotelListBasedOnLocation = "Retreived Hotel List based on Location";
    public const string GetHotel = "Retreived Hotel List Successfully";

    public const string AddRoom = "Created Room Successfully";
    public const string RoomListByHotel = "Retreived Room list based on Hotel";
    public const string CheckRoomAvailability = "Availability Checked Successfully";
    public const string RoomBlob = "Image Uploaded to Blob Successfully";

    public const string AddBooking = "Created Booking Successfully! An Email has been sent with Detailed Information";
    public const string BookingHistoryOfUser = "Retreived Booking History of the User";
    public const string CancelBooking = "Cancellation Successfull";
}