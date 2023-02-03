namespace HotelBooking.Helpers;

public class EncryptHelper
{
    public string passwordEncryptor(string password)
    {
        var encData_byte = System.Text.Encoding.UTF8.GetBytes(password);
        password = Convert.ToBase64String(encData_byte);
        return password;
    }
}