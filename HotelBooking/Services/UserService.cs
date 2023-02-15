using System;
using System.IdentityModel.Tokens.Jwt;
using AutoMapper;
using HotelBooking.Data;
using HotelBooking.Data.Constants;
using HotelBooking.Data.ViewModels;
using HotelBooking.Models;
using System.Security.Claims;
using System.Text;
using HotelBooking.Helpers;
using HotelBooking.Interfaces;
using Microsoft.IdentityModel.Tokens;
using HotelBooking.Email;

namespace HotelBooking.Services
{
    public class UserService : IUserService
    {
        private DbInitializer _dbContext;
        public readonly IMapper _mapper;
        private readonly ILogger<UserService> _logger;
        public IConfiguration _configuration;
        private EncryptHelper _encryptHelper;
        private ISendEmail _email;

        public UserService(DbInitializer dbContext, IMapper mapper, ILogger<UserService> logger, IConfiguration config, EncryptHelper encryptHelper, ISendEmail email)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _logger = logger;
            _configuration = config;
            _encryptHelper = encryptHelper;
            _email = email;
        }
        public UserService() {}

        public virtual bool UserSignUp(UserVM user)
        {
            if (user == null)
            {
                return false;
            }
            else
            {
                try
                {
                    if (GetUserByUserEmail(user.userEmail) == null)
                    {
                        var _mappedUser = _mapper.Map<User>(user);
                        _mappedUser.password = _encryptHelper.passwordEncryptor(_mappedUser.password);

                        _dbContext.User.Add(_mappedUser);
                        _dbContext.SaveChanges();
                        _logger.LogInformation(SuccessResponse.UserSignUp);
                        
                        return true;
                    }
                    else
                    {
                        _logger.LogError("User Email already Present");
                        return false;
                    }
                }
                catch (Exception exception)
                {
                    _logger.LogError(exception.Message);
                    return false;
                }
            }
        }

        public virtual bool changePassword(UserPasswordVM userPasswordVm)
        {
            try
            {
                User user = GetUserByUserEmail(userPasswordVm.userEmail);

                if (user != null)
                {
                    userPasswordVm.password = _encryptHelper.passwordEncryptor(userPasswordVm.password);
                    user.password = userPasswordVm.password;

                    _dbContext.User.Update(user);
                    _dbContext.SaveChanges();

                    _logger.LogInformation(SuccessResponse.UserForgotPassword);
                    return true;
                }
                else
                {
                    _logger.LogError(ErrorResponse.ErrorUserForgotPassword);
                    return false;
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return false;
            }
        }

        public virtual string UserLogin(UserLoginVM userLoginVm)
        {
            try
            {
                User user = GetUserByUserEmail(userLoginVm.userEmail);

                userLoginVm.password = _encryptHelper.passwordEncryptor(userLoginVm.password);

                if (user.password == userLoginVm.password)
                {
                    _logger.LogInformation(SuccessResponse.UserLogin);

                    var claims = new[] {
                        new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                        new Claim("UserId", user.userId.ToString()),
                        new Claim("UserName", user.userName),
                        new Claim("UserEmail", user.userEmail),
                        new Claim("Password", user.password)
                    };

                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                    var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                    var token = new JwtSecurityToken(
                        _configuration["Jwt:Issuer"],
                        _configuration["Jwt:Audience"],
                        claims,
                        expires: DateTime.UtcNow.AddMinutes(10),
                        signingCredentials: signIn);

                    _email.Send();
                    return new JwtSecurityTokenHandler().WriteToken(token);
                }
                else
                {
                    _logger.LogError(ErrorResponse.ErrorUserLogin);
                    return null;
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return null;
            }
        }

        public User GetUserByUserEmail(string email)
        {
            var _user = _dbContext.User.SingleOrDefault(u => u.userEmail == email);
            return _user;
        }
    }
}

