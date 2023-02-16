using System.Collections.Generic;
using HotelBooking.Controllers;
using HotelBooking.Data;
using HotelBooking.Data.Constants;
using HotelBooking.Data.ViewModels;
using HotelBooking.Interfaces;
using HotelBooking.Models;
using HotelBooking.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace HotelBooking.UnitTest;

public class UserControllerIsExecuting
{
    private readonly Mock<IUserService> userService;
    private readonly Mock<ILocationService> locationService;
    private readonly Mock<ILogger<UserController>> logger;

    public UserControllerIsExecuting()
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

        userService.Setup(x => x.UserSignUp(userList[0]))
            .Returns(true);
        var userController = new UserController(userService.Object, locationService.Object, logger.Object);
        
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

        userService.Setup(x => x.changePassword(userList[0]))
            .Returns(true);
        var userController = new UserController(userService.Object, locationService.Object, logger.Object);

        //act
        var userResult = userController.ForgotPassword(userList[0]);
        var OkUserResult = userResult as OkObjectResult;
        
        //assert
        Assert.Equal("User Password Updated Successfully", OkUserResult.Value);
    }

    [Fact]
    public void UserLogin()
    {
        //arrange
        var userList = GetUserData();
        var locationList = GetLocations();

        userService.Setup(x => x.UserLogin(userList[0])).Returns("abcd");
        locationService.Setup(x => x.GetLocations()).Returns(locationList);

        UserResponseDTO responseDto = new UserResponseDTO();
        responseDto.UserLogin = SuccessResponse.UserLogin;
        responseDto.jwt = "abcd";
        responseDto.Locations = locationList;
        
        var userController = new UserController(userService.Object, locationService.Object, logger.Object);
        
        //act
        var userResult = userController.UserLogin(userList[0]);
        var okUserResult = userResult as OkObjectResult;
        var result = okUserResult.Value as UserResponseDTO;
        
        //assert
        Assert.Equal(responseDto, result);
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

    private List<Location> GetLocations()
    {
        return new List<Location>
        {
            new Location()
            {
                location = "Salem",
                locationId = 1
            },
            new Location()
            {
                location = "Coimbatore",
                locationId = 2
            },
        };
    }
}