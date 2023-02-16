using Amazon.SimpleEmail;
using Amazon.SimpleEmail.Model;
using HotelBooking.Email.Constants;

namespace HotelBooking.Email;

public class SendEmail : ISendEmail
{
    async public void Send(string awsAccessKey, string awsSecretKey, string awsSessionToken)
        {
            Console.WriteLine("Inside Email Service");
            using (var client = new AmazonSimpleEmailServiceClient(awsAccessKey, awsSecretKey, awsSessionToken))
            {
                // Create a send email message request
            
                var dest = new Destination(new List<string> { EmailConstants.TO });
                var subject = new Content(EmailConstants.SUBJECT);
                var textBody = new Content(EmailConstants.BODY);
                var body = new Body(textBody);
                var message = new Message(subject, body);
                var request = new SendEmailRequest(EmailConstants.FROM, dest, message);
            
                // Send the email to recipients via Amazon SES
                Console.WriteLine($"Sending message to {EmailConstants.FROM}");
                var response = await client.SendEmailAsync(request);
                Console.WriteLine($"Response - HttpStatusCode: {response.HttpStatusCode}, MessageId: {response.MessageId}");
            }
        }
}