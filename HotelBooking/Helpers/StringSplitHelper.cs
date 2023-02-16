using System;

namespace HotelBooking.Helpers;

public class StringSplitHelper
{
    public string[] splitDate(DateTime date)
    {
        var value = date.ToString("dd/MM/yyyy").Split('/');
        return value;
    }
}