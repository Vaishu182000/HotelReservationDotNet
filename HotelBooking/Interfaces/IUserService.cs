using HotelBooking.Data.ViewModels;
using HotelBooking.Models;

namespace HotelBooking.Interfaces;

public interface IUserService
{
    public bool UserSignUp(UserVM userVm);
    public bool changePassword(UserPasswordVM userPasswordVm);
    public string UserLogin(UserLoginVM userLoginVm);
    public User GetUserByUserEmail(string email);
}