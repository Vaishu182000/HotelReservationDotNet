using System;
using AutoMapper;
using HotelBooking.Data;
using HotelBooking.Data.Constants;
using HotelBooking.Data.ViewModels;
using HotelBooking.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelBooking.Services
{
	public class UserService
	{
        private DbInitializer _dbContext;
        public readonly IMapper _mapper;
        private readonly ILogger<UserService> _logger;

        public UserService(DbInitializer dbContext, IMapper mapper, ILogger<UserService> logger)
		{
            _dbContext = dbContext;
            _mapper = mapper;
            _logger = logger;
        }

        public bool UserSignUp(UserVM user)
        {
            if (user == null)
            {
                return false;
            }
            else
            {
                try
                {
                    var _mappedUser = _mapper.Map<User>(user);
                    
                    _dbContext.User.Add(_mappedUser);
                    _dbContext.SaveChanges();
                    _logger.LogInformation(SuccessResponse.UserSignUp);
                    return true;
                }
                catch (Exception exception)
                {
                    _logger.LogError(exception.Message);
                    return false;
                }
            }
        }

        public bool UserLogin(string userEmail, string userPassword)
        {
            try
            {
                User user = GetUserByUserEmail(userEmail);
                if (user.password == userPassword)
                {
                    _logger.LogInformation(SuccessResponse.UserLogin);
                    return true;
                }
                else
                {
                    _logger.LogError(ErrorResponse.ErrorUserLogin);
                    return false;
                }
            }
            catch(Exception e)
            {
                _logger.LogError(e.Message);
                return false;
            }
        }

        public User GetUserByUserEmail(string email)
        {
            var _user = _dbContext.User.SingleOrDefault(u => u.userEmail == email);
            return _user;
        }
	}
}

