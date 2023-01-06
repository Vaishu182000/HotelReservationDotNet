using System;
namespace HotelBooking.Models
{
	public class Booking
	{
		public int booking_id { get; set; }
		public int no_of_persons { get; set; }
		public bool paid { get; set; }
	}
}

