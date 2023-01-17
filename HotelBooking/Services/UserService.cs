using System;
using AutoMapper;
using HotelBooking.Data;
using HotelBooking.Data.ViewModels;
using HotelBooking.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelBooking.Services
{
	public class UserService
	{
        private DbInitializer _dbContext;
        public readonly IMapper _mapper;

        public UserService(DbInitializer dbContext, IMapper mapper)
		{
            _dbContext = dbContext;
            _mapper = mapper;
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
                    return true;
                }
                catch (Exception exception)
                {
                    return false;
                }
            }
        }

        public bool UserLogin(string userEmail, string userPassword)
        {
            try
            {
                User user = GetUserByUserEmail(userEmail);
                if (user.password == userPassword) return true;
                else return false;
            }
            catch
            {
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

