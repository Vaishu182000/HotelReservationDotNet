namespace HotelBooking.Email;

public interface ISendEmail
{
    public void Send(string awsAccessKey, string awsSecretKey, string awsSessionToken);
}