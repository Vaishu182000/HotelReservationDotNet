using System;
using HotelBooking.Models;

namespace HotelBooking.Data.ViewModels
{
	public class HotelVM
	{
        public string hotelName { get; set; }
        public int noOfRooms { get; set; }

        public int locationId { get; set; }
    }
}

