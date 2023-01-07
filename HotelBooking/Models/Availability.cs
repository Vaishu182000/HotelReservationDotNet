using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelBooking.Models
{
	public class Availability
	{
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int availability_id { get; set; }
        public DateTime checkInTime { get; set; }
        public DateTime checkOutTime { get; set; }

        public int BookingId { get; set; }
        public Booking booking { get; set; }
	}
}

