using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelBooking.Models
{
	public class Booking
	{
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int bookingId { get; set; }
		public int noOfPersons { get; set; }
		public bool paid { get; set; }
		
		public DateTime checkInTime { get; set; }
		public DateTime checkOutTime { get; set; }

		public int RoomId { get; set; }
		public Room room { get; set; }

		public int UserId { get; set; }
		public User user { get; set; }
	}
}

