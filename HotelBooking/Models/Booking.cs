using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelBooking.Models
{
	public class Booking
	{
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int booking_id { get; set; }
		public int no_of_persons { get; set; }
		public bool paid { get; set; }

		public Availability availability { get; set; }

		public int RoomId { get; set; }
		public Room room { get; set; }

		public ICollection<User> users { get; set; }
	}
}

