using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Azure.Communication.Email;
using Azure.Communication.Email.Models;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using Microsoft.Identity.Client;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using JsonSerializer = System.Text.Json.JsonSerializer;
using HotelBooking;

namespace HotelBooking.EmailService;

public static class EmailService
{
    [FunctionName("EmailService")]
    public static async Task RunAsync([ServiceBusTrigger("emailservice", Connection = "AzureServiceBusConnectionString")] string myQueueItem, ILogger log)
    {
        log.LogInformation($"C# ServiceBus queue trigger function processed message: {myQueueItem}");
        
        dynamic deserialize = JsonConvert.DeserializeObject(myQueueItem);
        
        string connectionString = Environment.GetEnvironmentVariable("CommunicationServiceConnectionString");
        EmailClient emailClient = new EmailClient(connectionString);
        
        EmailContent emailContent = new EmailContent($"{Constants.BookingEmail}");
        emailContent.PlainText = $"Your Booking is Confirmed! The Hotel Name is {deserialize.room.hotel.hotelName} and Your Room No is {deserialize.room.roomName}. Your Check In Date is {deserialize.checkInTime} and Check Out Date is {deserialize.checkOutTime}";
        List<EmailAddress> emailAddresses = new List<EmailAddress> { new EmailAddress($"{deserialize.user.userEmail}") { DisplayName = "" } };
        EmailRecipients emailRecipients = new EmailRecipients(emailAddresses);
        EmailMessage emailMessage = new EmailMessage($"{Constants.FromEmail}", emailContent, emailRecipients);
        SendEmailResult emailResult = emailClient.Send(emailMessage, CancellationToken.None);
    }
}