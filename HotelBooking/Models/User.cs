using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelBooking.Models
{
	public class User
	{
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int userId { get; set; }
		public string userName { get; set; }
		public string userEmail { get; set; }
		public string phone { get; set; }
		public string password { get; set; }
	}
}

