using AutoMapper;
using HotelBooking.Data;
using HotelBooking.Data.ViewModels;
using HotelBooking.Email;
using HotelBooking.Helpers;
using HotelBooking.Models;
using HotelBooking.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;

namespace HotelBooking.UnitTest.Services;

public class UserServiceIsExecuting
{
    private readonly Mock<IMapper> _mapper;
    private readonly Mock<ILogger<UserService>> _logger;
    private readonly Mock<DbSet<User>> _mock;
    private readonly Mock<DbInitializer> _mockContext;
    private readonly Mock<IConfiguration> _config;
    private readonly Mock<EncryptHelper> _encrypt;
    private readonly Mock<ISendEmail> _email;

    public UserServiceIsExecuting()
    {
        _mapper = new Mock<IMapper>();
        _logger = new Mock<ILogger<UserService>>();
        _mock = new Mock<DbSet<User>>();
        _mockContext = new Mock<DbInitializer>();
        _config = new Mock<IConfiguration>();
        _encrypt = new Mock<EncryptHelper>();
        _email = new Mock<ISendEmail>();
    }

    [Fact]
    public void UserSignUp()
    {
        var userList = AddUsers();
        var usersList = GetUsers();
        
        _mock.As<IQueryable<User>>().Setup(m => m.Provider).Returns(usersList.Provider);
        _mock.As<IQueryable<User>>().Setup(m => m.Expression).Returns(usersList.Expression);
        _mock.As<IQueryable<User>>().Setup(m => m.ElementType).Returns(usersList.ElementType);
        _mock.As<IQueryable<User>>().Setup(m => m.GetEnumerator()).Returns(() => usersList.GetEnumerator());
        
        _mockContext.Setup(m => m.User).Returns(_mock.Object);

        var service = new UserService(_mockContext.Object, _mapper.Object, _logger.Object, _config.Object,
            _encrypt.Object, _email.Object);
        var actual = service.UserSignUp(userList[0]);
        
        // _mock.Verify(m => m.Add(It.IsAny<User>()), Times.Once());
        // _mockContext.Verify(m => m.SaveChanges(), Times.Once());
        
        Assert.Equal(false, actual);
    }

    [Fact]
    public void GetUserByUserEmail()
    {
        var userList = GetUsers();
        
        _mock.As<IQueryable<User>>().Setup(m => m.Provider).Returns(userList.Provider);
        _mock.As<IQueryable<User>>().Setup(m => m.Expression).Returns(userList.Expression);
        _mock.As<IQueryable<User>>().Setup(m => m.ElementType).Returns(userList.ElementType);
        _mock.As<IQueryable<User>>().Setup(m => m.GetEnumerator()).Returns(() => userList.GetEnumerator());

        _mockContext.Setup(c => c.User).Returns(_mock.Object);

        var service = new UserService(_mockContext.Object, _mapper.Object, _logger.Object, _config.Object,
            _encrypt.Object, _email.Object);
        var user = service.GetUserByUserEmail("vaishnavis@presidio.com");
        
        Assert.Equal(1, user.userId);
    }

    private List<UserVM> AddUsers()
    {
        var userList = new List<UserVM>
        {
            new UserVM()
            {
                userName = "Ajay R",
                userEmail = "ajayr@presidio.com",
                phone = "9500977257",
                password = "Ajay*123"
            },
        };
        return userList;
    }

    private IQueryable<User> GetUsers()
    {
        var userList = new List<User>
        {
            new User()
            {
                userId = 1,
                userEmail = "vaishnavis@presidio.com",
                userName = "Vaishnavi S",
                phone = "9500977257",
                password = "Vaish*123"
            },
            new User()
            {
                userId = 2,
                userEmail = "sridhars@presidio.com",
                userName = "Sridhar S",
                phone = "9500977257",
                password = "Sridhar*123"
            },
        }.AsQueryable();
        return userList;
    }
}