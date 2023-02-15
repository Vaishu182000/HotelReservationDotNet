using Amazon.SimpleEmail;
using Amazon.SimpleEmail.Model;

namespace HotelBooking.Email;

public class SendEmail : ISendEmail
{
    async public void Send()
        {
            var region = "us-east-1";
                        
            string source = "vaishnavis@presidio.com";

// Get recipient, access key, and secret key from the command line

            string recipient = "svaish2000@gmail.com";
            var awsAccessKey = "";
            var awsSecretKey = "";
            var awsSessionToken =
                "";

// Send the email to the recipients via Amazon SES.

            using (var client = new AmazonSimpleEmailServiceClient(awsAccessKey, awsSecretKey, awsSessionToken))
            {
                // Create a send email message request

                var dest = new Destination(new List<string> { recipient });
                var subject = new Content("Testing Amazon SES through the API");
                var textBody = new Content("This is a test message sent by Amazon SES from the AWS SDK for .NET.");
                var body = new Body(textBody);
                var message = new Message(subject, body);
                var request = new SendEmailRequest(source, dest, message);

                // Send the email to recipients via Amazon SES
                Console.WriteLine($"Sending message to {source}");
                var response = await client.SendEmailAsync(request);
                Console.WriteLine($"Response - HttpStatusCode: {response.HttpStatusCode}, MessageId: {response.MessageId}");
            }
        }
}