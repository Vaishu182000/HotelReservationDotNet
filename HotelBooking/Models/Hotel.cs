using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelBooking.Models
{
	public class Hotel
	{
		[Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int hotel_id { get; set; }
		public string hotel_name { get; set; }
		public int no_of_rooms { get; set; }

		public int LocationId { get; set; }
		public Location location { get; set; }

		public ICollection<Room> rooms { get; set; }
	}
}

