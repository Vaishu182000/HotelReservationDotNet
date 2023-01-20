
namespace HotelBooking.Data.ViewModels;

public class UserVM
{
    public string userName { get; set; }
    public string userEmail { get; set; }
    public string phone { get; set; }
    public string password { get; set; }
}

public class UserLoginVM
{
    public string userEmail { get; set; }
    public string password { get; set; }
}

public class UserPasswordVM
{
    public string userName { get; set; }
    public string userEmail { get; set; }
    public string password { get; set; }
}