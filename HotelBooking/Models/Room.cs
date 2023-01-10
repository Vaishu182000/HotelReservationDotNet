using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelBooking.Models
{
	public class Room
	{
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int roomId { get; set; }
		public string roomName { get; set; }
		public float roomRate { get; set; }
		public int roomCapacity { get; set; }

		public int HotelId { get; set; }
		[ForeignKey("HotelId")]
		public virtual Hotel hotel { get; set; }
	}
}

