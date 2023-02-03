using HotelBooking.Controllers;
using HotelBooking.Data.ViewModels;
using HotelBooking.Interfaces;
using HotelBooking.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace HotelBooking.UnitTest;

public class UserUnitTest
{
    private readonly Mock<IUserService> userService;
    private readonly Mock<ILocationService> locationService;
    private readonly Mock<ILogger<UserController>> logger;

    public UserUnitTest()
    {
        userService = new Mock<IUserService>();
        locationService = new Mock<ILocationService>();
        logger = new Mock<ILogger<UserController>>();
    }
    
    [Fact]
    public void AddUsers_UserSignUp()
    {
        // arrange
        var userList = AddUsersData();
        
        Mock<UserService> userServiceSubject = new Mock<UserService>();
        userServiceSubject.CallBase = true;
        Mock<LocationService> locationServiceSubject = new Mock<LocationService>();
        locationServiceSubject.CallBase = true;

        userService.Setup(x => x.UserSignUp(userList[0])).Returns(true);
        userServiceSubject.Setup(x => x.UserSignUp(userList[0]))
            .Returns(true);
        var userController = new UserController(userServiceSubject.Object, locationServiceSubject.Object, logger.Object);
        
        //act
        var userResult = userController.UserSignUp(userList[0]);
        var OkUserResult = userResult as OkObjectResult;
        
        //assert
        Assert.Equal("User Signed Up Successfully", OkUserResult.Value);
    }

    [Fact]
    public void UpdateUser_ForgotPassword()
    {
        //arrange
        var userList = UpdateUserData();
        
        Mock<UserService> userServiceSubject = new Mock<UserService>();
        userServiceSubject.CallBase = true;
        Mock<LocationService> locationServiceSubject = new Mock<LocationService>();
        locationServiceSubject.CallBase = true;

        userServiceSubject.Setup(x => x.changePassword(userList[0]))
            .Returns(true);
        var userController = new UserController(userServiceSubject.Object, locationServiceSubject.Object, logger.Object);

        //act
        var userResult = userController.ForgotPassword(userList[0]);
        var OkUserResult = userResult as OkObjectResult;
        
        //assert
        Assert.Equal("User Password Updated Successfully", OkUserResult.Value);
    }
    
    private List<UserVM> AddUsersData()
    {
        List<UserVM> usersData = new List<UserVM>
        {
            new UserVM()
            {
                userName = "Sridhar",
                userEmail = "sridhars@presidio.com",
                phone = "9500977257",
                password = "Sridhar*123"
            },
        };
        return usersData;
    }

    private List<UserPasswordVM> UpdateUserData()
    {
        List<UserPasswordVM> usersData = new List<UserPasswordVM>
        {
            new UserPasswordVM()
            {
                userName = "Sridhar",
                userEmail = "sridhars@presidio.com",
                password = "Sridhar*123"
            },
        };
        return usersData;
    }

    private List<UserLoginVM> GetUserData()
    {
        List<UserLoginVM> userData = new List<UserLoginVM>
        {
            new UserLoginVM()
            {
                userEmail = "sridhars@presidio.com",
                password = "Sridhar*123"
            },
        };
        return userData;
    }
}