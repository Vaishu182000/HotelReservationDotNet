using System;
using System.ComponentModel.DataAnnotations;

namespace HotelBooking.Models
{
	public class Hotel
	{
		[Key]
		public int hotel_id { get; set; }
		public string hotel_name { get; set; }
		public int no_of_rooms { get; set; }
	}
}

