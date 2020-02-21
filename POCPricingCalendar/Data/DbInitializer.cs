using PricingCalendar.Data;
using PricingCalendar.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PricingCalendar.Data
{
    public class DbInitializer
    {
        public static void Initialize(PricingCalendarContext context)
        {
            context.Database.EnsureCreated();

            // Look for any entries
            if (context.PricingCalendarEntries.Any())
            {
                return;   // DB has been seeded
            }

            var entries = new PricingCalendarEntry[]
            {
                new PricingCalendarEntry{ Date=DateTime.Parse("2020-02-01"), Price=37.5m },
                new PricingCalendarEntry{ Date=DateTime.Parse("2020-02-02"), Price=35m },
                new PricingCalendarEntry{ Date=DateTime.Parse("2020-02-03"), Price=32.5m },
                new PricingCalendarEntry{ Date=DateTime.Parse("2020-02-04"), Price=40m },
                new PricingCalendarEntry{ Date=DateTime.Parse("2020-02-05"), Price=42.5m },
                new PricingCalendarEntry{ Date=DateTime.Parse("2020-02-06"), Price=35m },
                new PricingCalendarEntry{ Date=DateTime.Parse("2020-02-07"), Price=30m },
                new PricingCalendarEntry{ Date=DateTime.Parse("2020-02-08"), Price=30m },
                new PricingCalendarEntry{ Date=DateTime.Parse("2020-02-09"), Price=32.5m },
                new PricingCalendarEntry{ Date=DateTime.Parse("2020-02-10"), Price=37.5m },
                new PricingCalendarEntry{ Date=DateTime.Parse("2020-02-11"), Price=37.5m },
                new PricingCalendarEntry{ Date=DateTime.Parse("2020-02-12"), Price=35m },
                new PricingCalendarEntry{ Date=DateTime.Parse("2020-02-13"), Price=32.5m },
                new PricingCalendarEntry{ Date=DateTime.Parse("2020-02-14"), Price=40m },
                new PricingCalendarEntry{ Date=DateTime.Parse("2020-02-15"), Price=42.5m },
                new PricingCalendarEntry{ Date=DateTime.Parse("2020-02-16"), Price=35m },
                new PricingCalendarEntry{ Date=DateTime.Parse("2020-02-17"), Price=30m },
                new PricingCalendarEntry{ Date=DateTime.Parse("2020-02-18"), Price=30m },
                new PricingCalendarEntry{ Date=DateTime.Parse("2020-02-19"), Price=32.5m },
                new PricingCalendarEntry{ Date=DateTime.Parse("2020-02-20"), Price=37.5m },
                new PricingCalendarEntry{ Date=DateTime.Parse("2020-02-21"), Price=37.5m },
                new PricingCalendarEntry{ Date=DateTime.Parse("2020-02-22"), Price=35m },
                new PricingCalendarEntry{ Date=DateTime.Parse("2020-02-23"), Price=32.5m },
                new PricingCalendarEntry{ Date=DateTime.Parse("2020-02-24"), Price=40m },
                new PricingCalendarEntry{ Date=DateTime.Parse("2020-02-25"), Price=42.5m },
                new PricingCalendarEntry{ Date=DateTime.Parse("2020-02-26"), Price=35m },
                new PricingCalendarEntry{ Date=DateTime.Parse("2020-02-27"), Price=30m },
                new PricingCalendarEntry{ Date=DateTime.Parse("2020-02-28"), Price=30m },
                new PricingCalendarEntry{ Date=DateTime.Parse("2020-02-29"), Price=32.5m },
            };

            foreach (PricingCalendarEntry p in entries)
            {
                context.PricingCalendarEntries.Add(p);
            }

            //var rental = new Rental { StartAt = new DateTime(2020, 2, 3, 23, 30, 0), EndAt = new DateTime(2020, 2, 5, 2, 0, 0) };

            //context.Rentals.Add(rental);

            context.SaveChanges();

        }
    }
}
