using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelBooking.Models
{
	public class Room
	{
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int room_id { get; set; }
		public string room_name { get; set; }
		public float room_rate { get; set; }
		public int room_capacity { get; set; }

		public int HotelId { get; set; }
		public Hotel hotel { get; set; }

		public ICollection<Booking> bookings { get; set; }
	}
}

