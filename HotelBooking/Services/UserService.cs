using System;
using System.IdentityModel.Tokens.Jwt;
using AutoMapper;
using HotelBooking.Data;
using HotelBooking.Data.Constants;
using HotelBooking.Data.ViewModels;
using HotelBooking.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Text;
using Azure.Communication.Email;
using Azure.Communication.Email.Models;
using Microsoft.IdentityModel.Tokens;

namespace HotelBooking.Services
{
	public class UserService
	{
        private DbInitializer _dbContext;
        public readonly IMapper _mapper;
        private readonly ILogger<UserService> _logger;
        public IConfiguration _configuration;

        public UserService(DbInitializer dbContext, IMapper mapper, ILogger<UserService> logger, IConfiguration config)
		{
            _dbContext = dbContext;
            _mapper = mapper;
            _logger = logger;
            _configuration = config;
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
                    if (GetUserByUserEmail(user.userEmail) == null)
                    {
                        var _mappedUser = _mapper.Map<User>(user);
                        var encData_byte = System.Text.Encoding.UTF8.GetBytes(_mappedUser.password);
                        _mappedUser.password = Convert.ToBase64String(encData_byte);
                    
                        _dbContext.User.Add(_mappedUser);
                        _dbContext.SaveChanges();
                        _logger.LogInformation(SuccessResponse.UserSignUp);
                        
                        string connectionString = _configuration["communicationService"];
                        EmailClient emailClient = new EmailClient(connectionString);
                        
                        EmailContent emailContent = new EmailContent("Hotel Reservation User Sign Up");
                        emailContent.PlainText = "User Sign Up Successful! You Can proceed to Login";
                        List<EmailAddress> emailAddresses = new List<EmailAddress> { new EmailAddress(_mappedUser.userEmail) { DisplayName = "Friendly Display Name" }};
                        EmailRecipients emailRecipients = new EmailRecipients(emailAddresses);
                        EmailMessage emailMessage = new EmailMessage(_configuration["communicationService:FromEmail"], emailContent, emailRecipients);
                        SendEmailResult emailResult = emailClient.Send(emailMessage,CancellationToken.None);
                        
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

        public bool changePassword(UserPasswordVM userPasswordVm)
        {
            try
            {
                User user = GetUserByUserEmail(userPasswordVm.userEmail);

                if (user != null)
                {
                    var encData_byte = System.Text.Encoding.UTF8.GetBytes(userPasswordVm.password);
                    userPasswordVm.password = Convert.ToBase64String(encData_byte);
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

        public string UserLogin(UserLoginVM userLoginVm)
        {
            try
            {
                User user = GetUserByUserEmail(userLoginVm.userEmail);
                
                var encData_byte = System.Text.Encoding.UTF8.GetBytes(userLoginVm.password);
                userLoginVm.password = Convert.ToBase64String(encData_byte);
                
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
                    
                    return new JwtSecurityTokenHandler().WriteToken(token);
                }
                else
                {
                    _logger.LogError(ErrorResponse.ErrorUserLogin);
                    return null;
                }
            }
            catch(Exception e)
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

