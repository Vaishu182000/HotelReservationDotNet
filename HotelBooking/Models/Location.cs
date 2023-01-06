using System;
using System.ComponentModel.DataAnnotations;

namespace HotelBooking.Models
{
	public class Location
	{
		public string location { get; set; }

		[Key]
		public int location_id { get; set; }
	}
}

