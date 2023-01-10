using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelBooking.Models
{
	public class Location
	{
		public string location { get; set; }

		[Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int locationId { get; set; }
	}
}

