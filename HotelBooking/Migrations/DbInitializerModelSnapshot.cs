﻿// <auto-generated />
using System;
using HotelBooking.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace HotelBooking.Migrations
{
    [DbContext(typeof(DbInitializer))]
    partial class DbInitializerModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("HotelBooking.Models.Availability", b =>
                {
                    b.Property<int>("availabilityId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("availabilityId"));

                    b.Property<int>("BookingId")
                        .HasColumnType("int");

                    b.Property<DateTime>("checkInTime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("checkOutTime")
                        .HasColumnType("datetime2");

                    b.HasKey("availabilityId");

                    b.HasIndex("BookingId")
                        .IsUnique();

                    b.ToTable("Availability");
                });

            modelBuilder.Entity("HotelBooking.Models.Booking", b =>
                {
                    b.Property<int>("bookingId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("bookingId"));

                    b.Property<int>("RoomId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("noOfPersons")
                        .HasColumnType("int");

                    b.Property<bool>("paid")
                        .HasColumnType("bit");

                    b.HasKey("bookingId");

                    b.HasIndex("RoomId");

                    b.HasIndex("UserId");

                    b.ToTable("Booking");
                });

            modelBuilder.Entity("HotelBooking.Models.Hotel", b =>
                {
                    b.Property<int>("hotelId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("hotelId"));

                    b.Property<int>("LocationId")
                        .HasColumnType("int");

                    b.Property<string>("hotelName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("noOfRooms")
                        .HasColumnType("int");

                    b.HasKey("hotelId");

                    b.HasIndex("LocationId");

                    b.ToTable("Hotel");
                });

            modelBuilder.Entity("HotelBooking.Models.Location", b =>
                {
                    b.Property<int>("locationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("locationId"));

                    b.Property<string>("location")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("locationId");

                    b.ToTable("Location");
                });

            modelBuilder.Entity("HotelBooking.Models.Room", b =>
                {
                    b.Property<int>("roomId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("roomId"));

                    b.Property<int>("HotelId")
                        .HasColumnType("int");

                    b.Property<int>("roomCapacity")
                        .HasColumnType("int");

                    b.Property<string>("roomName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<float>("roomRate")
                        .HasColumnType("real");

                    b.HasKey("roomId");

                    b.HasIndex("HotelId");

                    b.ToTable("Room");
                });

            modelBuilder.Entity("HotelBooking.Models.User", b =>
                {
                    b.Property<int>("userId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("userId"));

                    b.Property<string>("password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("phone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("userEmail")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("userName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("userId");

                    b.ToTable("User");
                });

            modelBuilder.Entity("HotelBooking.Models.Availability", b =>
                {
                    b.HasOne("HotelBooking.Models.Booking", "booking")
                        .WithOne("availability")
                        .HasForeignKey("HotelBooking.Models.Availability", "BookingId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("booking");
                });

            modelBuilder.Entity("HotelBooking.Models.Booking", b =>
                {
                    b.HasOne("HotelBooking.Models.Room", "room")
                        .WithMany()
                        .HasForeignKey("RoomId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HotelBooking.Models.User", "user")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("room");

                    b.Navigation("user");
                });

            modelBuilder.Entity("HotelBooking.Models.Hotel", b =>
                {
                    b.HasOne("HotelBooking.Models.Location", "location")
                        .WithMany()
                        .HasForeignKey("LocationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("location");
                });

            modelBuilder.Entity("HotelBooking.Models.Room", b =>
                {
                    b.HasOne("HotelBooking.Models.Hotel", "hotel")
                        .WithMany()
                        .HasForeignKey("HotelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("hotel");
                });

            modelBuilder.Entity("HotelBooking.Models.Booking", b =>
                {
                    b.Navigation("availability")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
