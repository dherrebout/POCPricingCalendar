using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PricingCalendar.Models;

namespace PricingCalendar.Data
{
    public class PricingCalendarContext : DbContext
    {
        public PricingCalendarContext (DbContextOptions<PricingCalendarContext> options)
            : base(options)
        {
        }

        public DbSet<PricingCalendar.Models.Rental> Rentals { get; set; }
        public DbSet<PricingCalendar.Models.RentalPeriod> RentalPeriods { get; set; }
        public DbSet<PricingCalendar.Models.RentalPeriodPricingCalendarWindow> RentalPeriodPricingCalendarWindows { get; set; }
        public DbSet<PricingCalendar.Models.PricingCalendarEntry> PricingCalendarEntries { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Rental>().ToTable("Rentals");
            modelBuilder.Entity<RentalPeriod>().ToTable("RentalPeriods");
            modelBuilder.Entity<RentalPeriodPricingCalendarWindow>().ToTable("RentalPeriodPricingCalendarWindows");
            modelBuilder.Entity<PricingCalendarEntry>().ToTable("PricingCalendarEntries");
        }
    }
}
