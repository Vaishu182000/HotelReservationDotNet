using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelBooking.Models
{
    public class Hotel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int hotelId { get; set; }
        public string hotelName { get; set; }
        public int noOfRooms { get; set; }

        public int LocationId { get; set; }
        [ForeignKey("LocationId")]
        public virtual Location location { get; set; }
    }
}

