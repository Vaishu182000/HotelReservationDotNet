using System.Text;
using System.Text.Json;
using HotelBooking.Models;

namespace HotelBooking.Services;
using Microsoft.Azure.ServiceBus;

public class ServiceBusService
{
    private readonly IConfiguration _configuration;

    public ServiceBusService(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    
    public async Task SendMessageAsync(Booking booking)
    {
        IQueueClient client = new QueueClient(_configuration["AzureServiceBusConnectionString"], _configuration["QueueName"]);

        //Serialize car details object
        var messageBody = JsonSerializer.Serialize(booking);

        //Set content type and Guid
        var message = new Message(Encoding.UTF8.GetBytes(messageBody))
        {
            MessageId = Guid.NewGuid().ToString(),
            ContentType = "application/json"
        };

        await client.SendAsync(message);
    }

}