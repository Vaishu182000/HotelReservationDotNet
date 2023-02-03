using System;
using HotelBooking.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace HotelBooking.Data
{
    public class DbInitializer : DbContext
    {
        public DbInitializer(DbContextOptions<DbInitializer> options) : base(options)
        {
        }

        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<Location> Location { get; set; }
        public virtual DbSet<Hotel> Hotel { get; set; }
        public virtual DbSet<Room> Room { get; set; }
        public virtual DbSet<Booking> Booking { get; set; }
    }
}

