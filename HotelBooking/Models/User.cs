using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelBooking.Models
{
	public class User
	{
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int user_id { get; set; }
		public string user_name { get; set; }
		public string user_email { get; set; }
		public string phone { get; set; }

		public int BookingId { get; set; }
		public Booking booking { get; set; }
	}
}

