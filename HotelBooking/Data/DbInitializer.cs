using System;
using HotelBooking.Models;
using HotelBooking.SecretManager.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace HotelBooking.Data
{
    public class DbInitializer : DbContext
    {
        private readonly IConfigSettings _configSettings;
        public DbInitializer(DbContextOptions<DbInitializer> options, IConfigSettings configSettings) : base(options)
        {
            _configSettings = configSettings;
        }

        public DbInitializer(){}

        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<Location> Location { get; set; }
        public virtual DbSet<Hotel> Hotel { get; set; }
        public virtual DbSet<Room> Room { get; set; }
        public virtual DbSet<Booking> Booking { get; set; }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_configSettings.DbSecret);
        }
    }
}

