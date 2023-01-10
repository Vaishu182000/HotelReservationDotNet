using System;
using HotelBooking.Data;
using HotelBooking.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelBooking.Services
{
	public class UserService
	{
        private DbInitializer _dbContext;

        public UserService(DbInitializer dbContext)
		{
            _dbContext = dbContext;
        }

        public bool UserSignUp(User user)
        {
            if (user == null)
            {
                return false;
            }
            else
            {
                try
                {
                    _dbContext.User.Add(user);
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

