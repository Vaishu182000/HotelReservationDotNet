﻿using System;
using HotelBooking.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelBooking.Data
{
    public class DbInitializer : DbContext
    {
		public DbInitializer(DbContextOptions<DbInitializer> options) : base(options){ }

        public DbSet<User> User { get; set; }
        public DbSet<Location> Location { get; set; }
        public DbSet<Hotel> Hotel { get; set; }
        public DbSet<Room> Room { get; set; }
        public DbSet<Availability> Availability { get; set; }
        public DbSet<Booking> Booking { get; set; }
    }
}

