using CarRental.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;

namespace CarRental.Data
{
    public class CarRentalContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Car> Cars { get; set; }

        protected override void OnModelCreating (ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Car>(entity => entity.HasIndex(e => e.RegistrationNo).IsUnique());

            modelBuilder.Entity<Booking>()
                .HasIndex(b => new { b.CarId, b.StartTime, b.EndTime })
                .IsUnique();

            modelBuilder.Entity<Customer>()
                .Property(c => c.Email)
                .IsRequired();
            modelBuilder.Entity<Customer>()
                .Property(c => c.FirstName)
                .IsRequired();
            modelBuilder.Entity<Customer>()
                .Property(c => c.LastName)
                .IsRequired();
            modelBuilder.Entity<Customer>()
                .HasIndex(b => b.Email)
                .IsUnique();
            modelBuilder.Entity<Booking>()
                .Property(c => c.CarInGarage)
                .HasDefaultValue(true);
            modelBuilder.Entity<Car>()
                .Property(c => c.AvailableForBooking)
                .HasDefaultValue(true);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConfigurationManager.ConnectionStrings["CarRentalDB"].ConnectionString);
        }
    }
}
