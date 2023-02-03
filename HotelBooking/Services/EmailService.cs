using Azure.Communication.Email;
using Azure.Communication.Email.Models;
using HotelBooking.Data.Constants;
using HotelBooking.Data.ViewModels;
using HotelBooking.Models;

namespace HotelBooking.Services;

public class EmailService
{
    public IConfiguration _configuration;
    private readonly ILogger<EmailService> _logger;
    public EmailClient emailClient;
    public string connectionString;

    public EmailService(IConfiguration configuration, ILogger<EmailService> logger)
    {
        _configuration = configuration;
        _logger = logger;
        connectionString = _configuration["communicationService"];
    }
    
    public bool bookingEmail(Hotel hotel, Room room, BookingVM booking, User user)
    {
        try
        {
            emailClient = new EmailClient(connectionString);
            EmailContent emailContent = new EmailContent(SuccessResponse.BookingEmail);
            emailContent.PlainText = $"Your Booking is Confirmed! The Hotel Name is {hotel.hotelName} and Your Room No is {room.roomName}. Your Check In Date is {booking.checkInTime} and Check Out Date is {booking.checkOutTime}";
            List<EmailAddress> emailAddresses = new List<EmailAddress> { new EmailAddress(user.userEmail) { DisplayName = "" } };
            EmailRecipients emailRecipients = new EmailRecipients(emailAddresses);
            EmailMessage emailMessage = new EmailMessage(_configuration["communicationService:FromEmail"], emailContent, emailRecipients);
            SendEmailResult emailResult = emailClient.Send(emailMessage, CancellationToken.None);
            
            _logger.LogInformation(SuccessResponse.SuccessEmail);
            return true;
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return false;
        }
    }

    public bool userEmail(User user)
    {
        try
        {
            emailClient = new EmailClient(connectionString);

            EmailContent emailContent = new EmailContent(SuccessResponse.UserSignUp);
            emailContent.PlainText = SuccessResponse.UserSignUpEmailBody;
            List<EmailAddress> emailAddresses = new List<EmailAddress> { new EmailAddress(user.userEmail) { DisplayName = "" } };
            EmailRecipients emailRecipients = new EmailRecipients(emailAddresses);
            EmailMessage emailMessage = new EmailMessage(_configuration["communicationService:FromEmail"], emailContent, emailRecipients);
            SendEmailResult emailResult = emailClient.Send(emailMessage, CancellationToken.None);
            
            _logger.LogInformation(SuccessResponse.UserSignUp);
            return true;
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return false;
        }
    }
}